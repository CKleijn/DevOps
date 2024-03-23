using Domain.States.BacklogItem;

namespace Domain.Tests;

public class ActivityTests
{
    [Fact]
    public void FinishActivity_GivenTitleItemDeveloper_WhenNoPreConditions_ThenFinishActivity()
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
        
        // Act
        Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
        Activity activity = new("Activity", item, scrumMaster);
        item.AddActivityToItem(activity);
        activity.IsFinished = true;

        // Assert
        Assert.True(activity.IsFinished);
        Assert.IsType<Activity>(activity);
        Assert.NotNull(activity);
    }
    
    [Fact]
    public void UpdateActivity_GivenTitleItemDeveloper_WhenNoPreConditions_ThenUpdateActivity()
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
        
        string initialTitle = "Activity";
        string newTitle = "NewTitle";
        
        // Act
        Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
        Activity activity = new(initialTitle, item, scrumMaster);
        item.AddActivityToItem(activity);

        activity.Title = newTitle;
        activity.IsFinished = true;
        
        // Assert
        Assert.True(activity.IsFinished);
        Assert.IsType<Activity>(activity);
        Assert.NotNull(activity);
        Assert.Equal(newTitle, activity.Title);
        Assert.NotEqual(initialTitle, activity.Title);
    }
    
    [Fact]
    public void UpdateActivity_GivenTitleItemDeveloper_WhenItemIsClosed_ThenDontUpdateActivity()
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
        
        string initialTitle = "Activity";
        string newTitle = "NewTitle";
        
        // Act
        Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
        Activity activity = new(initialTitle, item, scrumMaster);
        item.AddActivityToItem(activity);

        activity.Title = newTitle;
        activity.IsFinished = true;
        
        item.DevelopBacklogItem();
        item.FinalizeDevelopmentBacklogItem();
        item.TestingBacklogItem();
        item.FinalizeTestingBacklogItem();
        item.FinalizeBacklogItem();
        item.CloseBacklogItem();
        
        //won't be executed
        activity.IsFinished = false;

        // Assert
        Assert.True(activity.IsFinished);
        Assert.IsType<Activity>(activity);
        Assert.NotNull(activity);
        Assert.Equal(newTitle, activity.Title);
        Assert.NotEqual(initialTitle, activity.Title);
        Assert.Equal(typeof(ClosedState), item.CurrentStatus.GetType());
    }
    
    [Fact]
    public void UpdateActivity_GivenTitleItemDeveloper_WhenItemIsDone_ThenDontUpdateActivity()
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
        
        string initialTitle = "Activity";
        string newTitle = "NewTitle";
        
        // Act
        Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
        Activity activity = new(initialTitle, item, scrumMaster);
        item.AddActivityToItem(activity);

        activity.Title = newTitle;
        activity.IsFinished = true;
        
        item.DevelopBacklogItem();
        item.FinalizeDevelopmentBacklogItem();
        item.TestingBacklogItem();
        item.FinalizeTestingBacklogItem();
        item.FinalizeBacklogItem();
        
        // Assert
        Assert.True(activity.IsFinished);
        Assert.IsType<Activity>(activity);
        Assert.NotNull(activity);
        Assert.Equal(newTitle, activity.Title);
        Assert.NotEqual(initialTitle, activity.Title);
        Assert.Equal(typeof(DoneState), item.CurrentStatus.GetType());
    }
    
    [Fact]
    public void CreateActivity_GivenTitleItemDeveloper_WhenNoPreConditions_ThenCreateActivity()
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
        
        // Act
        Activity activity = new("Activity", item, scrumMaster);
        
        // Assert
        Assert.IsType<Activity>(activity);
        Assert.NotNull(activity);
        Assert.NotNull(activity.Developer);
    }
    
    [Fact]
    public void CreateActivity_GivenTitleItem_WhenNoDeveloperProvided_ThenCreateActivity()
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
        
        // Act
        Activity activity = new("Activity", item);
        
        // Assert
        Assert.IsType<Activity>(activity);
        Assert.NotNull(activity);
        Assert.Null(activity.Developer);
    }
}