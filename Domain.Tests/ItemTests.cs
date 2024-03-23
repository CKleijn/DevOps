using Domain.States.BacklogItem;

namespace Domain.Tests;

public class ItemTests
{
    [Fact]
    public void CreateBacklogItem_TitleDescriptionDeveloperStoryPoints_WhenNoPreConditions_ThenCreateBacklogItem()
    {
        //Arrange
        ISprintFactory<SprintRelease> factory = new SprintReleaseFactory();
                
        List<NotificationProvider> notificationProviders = new();
        notificationProviders.Add(NotificationProvider.MAIL);
        
        Developer scrumMaster = new("name", "email", "password", notificationProviders);
        IVersionControlStrategy versionControl = new GitHub();
        ProductOwner productOwner = new("name", "email", "password", notificationProviders);
                
        Project project = new("Project", "Description", productOwner, versionControl);
        SprintRelease sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
        
        string initialTitle = "Item";
        string initialDescription = "Description";
        int initialStoryPoints = 1;
        
        //Act
        Item item = new(initialTitle, initialDescription, scrumMaster, initialStoryPoints, sprint.SprintBacklog);
        
        //Assert
        Assert.NotNull(item);
        Assert.Equal(initialTitle, item.Title);
        Assert.Equal(initialDescription, item.Description);
        Assert.Equal(initialStoryPoints, item.StoryPoints);
        Assert.NotNull(item.SprintBacklog);
    }
    
    [Fact]
    public void UpdateBacklogItem_TitleDescriptionDeveloperStoryPoints_WhenNoPreConditions_ThenUpdateBacklogItem()
    {
        //Arrange
        ISprintFactory<SprintRelease> factory = new SprintReleaseFactory();
                
        List<NotificationProvider> notificationProviders = new();
        notificationProviders.Add(NotificationProvider.MAIL);
        
        Developer scrumMaster = new("name", "email", "password", notificationProviders);
        IVersionControlStrategy versionControl = new GitHub();
        ProductOwner productOwner = new("name", "email", "password", notificationProviders);
                
        Project project = new("Project", "Description", productOwner, versionControl);
        SprintRelease sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
        
        string initialTitle = "Item";
        string initialDescription = "Description";
        int initialStoryPoints = 1;
        
        string newTitle = "newItem";
        string newDescription = "newDescription";
        int newStoryPoints = 3;
        
        //Act
        Item item = new(initialTitle, initialDescription, scrumMaster, initialStoryPoints, sprint.SprintBacklog);

        item.Title = newTitle;
        item.Description = newDescription;
        item.StoryPoints = newStoryPoints;
        
        //Assert
        Assert.NotNull(item);
        Assert.Equal(newTitle, item.Title);
        Assert.NotEqual(initialTitle, item.Title);
        Assert.Equal(newDescription, item.Description);
        Assert.NotEqual(initialDescription, item.Description);
        Assert.Equal(newStoryPoints, item.StoryPoints);
        Assert.NotEqual(initialStoryPoints, item.StoryPoints);
        Assert.NotNull(item.SprintBacklog);
    }
    
    [Fact]
    public void CloseBacklogItem_TitleDescriptionDeveloperStoryPoints_WhenGoingThroughPhases_ThenCloseBacklogItem()
    {
        //Arrange
        ISprintFactory<SprintRelease> factory = new SprintReleaseFactory();
                
        List<NotificationProvider> notificationProviders = new();
        notificationProviders.Add(NotificationProvider.MAIL);
        
        Developer scrumMaster = new("name", "email", "password", notificationProviders);
        IVersionControlStrategy versionControl = new GitHub();
        ProductOwner productOwner = new("name", "email", "password", notificationProviders);
                
        Project project = new("Project", "Description", productOwner, versionControl);
        SprintRelease sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
        
        Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
        Activity activity = new("Activity", item, scrumMaster);
        item.AddActivityToItem(activity);
        activity.IsFinished = true;
        
        //Act
        item.DevelopBacklogItem();
        item.FinalizeDevelopmentBacklogItem();
        item.TestingBacklogItem();
        item.FinalizeTestingBacklogItem();
        item.FinalizeBacklogItem();
        item.CloseBacklogItem();
        
        //Assert
        Assert.NotNull(item);
        Assert.Equal(typeof(DoneState), item.PreviousStatus!.GetType());
        Assert.Equal(typeof(ClosedState), item.CurrentStatus.GetType());
    }
    
