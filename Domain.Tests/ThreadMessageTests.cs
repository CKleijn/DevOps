using Thread = Domain.Entities.Thread;

namespace Domain.Tests;

[Collection("SequentialTest")]
public class ThreadMessageTests
{
    [Fact]
    public void CreateThreadMessage_TitleBodyBacklogItem_WhenNoPreConditions_ThenCreateThreadMessage()
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
        var mockThread = new Mock<Thread>(It.IsAny<string>(), It.IsAny<string>(), mockItem.Object);

        //Act
        var message = new ThreadMessage("Title", "Body", mockThread.Object);

        mockThread.Object.AddThreadMessage(message);

        //Assert
        Assert.NotNull(message);
        Assert.IsType<ThreadMessage>(message);
        Assert.NotNull(message.Thread.ThreadMessages);
        Assert.NotNull(message.Thread);
        Assert.Equal(1, message.Thread.ThreadMessages.Count);
    }
    
    [Fact]
    public void UpdateThreadMessage_TitleBodyBacklogItem_WhenBacklogItemIsClosed_ThenDontAddMessageAndDontUpdateThreadMessage()
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
        var mockThread = new Mock<Thread>(It.IsAny<string>(), It.IsAny<string>(), mockItem.Object);

        string initialTitle = "Title";
        string newTitle = "newTitle";

        string initialBody = "Description";
        string newBody = "newDescription";

        //Act
        mockItem.Object.CurrentStatus = new ClosedState(mockItem.Object);

        ThreadMessage message = new(initialTitle, initialBody, mockThread.Object);
        message.Title = newTitle;
        message.Body = newBody;

        mockThread.Object.AddThreadMessage(message);

        //Assert
        Assert.NotNull(message);
        Assert.IsType<ThreadMessage>(message);
        Assert.NotEqual(newTitle, message.Title);
        Assert.Equal(initialTitle, message.Title);
        Assert.NotEqual(newBody, message.Body);
        Assert.Equal(initialBody, message.Body);
        Assert.Equal(0, message.Thread.ThreadMessages.Count);
    }
    
    [Fact]
    public void UpdateThreadMessage_TitleBodyBacklogItem_WhenNoPreConditions_ThenAddMessageAndUpdateThreadMessage()
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
        var mockThread = new Mock<Thread>(It.IsAny<string>(), It.IsAny<string>(), mockItem.Object);

        string initialTitle = "Title";
        string newTitle = "newTitle";

        string initialBody = "Description";
        string newBody = "newDescription";

        //Act
        ThreadMessage message = new(initialTitle, initialBody, mockThread.Object);
        message.Title = newTitle;
        message.Body = newBody;

        mockThread.Object.AddThreadMessage(message);
        mockThread.Object.RemoveThreadMessage(message);
        mockThread.Object.AddThreadMessage(message);

        //Assert
        Assert.NotNull(message);
        Assert.IsType<ThreadMessage>(message);
        Assert.Equal(newTitle, message.Title);
        Assert.NotEqual(initialTitle, message.Title);
        Assert.Equal(newBody, message.Body);
        Assert.NotEqual(initialBody, message.Body);
        Assert.Equal(1, message.Thread.ThreadMessages.Count);
    }
    
    [Fact]
    public void UpdateThreadMessage_TitleBodyBacklogItem_WhenBacklogItemIsClosedAfterAddingMessage_ThenDontUpdateThreadMessage()
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
        var mockThread = new Mock<Thread>(It.IsAny<string>(), It.IsAny<string>(), mockItem.Object);

        string initialTitle = "Title";
        string newTitle = "newTitle";

        string initialBody = "Description";
        string newBody = "newDescription";

        //Act
        ThreadMessage message = new(initialTitle, initialBody, mockThread.Object);

        mockThread.Object.AddThreadMessage(message);
        
        mockItem.Object.CurrentStatus = new ClosedState(mockItem.Object);
        
        message.Title = newTitle;
        message.Body = newBody;
        
        //Assert
        Assert.NotNull(message);
        Assert.IsType<ThreadMessage>(message);
        Assert.NotEqual(newTitle, message.Title);
        Assert.Equal(initialTitle, message.Title);
        Assert.NotEqual(newBody, message.Body);
        Assert.Equal(initialBody, message.Body);
        Assert.Equal(1, message.Thread.ThreadMessages.Count);
    }
}