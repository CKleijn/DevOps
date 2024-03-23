using Domain.States.BacklogItem;

namespace Domain.Tests;

[Collection("SequentialTest")]
public class ActivityTests
{
    [Fact]
    public void FinishActivity_GivenTitleItemDeveloper_WhenNoPreConditions_ThenFinishActivity()
    {
        //Arrange
        var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockVersionControl = new Mock<IVersionControlStrategy>();
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

        var mockSprint = new Mock<SprintRelease>(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), mockDeveloper.Object, mockProject.Object);
        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(mockSprint.Object);

        var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockSprint.Object.SprintBacklog);

        // Act
        Activity activity = new("Activity", mockItem.Object, mockDeveloper.Object);
        mockItem.Object.AddActivityToItem(activity);
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
        var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockVersionControl = new Mock<IVersionControlStrategy>();
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

        var mockSprint = new Mock<SprintRelease>(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), mockDeveloper.Object, mockProject.Object);
        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(mockSprint.Object);

        string initialTitle = "Activity";
        string newTitle = "NewTitle";

        // Act
        var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockSprint.Object.SprintBacklog);
        Activity activity = new(initialTitle, mockItem.Object, mockDeveloper.Object);
        mockItem.Object.AddActivityToItem(activity);

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
        var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockVersionControl = new Mock<IVersionControlStrategy>();
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

        var mockSprint = new Mock<SprintRelease>(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), mockDeveloper.Object, mockProject.Object);
        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(mockSprint.Object);

        string initialTitle = "Activity";
        string newTitle = "NewTitle";

        // Act
        var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockSprint.Object.SprintBacklog);
        Activity activity = new(initialTitle, mockItem.Object, mockDeveloper.Object);
        mockItem.Object.AddActivityToItem(activity);

        activity.Title = newTitle;
        activity.IsFinished = true;

        mockItem.Object.DevelopBacklogItem();
        mockItem.Object.FinalizeDevelopmentBacklogItem();
        mockItem.Object.TestingBacklogItem();
        mockItem.Object.FinalizeTestingBacklogItem();
        mockItem.Object.FinalizeBacklogItem();
        mockItem.Object.CloseBacklogItem();

        //won't be executed
        activity.IsFinished = false;

        // Assert
        Assert.True(activity.IsFinished);
        Assert.IsType<Activity>(activity);
        Assert.NotNull(activity);
        Assert.Equal(newTitle, activity.Title);
        Assert.NotEqual(initialTitle, activity.Title);
        Assert.Equal(typeof(ClosedState), mockItem.Object.CurrentStatus.GetType());
    }
    
    [Fact]
    public void UpdateActivity_GivenTitleItemDeveloper_WhenItemIsDone_ThenDontUpdateActivity()
    {
        //Arrange
        var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockVersionControl = new Mock<IVersionControlStrategy>();
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

        var mockSprint = new Mock<SprintRelease>(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), mockDeveloper.Object, mockProject.Object);
        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(mockSprint.Object);

        string initialTitle = "Activity";
        string newTitle = "NewTitle";

        // Act
        var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockSprint.Object.SprintBacklog);
        Activity activity = new(initialTitle, mockItem.Object, mockDeveloper.Object);
        mockItem.Object.AddActivityToItem(activity);

        activity.Title = newTitle;
        activity.IsFinished = true;

        mockItem.Object.DevelopBacklogItem();
        mockItem.Object.FinalizeDevelopmentBacklogItem();
        mockItem.Object.TestingBacklogItem();
        mockItem.Object.FinalizeTestingBacklogItem();
        mockItem.Object.FinalizeBacklogItem();

        // Assert
        Assert.True(activity.IsFinished);
        Assert.IsType<Activity>(activity);
        Assert.NotNull(activity);
        Assert.Equal(newTitle, activity.Title);
        Assert.NotEqual(initialTitle, activity.Title);
        Assert.Equal(typeof(DoneState), mockItem.Object.CurrentStatus.GetType());
    }

    [Fact]
    public void CreateActivity_GivenTitleItemDeveloper_WhenNoPreConditions_ThenCreateActivity()
    {
        //Arrange
        var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockVersionControl = new Mock<IVersionControlStrategy>();
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

        var mockSprint = new Mock<SprintRelease>(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), mockDeveloper.Object, mockProject.Object);
        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(mockSprint.Object);

        var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockSprint.Object.SprintBacklog);

        // Act
        Activity activity = new("Activity", mockItem.Object, mockDeveloper.Object);

        // Assert
        Assert.IsType<Activity>(activity);
        Assert.NotNull(activity);
        Assert.NotNull(activity.Developer);
    }
    
    [Fact]
    public void CreateActivity_GivenTitleItem_WhenNoDeveloperProvided_ThenCreateActivity()
    {
        //Arrange
        var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockVersionControl = new Mock<IVersionControlStrategy>();
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

        var mockSprint = new Mock<SprintRelease>(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), mockDeveloper.Object, mockProject.Object);
        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(mockSprint.Object);

        var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockSprint.Object.SprintBacklog);

        // Act
        Activity activity = new("Activity", mockItem.Object);

        // Assert
        Assert.IsType<Activity>(activity);
        Assert.NotNull(activity);
        Assert.Null(activity.Developer);
    }
}