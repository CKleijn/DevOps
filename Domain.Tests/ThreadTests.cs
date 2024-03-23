using Thread = Domain.Entities.Thread;

namespace Domain.Tests;

[Collection("SequentialTest")]
public class ThreadTests
{
    [Fact]
    public void CreateThread_TitleBodyBacklogItem_WhenNoPreConditions_ThenCreateThread()
    {
        //Arrange
        var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockVersionControl = new Mock<IVersionControlStrategy>();
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

        var mockSprint = new Mock<SprintRelease>(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), mockDeveloper.Object, mockProject.Object);
        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(mockSprint.Object);

        var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, It.IsAny<int>(), mockSprint.Object.SprintBacklog);

        string initialTitle = "Title";
        string initialDescription = "Description";

        //Act
        Thread thread = new(initialTitle, initialDescription, mockItem.Object);

        //Assert
        Assert.NotNull(thread);
        Assert.IsType<Thread>(thread);
        Assert.Equal(initialTitle, thread.Subject);
        Assert.Equal(initialDescription, thread.Description);
        Assert.Equal(mockItem.Object, thread.Item);
        Assert.NotNull(thread.Item);
    }
    
    [Fact]
    public void UpdateThread_TitleBodyBacklogItem_WhenNoPreConditions_ThenUpdateThread()
    {
        //Arrange
        var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockVersionControl = new Mock<IVersionControlStrategy>();
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

        var mockSprint = new Mock<SprintRelease>(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), mockDeveloper.Object, mockProject.Object);
        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(mockSprint.Object);

        var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, It.IsAny<int>(), mockSprint.Object.SprintBacklog);

        string initialTitle = "Title";
        string newTitle = "newTitle";

        string initialDescription = "Description";
        string newDescription = "newDescription";

        //Act
        Thread thread = new(initialTitle, initialDescription, mockItem.Object);
        thread.Subject = newTitle;
        thread.Description = newDescription;

        //Assert
        Assert.NotNull(thread);
        Assert.IsType<Thread>(thread);
        Assert.Equal(newTitle, thread.Subject);
        Assert.NotEqual(initialTitle, thread.Subject);
        Assert.Equal(newDescription, thread.Description);
        Assert.NotEqual(initialDescription, thread.Description);
        Assert.Equal(mockItem.Object, thread.Item);
        Assert.NotNull(thread.Item);
    }

    [Fact]
    public void UpdateThread_TitleBodyBacklogItem_WhenBacklogItemIsClosed_ThenDontUpdateThread()
    {
        //Arrange
        var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockVersionControl = new Mock<IVersionControlStrategy>();
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

        var mockSprint = new Mock<SprintRelease>(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), mockDeveloper.Object, mockProject.Object);
        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(mockSprint.Object);

        var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, It.IsAny<int>(), mockSprint.Object.SprintBacklog);
        mockItem.Object.CurrentStatus = new ClosedState(mockItem.Object);

        string initialTitle = "Title";
        string newTitle = "newTitle";

        string initialDescription = "Description";
        string newDescription = "newDescription";

        //Act
        Thread thread = new(initialTitle, initialDescription, mockItem.Object);
        thread.Subject = newTitle;
        thread.Description = newDescription;

        //Assert
        Assert.NotNull(thread);
        Assert.IsType<Thread>(thread);
        Assert.NotEqual(newTitle, thread.Subject);
        Assert.Equal(initialTitle, thread.Subject);
        Assert.NotEqual(newDescription, thread.Description);
        Assert.Equal(initialDescription, thread.Description);
        Assert.Equal(mockItem.Object, thread.Item);
        Assert.NotNull(thread.Item);
    }
}