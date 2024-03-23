namespace Domain.Tests;

[Collection("SequentialTest")]
public class ItemTests
{
    [Fact]
    public void CreateBacklogItem_TitleDescriptionDeveloperStoryPoints_WhenNoPreConditions_ThenCreateBacklogItem()
    {
        //Arrange
        var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockVersionControl = new Mock<IVersionControlStrategy>();
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintRelease("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

        string initialTitle = "Item";
        string initialDescription = "Description";
        int initialStoryPoints = 1;

        //Act
        Item item = new(initialTitle, initialDescription, mockDeveloper.Object, initialStoryPoints, mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object).SprintBacklog);

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
        var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockVersionControl = new Mock<IVersionControlStrategy>();
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintRelease("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

        string initialTitle = "Item";
        string initialDescription = "Description";
        int initialStoryPoints = 1;

        string newTitle = "newItem";
        string newDescription = "newDescription";
        int newStoryPoints = 3;

        //Act
        Item item = new(initialTitle, initialDescription, mockDeveloper.Object, initialStoryPoints, mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object).SprintBacklog);

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
        var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockVersionControl = new Mock<IVersionControlStrategy>();
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintRelease("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

        var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object).SprintBacklog);
        var mockActivity = new Mock<Activity>(It.IsAny<string>(), mockItem.Object, mockDeveloper.Object);
        mockItem.Object.AddActivityToItem(mockActivity.Object);
        mockActivity.Object.IsFinished = true;

        //Act
        mockItem.Object.DevelopBacklogItem();
        mockItem.Object.FinalizeDevelopmentBacklogItem();
        mockItem.Object.TestingBacklogItem();
        mockItem.Object.FinalizeTestingBacklogItem();
        mockItem.Object.FinalizeBacklogItem();
        mockItem.Object.CloseBacklogItem();

