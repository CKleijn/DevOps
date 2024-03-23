namespace Domain.Tests;

public class BacklogTests
{
    [Fact]
    public void CreateBacklog_GivenNoProperties_WhenNoPreConditions_ThenCreateBacklog()
    {
        //Arrange
        var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, new GitHub());

        var mockSprint = new Mock<SprintRelease>(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), mockDeveloper.Object, mockProject.Object);
        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(mockSprint.Object);

        // Act
        Backlog backlog = new SprintBacklog(mockSprint.Object);

        // Assert
        Assert.NotNull(backlog);
        Assert.IsType<SprintBacklog>(backlog);
    }

    [Fact]
    public void UpdateBacklog_GivenBacklogItem_WhenNoPreConditions_ThenUpdateBacklog()
    {
        // Arrange
        var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, new GitHub());

        var mockSprint = new Mock<SprintRelease>(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), mockDeveloper.Object, mockProject.Object);
        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(mockSprint.Object);

        var mockInitialItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockSprint.Object.SprintBacklog);
        var mockNewItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockSprint.Object.SprintBacklog);

        // Act
        Backlog backlog = new SprintBacklog(mockSprint.Object);

        backlog.AddItemToBacklog(mockInitialItem.Object);
        backlog.RemoveItemFromBacklog(mockInitialItem.Object);
        backlog.AddItemToBacklog(mockNewItem.Object);

        // Assert
        Assert.NotNull(backlog);
        Assert.Equal(1, backlog.Items.Count);
        Assert.Equal(mockNewItem.Object, backlog.Items[0]);
        Assert.NotEqual(mockInitialItem.Object, backlog.Items[0]);
    }
}