    [Fact]
    public void AddBacklogItemToSprint_TitleDescriptionDeveloperStoryPoints_WhenSprintIsExecuted_ThenDontAddBacklogItem()
    {
        //Arrange
        ISprintFactory<SprintRelease> factory = new SprintReleaseFactory();
                
        List<NotificationProvider> notificationProviders = new();
        notificationProviders.Add(NotificationProvider.MAIL);
        
        Developer scrumMaster = new("name", "email", "password", notificationProviders);
        IVersionControlStrategy versionControl = new GitHub();
        ProductOwner productOwner = new("name", "email", "password", notificationProviders);
                
        Project project = new("Project", "Description", productOwner, versionControl);
        SprintRelease sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
        
        Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
        Activity activity = new("Activity", item, scrumMaster);
        item.AddActivityToItem(activity);
        activity.IsFinished = true;
        
        //Act
        sprint.ExecuteSprint();
        item.SprintBacklog!.AddItemToBacklog(item);
        
        //Assert
        Assert.NotNull(item);
        Assert.Equal(0, item.SprintBacklog!.Items.Count);
    }
    
    [Fact]
    public void CloseBacklogItem_TitleDescriptionDeveloperStoryPoints_WhenBacklogItemInTodoState_ThenDontCloseBacklogItem()
    {
        //Arrange
        ISprintFactory<SprintRelease> factory = new SprintReleaseFactory();
                
        List<NotificationProvider> notificationProviders = new();
        notificationProviders.Add(NotificationProvider.MAIL);
        
        Developer scrumMaster = new("name", "email", "password", notificationProviders);
        IVersionControlStrategy versionControl = new GitHub();
        ProductOwner productOwner = new("name", "email", "password", notificationProviders);
                
        Project project = new("Project", "Description", productOwner, versionControl);
        SprintRelease sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
        
        Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
        
        //Act
        try
        {
            Activity activity = new("Activity", item, scrumMaster);
            item.AddActivityToItem(activity);
            activity.IsFinished = true;
            
            item.CloseBacklogItem();
        }
        catch (Exception e){}

        //Assert
        Assert.NotNull(item);
        Assert.Equal(typeof(TodoState), item.CurrentStatus.GetType());
        Assert.NotEqual(typeof(ClosedState), item.CurrentStatus.GetType());
    }
    
    [Fact]
    public void CloseBacklogItem_TitleDescriptionDeveloperStoryPoints_WhenNotAllActivitiesAreFinished_ThenDontCloseBacklogItem()
    {
        //Arrange
        ISprintFactory<SprintRelease> factory = new SprintReleaseFactory();
                
        List<NotificationProvider> notificationProviders = new();
        notificationProviders.Add(NotificationProvider.MAIL);
        
        Developer scrumMaster = new("name", "email", "password", notificationProviders);
        IVersionControlStrategy versionControl = new GitHub();
        ProductOwner productOwner = new("name", "email", "password", notificationProviders);
                
        Project project = new("Project", "Description", productOwner, versionControl);
        SprintRelease sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
        
        Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
        Activity activity = new("Activity", item, scrumMaster);
        item.AddActivityToItem(activity);
        
        //Act
        try
        {
            item.DevelopBacklogItem();
            item.FinalizeDevelopmentBacklogItem();
            item.TestingBacklogItem();
            item.FinalizeTestingBacklogItem();
            item.FinalizeBacklogItem();

            //try to close the item
            item.CloseBacklogItem();
        }
        catch (Exception e){}

        //Assert
        Assert.NotNull(item);
        Assert.Equal(typeof(TestedState), item.CurrentStatus.GetType());
        Assert.NotEqual(typeof(DoneState), item.CurrentStatus.GetType());
    }
    
    [Fact]
    public void FinalizeBacklogItem_TitleDescriptionDeveloperStoryPoints_WhenGoingThroughPhases_ThenFinalizeBacklogItem()
    {
        //Arrange
        ISprintFactory<SprintRelease> factory = new SprintReleaseFactory();
                
        List<NotificationProvider> notificationProviders = new();
        notificationProviders.Add(NotificationProvider.MAIL);
        
        Developer scrumMaster = new("name", "email", "password", notificationProviders);
        IVersionControlStrategy versionControl = new GitHub();
        ProductOwner productOwner = new("name", "email", "password", notificationProviders);
                
        Project project = new("Project", "Description", productOwner, versionControl);
        SprintRelease sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
        
        Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
        Activity activity = new("Activity", item, scrumMaster);
        item.AddActivityToItem(activity);
        activity.IsFinished = true;
        
        //Act
        item.DevelopBacklogItem();
        item.FinalizeDevelopmentBacklogItem();
        item.TestingBacklogItem();
        item.FinalizeTestingBacklogItem();
        item.FinalizeBacklogItem();
        
        //Assert
        Assert.NotNull(item);
        Assert.Equal(typeof(TestedState), item.PreviousStatus!.GetType());
        Assert.Equal(typeof(DoneState), item.CurrentStatus.GetType());
    }
    
