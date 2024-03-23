using Moq;

namespace Domain.Tests;

public class VersionControlTests
{
    [Fact]
    public void CreateProjectWithVersionControl_GivenTitleDescriptionProductOwnerVersionControl_WhenNoPreConditions_ThenCreateProjectWithSelectedVersionControl()
    {
        // Arrange
        var title = "Project1";
        var description = "Description1";
        var productOwnerMock = new Mock<ProductOwner>("Klaas", "klaas@mail.com", "Password", new List<NotificationProvider> { NotificationProvider.MAIL });

        // Act
        var project = new Project(title, description, productOwnerMock.Object, new GitHub());

        // Assert
        Assert.NotNull(project);
        Assert.IsType<Project>(project);
        Assert.Equal(title, project.Title);
        Assert.Equal(description, project.Description);
        Assert.Equal(productOwnerMock.Object, project.ProductOwner);
        Assert.Equal(typeof(GitHub), project.VersionControl.GetType());
    }

    [Fact]
    public void UpdateVersionControlWithinAExistingProject_GivenVersionControl_WhenProjectExists_ThenUpdateVersionControlInsideProject()
    {
        // Arrange
        var title = "Project1";
        var description = "Description1";
        var productOwnerMock = new Mock<ProductOwner>("Klaas", "klaas@mail.com", "Password", new List<NotificationProvider> { NotificationProvider.MAIL });

        var project = new Project(title, description, productOwnerMock.Object, new GitHub());

        // Act
        project.VersionControl = new GitLab();

        // Assert
        Assert.NotNull(project);
        Assert.IsType<Project>(project);
        Assert.Equal(title, project.Title);
        Assert.Equal(description, project.Description);
        Assert.Equal(productOwnerMock.Object, project.ProductOwner);
        Assert.Equal(typeof(GitLab), project.VersionControl.GetType());
    }

    [Fact]
    public void UseSetFunctionsOfVersionControl_GivenNothing_WhenProjectExistsWithVersionControl_ThenUseSetFunctionsOfProjectsVersionControl()
    {
        // Arrange
        var title = "Project1";
        var description = "Description1";
        var url = "repository.git";
        var message = "New commit repository.git";
        var productOwnerMock = new Mock<ProductOwner>("Klaas", "klaas@mail.com", "Password", new List<NotificationProvider> { NotificationProvider.MAIL });

        var mockTextWriter = new StringWriter();
        Console.SetOut(mockTextWriter);

        var project = new Project(title, description, productOwnerMock.Object, new GitHub());

        // Act
        project.VersionControl.CloneRepo(url);
        project.VersionControl.CommitChanges(message);
        project.VersionControl.PullChanges();
        project.VersionControl.PushChanges();

        var output = mockTextWriter.ToString().Trim();

        // Assert
        Assert.NotNull(project);
        Assert.IsType<Project>(project);
        Assert.Equal(title, project.Title);
        Assert.Equal(description, project.Description);
        Assert.Equal(productOwnerMock.Object, project.ProductOwner);
        Assert.Equal(typeof(GitHub), project.VersionControl.GetType());
        Assert.Contains("Succesfully cloned GitHub repo!", output);
        Assert.Contains("Succesfully committed to GitHub repo!", output);
        Assert.Contains("Succesfully pulled changes from GitHub repo!", output);
        Assert.Contains("Succesfully pushed changed to GitHub repo!", output);
        Assert.Contains(url, output);
        Assert.Contains(message, output);
    }
}