        //Assert
        Assert.NotNull(mockItem.Object);
        Assert.Equal(typeof(DoneState), mockItem.Object.PreviousStatus!.GetType());
        Assert.Equal(typeof(ClosedState), mockItem.Object.CurrentStatus.GetType());
    }
    
   [Fact]
    public void AddBacklogItemToSprint_TitleDescriptionDeveloperStoryPoints_WhenSprintIsExecuted_ThenDontAddBacklogItem()
    {
        //Arrange
        var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockVersionControl = new Mock<IVersionControlStrategy>();
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintRelease("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

        var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object).SprintBacklog);
        var mockActivity = new Mock<Activity>(It.IsAny<string>(), mockItem.Object, mockDeveloper.Object);
        mockItem.Object.AddActivityToItem(mockActivity.Object);
        mockActivity.Object.IsFinished = true;

        //Act
        mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object).ExecuteSprint();
        mockItem.Object.SprintBacklog!.AddItemToBacklog(mockItem.Object);

        //Assert
        Assert.NotNull(mockItem.Object);
        Assert.Equal(0, mockItem.Object.SprintBacklog!.Items.Count);
    }

    [Fact]
    public void CloseBacklogItem_TitleDescriptionDeveloperStoryPoints_WhenBacklogItemInTodoState_ThenDontCloseBacklogItem()
    {
        //Arrange
        var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockVersionControl = new Mock<IVersionControlStrategy>();
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintRelease("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

        var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object).SprintBacklog);

        //Act
        try
        {
            var mockActivity = new Mock<Activity>(It.IsAny<string>(), mockItem.Object, mockDeveloper.Object);
            mockItem.Object.AddActivityToItem(mockActivity.Object);
            mockActivity.Object.IsFinished = true;

            mockItem.Object.CloseBacklogItem();
        }
        catch (Exception){}

        //Assert
        Assert.NotNull(mockItem.Object);
        Assert.Equal(typeof(TodoState), mockItem.Object.CurrentStatus.GetType());
        Assert.NotEqual(typeof(ClosedState), mockItem.Object.CurrentStatus.GetType());
    }
    
    [Fact]
    public void CloseBacklogItem_TitleDescriptionDeveloperStoryPoints_WhenNotAllActivitiesAreFinished_ThenDontCloseBacklogItem()
    {
        //Arrange
        var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockVersionControl = new Mock<IVersionControlStrategy>();
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintRelease("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

        var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object).SprintBacklog);
        var mockActivity = new Mock<Activity>(It.IsAny<string>(), mockItem.Object, mockDeveloper.Object);
        mockItem.Object.AddActivityToItem(mockActivity.Object);

        //Act
        try
        {
            mockItem.Object.DevelopBacklogItem();
            mockItem.Object.FinalizeDevelopmentBacklogItem();
            mockItem.Object.TestingBacklogItem();
            mockItem.Object.FinalizeTestingBacklogItem();
            mockItem.Object.FinalizeBacklogItem();

            //try to close the item
            mockItem.Object.CloseBacklogItem();
        }
        catch (Exception){}

        //Assert
        Assert.NotNull(mockItem.Object);
        Assert.Equal(typeof(TestedState), mockItem.Object.CurrentStatus.GetType());
        Assert.NotEqual(typeof(DoneState), mockItem.Object.CurrentStatus.GetType());
    }

    [Fact]
    public void FinalizeBacklogItem_TitleDescriptionDeveloperStoryPoints_WhenGoingThroughPhases_ThenFinalizeBacklogItem()
    {
        //Arrange
        var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockVersionControl = new Mock<IVersionControlStrategy>();
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintRelease("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

        var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object).SprintBacklog);
        var mockActivity = new Mock<Activity>(It.IsAny<string>(), mockItem.Object, mockDeveloper.Object);
        mockItem.Object.AddActivityToItem(mockActivity.Object);
        mockActivity.Object.IsFinished = true;

        //Act
        mockItem.Object.DevelopBacklogItem();
        mockItem.Object.FinalizeDevelopmentBacklogItem();
        mockItem.Object.TestingBacklogItem();
        mockItem.Object.FinalizeTestingBacklogItem();
        mockItem.Object.FinalizeBacklogItem();

        //Assert
        Assert.NotNull(mockItem.Object);
        Assert.Equal(typeof(TestedState), mockItem.Object.PreviousStatus!.GetType());
        Assert.Equal(typeof(DoneState), mockItem.Object.CurrentStatus.GetType());
    }
    
    [Fact]
    public void ReceiveFeedbackBacklogItem_TitleDescriptionDeveloperStoryPoints_WhenGoingThroughPhases_ThenReceiveFeedbackBacklogItem()
    {
        //Arrange
        var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockVersionControl = new Mock<IVersionControlStrategy>();
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintRelease("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

        var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object).SprintBacklog);
        var mockActivity = new Mock<Activity>(It.IsAny<string>(), mockItem.Object, mockDeveloper.Object);
        mockItem.Object.AddActivityToItem(mockActivity.Object);
        mockActivity.Object.IsFinished = true;

        //Act
        mockItem.Object.DevelopBacklogItem();
        mockItem.Object.FinalizeDevelopmentBacklogItem();
        mockItem.Object.TestingBacklogItem();
        mockItem.Object.FinalizeTestingBacklogItem();
        mockItem.Object.FinalizeBacklogItem();
        mockItem.Object.ReceiveFeedback();

        //Assert
        Assert.NotNull(mockItem.Object);
        Assert.Equal(typeof(DoneState), mockItem.Object.PreviousStatus!.GetType());
        Assert.Equal(typeof(TodoState), mockItem.Object.CurrentStatus.GetType());
    }

    [Fact]
    public void FinalizeTestingBacklogItem_TitleDescriptionDeveloperStoryPoints_WhenGoingThroughPhases_ThenFinalizeTestingBacklogItem()
    {
        //Arrange
        var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockVersionControl = new Mock<IVersionControlStrategy>();
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintRelease("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

        var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object).SprintBacklog);
        var mockActivity = new Mock<Activity>(It.IsAny<string>(), mockItem.Object, mockDeveloper.Object);
        mockItem.Object.AddActivityToItem(mockActivity.Object);
        mockActivity.Object.IsFinished = true;

        //Act
        mockItem.Object.DevelopBacklogItem();
        mockItem.Object.FinalizeDevelopmentBacklogItem();
        mockItem.Object.TestingBacklogItem();
        mockItem.Object.FinalizeTestingBacklogItem();

        //Assert
        Assert.NotNull(mockItem.Object);
        Assert.Equal(typeof(TestingState), mockItem.Object.PreviousStatus!.GetType());
        Assert.Equal(typeof(TestedState), mockItem.Object.CurrentStatus.GetType());
    }
    
    [Fact]
    public void TestingBacklogItem_TitleDescriptionDeveloperStoryPoints_WhenGoingThroughPhases_ThenTestingBacklogItem()
    {
        //Arrange
        var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockVersionControl = new Mock<IVersionControlStrategy>();
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintRelease("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

        var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object).SprintBacklog);
        var mockActivity = new Mock<Activity>(It.IsAny<string>(), mockItem.Object, mockDeveloper.Object);
        mockItem.Object.AddActivityToItem(mockActivity.Object);
        mockActivity.Object.IsFinished = true;

        //Act
        mockItem.Object.DevelopBacklogItem();
        mockItem.Object.FinalizeDevelopmentBacklogItem();
        mockItem.Object.TestingBacklogItem();

        //Assert
        Assert.NotNull(mockItem.Object);
        Assert.Equal(typeof(ReadyForTestingState), mockItem.Object.PreviousStatus!.GetType());
        Assert.Equal(typeof(TestingState), mockItem.Object.CurrentStatus.GetType());
    }

    [Fact]
    public void DenyTestedBacklogItem_TitleDescriptionDeveloperStoryPoints_WhenGoingThroughPhases_ThenDenyTestedBacklogItem()
    {
        //Arrange
        var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockVersionControl = new Mock<IVersionControlStrategy>();
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintRelease("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

        var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object).SprintBacklog);
        var mockActivity = new Mock<Activity>(It.IsAny<string>(), mockItem.Object, mockDeveloper.Object);
        mockItem.Object.AddActivityToItem(mockActivity.Object);
        mockActivity.Object.IsFinished = true;

        //Act
        mockItem.Object.DevelopBacklogItem();
        mockItem.Object.FinalizeDevelopmentBacklogItem();
        mockItem.Object.TestingBacklogItem();
        mockItem.Object.FinalizeTestingBacklogItem();
        mockItem.Object.DenyTestedBacklogItem();

        //Assert
        Assert.NotNull(mockItem.Object);
        Assert.Equal(typeof(TestedState), mockItem.Object.PreviousStatus!.GetType());
        Assert.Equal(typeof(ReadyForTestingState), mockItem.Object.CurrentStatus.GetType());
    }
        
    [Fact]
    public void DenyDevelopedBacklogItem_TitleDescriptionDeveloperStoryPoints_WhenGoingThroughPhases_ThenDenyDevelopedBacklogItem()
    {
        //Arrange
        var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockVersionControl = new Mock<IVersionControlStrategy>();
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintRelease("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

        var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object).SprintBacklog);
        var mockActivity = new Mock<Activity>(It.IsAny<string>(), mockItem.Object, mockDeveloper.Object);
        mockItem.Object.AddActivityToItem(mockActivity.Object);
        mockActivity.Object.IsFinished = true;

        //Act
        mockItem.Object.DevelopBacklogItem();
        mockItem.Object.FinalizeDevelopmentBacklogItem();
        mockItem.Object.TestingBacklogItem();
        mockItem.Object.DenyDevelopedBacklogItem();

        //Assert
        Assert.NotNull(mockItem.Object);
        Assert.Equal(typeof(TestingState), mockItem.Object.PreviousStatus!.GetType());
        Assert.Equal(typeof(TodoState), mockItem.Object.CurrentStatus.GetType());
    }

    [Fact]
    public void FinalizeDevelopmentBacklogItem_TitleDescriptionDeveloperStoryPoints_WhenGoingThroughPhases_ThenFinalizeDevelopmentBacklogItem()
    {
        //Arrange
        var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockVersionControl = new Mock<IVersionControlStrategy>();
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintRelease("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

        var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object).SprintBacklog);
        var mockActivity = new Mock<Activity>(It.IsAny<string>(), mockItem.Object, mockDeveloper.Object);
        mockItem.Object.AddActivityToItem(mockActivity.Object);
        mockActivity.Object.IsFinished = true;

        //Act
        mockItem.Object.DevelopBacklogItem();
        mockItem.Object.FinalizeDevelopmentBacklogItem();

        //Assert
        Assert.NotNull(mockItem.Object);
        Assert.Equal(typeof(DoingState), mockItem.Object.PreviousStatus!.GetType());
        Assert.Equal(typeof(ReadyForTestingState), mockItem.Object.CurrentStatus.GetType());
    }
    
    [Fact]
    public void DevelopBacklogItem_TitleDescriptionDeveloperStoryPoints_WhenGoingThroughPhases_ThenDevelopBacklogItem()
    {
        //Arrange
        var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockVersionControl = new Mock<IVersionControlStrategy>();
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintRelease("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

        var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object).SprintBacklog);
        var mockActivity = new Mock<Activity>(It.IsAny<string>(), mockItem.Object, mockDeveloper.Object);
        mockItem.Object.AddActivityToItem(mockActivity.Object);
        mockActivity.Object.IsFinished = true;

        //Act
        mockItem.Object.DevelopBacklogItem();

        //Assert
        Assert.NotNull(mockItem.Object);
        Assert.Equal(typeof(TodoState), mockItem.Object.PreviousStatus!.GetType());
        Assert.Equal(typeof(DoingState), mockItem.Object.CurrentStatus.GetType());
    }
    
    [Fact]
    public void DevelopBacklogItem_TitleDescriptionDeveloperStoryPoints_WhenSprintBacklogIsNull_ThenDontDevelopBacklogItem()
    {
        //Arrange
        var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockVersionControl = new Mock<IVersionControlStrategy>();
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintRelease("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

        var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1);
        var mockActivity = new Mock<Activity>(It.IsAny<string>(), mockItem.Object, mockDeveloper.Object);
        mockItem.Object.AddActivityToItem(mockActivity.Object);
        mockActivity.Object.IsFinished = true;

        //Act
        mockItem.Object.DevelopBacklogItem();

        //Assert
        Assert.NotNull(mockItem.Object);
        Assert.Equal(typeof(TodoState), mockItem.Object.PreviousStatus!.GetType());
        Assert.Equal(typeof(TodoState), mockItem.Object.CurrentStatus.GetType());
    }
}