    [Fact]
    public void ReceiveFeedbackBacklogItem_TitleDescriptionDeveloperStoryPoints_WhenGoingThroughPhases_ThenReceiveFeedbackBacklogItem()
    {
        //Arrange
        ISprintFactory<SprintRelease> factory = new SprintReleaseFactory();
                
        List<NotificationProvider> notificationProviders = new();
        notificationProviders.Add(NotificationProvider.MAIL);
        
        Developer scrumMaster = new("name", "email", "password", notificationProviders);
        IVersionControlStrategy versionControl = new GitHub();
        ProductOwner productOwner = new("name", "email", "password", notificationProviders);
                
        Project project = new("Project", "Description", productOwner, versionControl);
        SprintRelease sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
        
        Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
        Activity activity = new("Activity", item, scrumMaster);
        item.AddActivityToItem(activity);
        activity.IsFinished = true;
        
        //Act
        item.DevelopBacklogItem();
        item.FinalizeDevelopmentBacklogItem();
        item.TestingBacklogItem();
        item.FinalizeTestingBacklogItem();
        item.FinalizeBacklogItem();
        item.ReceiveFeedback();
        
        //Assert
        Assert.NotNull(item);
        Assert.Equal(typeof(DoneState), item.PreviousStatus!.GetType());
        Assert.Equal(typeof(TodoState), item.CurrentStatus.GetType());
    }
    
    [Fact]
    public void FinalizeTestingBacklogItem_TitleDescriptionDeveloperStoryPoints_WhenGoingThroughPhases_ThenFinalizeTestingBacklogItem()
    {
        //Arrange
        ISprintFactory<SprintRelease> factory = new SprintReleaseFactory();
                
        List<NotificationProvider> notificationProviders = new();
        notificationProviders.Add(NotificationProvider.MAIL);
        
        Developer scrumMaster = new("name", "email", "password", notificationProviders);
        IVersionControlStrategy versionControl = new GitHub();
        ProductOwner productOwner = new("name", "email", "password", notificationProviders);
                
        Project project = new("Project", "Description", productOwner, versionControl);
        SprintRelease sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
        
        Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
        Activity activity = new("Activity", item, scrumMaster);
        item.AddActivityToItem(activity);
        activity.IsFinished = true;
        
        //Act
        item.DevelopBacklogItem();
        item.FinalizeDevelopmentBacklogItem();
        item.TestingBacklogItem();
        item.FinalizeTestingBacklogItem();
        
        //Assert
        Assert.NotNull(item);
        Assert.Equal(typeof(TestingState), item.PreviousStatus!.GetType());
        Assert.Equal(typeof(TestedState), item.CurrentStatus.GetType());
    }
    
    [Fact]
    public void TestingBacklogItem_TitleDescriptionDeveloperStoryPoints_WhenGoingThroughPhases_ThenTestingBacklogItem()
    {
        //Arrange
        ISprintFactory<SprintRelease> factory = new SprintReleaseFactory();
                
        List<NotificationProvider> notificationProviders = new();
        notificationProviders.Add(NotificationProvider.MAIL);
        
        Developer scrumMaster = new("name", "email", "password", notificationProviders);
        IVersionControlStrategy versionControl = new GitHub();
        ProductOwner productOwner = new("name", "email", "password", notificationProviders);
                
        Project project = new("Project", "Description", productOwner, versionControl);
        SprintRelease sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
        
        Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
        Activity activity = new("Activity", item, scrumMaster);
        item.AddActivityToItem(activity);
        activity.IsFinished = true;
        
        //Act
        item.DevelopBacklogItem();
        item.FinalizeDevelopmentBacklogItem();
        item.TestingBacklogItem();
        
        //Assert
        Assert.NotNull(item);
        Assert.Equal(typeof(ReadyForTestingState), item.PreviousStatus!.GetType());
        Assert.Equal(typeof(TestingState), item.CurrentStatus.GetType());
    }
    
