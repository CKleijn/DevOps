namespace Domain.Tests;

public class ProjectTests
{
    [Fact]
    public void CreateProject_GivenTitleDescriptionPasswordScrumMaster_WhenNoPreConditions_ThenCreateProject()
    {
        // Arrange
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockVersionControl = new Mock<IVersionControlStrategy>();

        string projectTitle = "Project";
        string projectDescription = "Description";

        // Act
        Project project = new(projectTitle, projectDescription, mockProductOwner.Object, mockVersionControl.Object);

        // Assert
        Assert.NotNull(project);
        Assert.IsType<Project>(project);
        Assert.NotNull(project.Backlog);
        Assert.NotNull(project.VersionControl);
        Assert.Equal(project.Title, projectTitle);
        Assert.Equal(project.Description, projectDescription);
        Assert.Equal(project.ProductOwner, mockProductOwner.Object);
        Assert.Equal(project.VersionControl, mockVersionControl.Object);
    }

    [Fact]
    public void UpdateProject_GivenTitleDescriptionPasswordScrumMasterPipeline_WhenNoPreConditions_ThenUpdateProject()
    {
        // Arrange
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockNewProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());

        var mockVersionControl = new Mock<IVersionControlStrategy>();
        var mockNewVersionControl = new Mock<IVersionControlStrategy>();

        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());

        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

        var mockSprintFactory = new Mock<ISprintFactory<SprintRelease>>();
        var mockSprint = new Mock<SprintRelease>(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), mockDeveloper.Object, mockProject.Object);
        mockSprintFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), mockProject.Object)).Returns(mockSprint.Object);

        var mockPipeline = new Mock<TestPipeline>(It.IsAny<string>(), mockSprint.Object);
        var mockNewPipeline = new Mock<ReleasePipeline>(It.IsAny<string>(), mockSprint.Object);

        string projectTitle = "Project";
        string newProjectTitle = "NewProject";

        string projectDescription = "Description";
        string newProjectDescription = "NewDescription";

        Project project = new(projectTitle, projectDescription, mockProductOwner.Object, mockVersionControl.Object);

        // Act
        project.Title = newProjectTitle;
        project.Description = newProjectDescription;
        project.ProductOwner = mockNewProductOwner.Object;
        project.VersionControl = mockNewVersionControl.Object;

        project.AddPipeline(mockPipeline.Object);
        project.RemovePipeline(mockPipeline.Object);
        project.AddPipeline(mockNewPipeline.Object);

        // Assert
        Assert.NotNull(project);
        Assert.NotNull(project.Backlog);
        Assert.NotNull(project.VersionControl);
        Assert.Equal(newProjectTitle, project.Title);
        Assert.Equal(newProjectDescription, project.Description);
        Assert.Equal(mockNewProductOwner.Object, project.ProductOwner);
        Assert.Equal(mockNewVersionControl.Object, project.VersionControl);
        Assert.Equal(mockNewPipeline.Object, project.Pipelines![0]);
        Assert.Equal(1, project.Pipelines.Count);
    }
}