    [Fact]
    public void DenyTestedBacklogItem_TitleDescriptionDeveloperStoryPoints_WhenGoingThroughPhases_ThenDenyTestedBacklogItem()
    {
        //Arrange
        ISprintFactory<SprintRelease> factory = new SprintReleaseFactory();
                
        List<NotificationProvider> notificationProviders = new();
        notificationProviders.Add(NotificationProvider.MAIL);
        
        Developer scrumMaster = new("name", "email", "password", notificationProviders);
        IVersionControlStrategy versionControl = new GitHub();
        ProductOwner productOwner = new("name", "email", "password", notificationProviders);
                
        Project project = new("Project", "Description", productOwner, versionControl);
        SprintRelease sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
        
        Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
        Activity activity = new("Activity", item, scrumMaster);
        item.AddActivityToItem(activity);
        activity.IsFinished = true;
        
        //Act
        item.DevelopBacklogItem();
        item.FinalizeDevelopmentBacklogItem();
        item.TestingBacklogItem();
        item.FinalizeTestingBacklogItem();
        item.DenyTestedBacklogItem();
        
        //Assert
        Assert.NotNull(item);
        Assert.Equal(typeof(TestedState), item.PreviousStatus!.GetType());
        Assert.Equal(typeof(ReadyForTestingState), item.CurrentStatus.GetType());
    }
    
    [Fact]
    public void DenyDevelopedBacklogItem_TitleDescriptionDeveloperStoryPoints_WhenGoingThroughPhases_ThenDenyDevelopedBacklogItem()
    {
        //Arrange
        ISprintFactory<SprintRelease> factory = new SprintReleaseFactory();
                
        List<NotificationProvider> notificationProviders = new();
        notificationProviders.Add(NotificationProvider.MAIL);
        
        Developer scrumMaster = new("name", "email", "password", notificationProviders);
        IVersionControlStrategy versionControl = new GitHub();
        ProductOwner productOwner = new("name", "email", "password", notificationProviders);
                
        Project project = new("Project", "Description", productOwner, versionControl);
        SprintRelease sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
        
        Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
        Activity activity = new("Activity", item, scrumMaster);
        item.AddActivityToItem(activity);
        activity.IsFinished = true;
        
        //Act
        item.DevelopBacklogItem();
        item.FinalizeDevelopmentBacklogItem();
        item.TestingBacklogItem();
        item.DenyDevelopedBacklogItem();
        
        //Assert
        Assert.NotNull(item);
        Assert.Equal(typeof(TestingState), item.PreviousStatus!.GetType());
        Assert.Equal(typeof(TodoState), item.CurrentStatus.GetType());
    }
    
    [Fact]
    public void FinalizeDevelopmentBacklogItem_TitleDescriptionDeveloperStoryPoints_WhenGoingThroughPhases_ThenFinalizeDevelopmentBacklogItem()
    {
        //Arrange
        ISprintFactory<SprintRelease> factory = new SprintReleaseFactory();
                
        List<NotificationProvider> notificationProviders = new();
        notificationProviders.Add(NotificationProvider.MAIL);
        
        Developer scrumMaster = new("name", "email", "password", notificationProviders);
        IVersionControlStrategy versionControl = new GitHub();
        ProductOwner productOwner = new("name", "email", "password", notificationProviders);
                
        Project project = new("Project", "Description", productOwner, versionControl);
        SprintRelease sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
        
        Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
        Activity activity = new("Activity", item, scrumMaster);
        item.AddActivityToItem(activity);
        activity.IsFinished = true;
        
        //Act
        item.DevelopBacklogItem();
        item.FinalizeDevelopmentBacklogItem();
        
        //Assert
        Assert.NotNull(item);
        Assert.Equal(typeof(DoingState), item.PreviousStatus!.GetType());
        Assert.Equal(typeof(ReadyForTestingState), item.CurrentStatus.GetType());
    }
    
    [Fact]
    public void DevelopBacklogItem_TitleDescriptionDeveloperStoryPoints_WhenGoingThroughPhases_ThenDevelopBacklogItem()
    {
        //Arrange
        ISprintFactory<SprintRelease> factory = new SprintReleaseFactory();
                
        List<NotificationProvider> notificationProviders = new();
        notificationProviders.Add(NotificationProvider.MAIL);
        
        Developer scrumMaster = new("name", "email", "password", notificationProviders);
        IVersionControlStrategy versionControl = new GitHub();
        ProductOwner productOwner = new("name", "email", "password", notificationProviders);
                
        Project project = new("Project", "Description", productOwner, versionControl);
        SprintRelease sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
        
        Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
        Activity activity = new("Activity", item, scrumMaster);
        item.AddActivityToItem(activity);
        activity.IsFinished = true;
        
        //Act
        item.DevelopBacklogItem();
        
        //Assert
        Assert.NotNull(item);
        Assert.Equal(typeof(TodoState), item.PreviousStatus!.GetType());
        Assert.Equal(typeof(DoingState), item.CurrentStatus.GetType());
    }

}