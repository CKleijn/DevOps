using Domain.Enums;
using Domain.Interfaces.Strategies;
using Infrastructure.Libraries.VersionControls;

namespace Domain.Tests;

public class ProjectTests
{
     [Fact]
     public void CreateProject_GivenTitleDescriptionPasswordScrumMaster_WhenNoPreConditions_ThenInstantiateProject()
     {
         // Arrange
         List<NotificationProvider> notificationProviders = new();
         notificationProviders.Add(NotificationProvider.MAIL);
         ProductOwner productOwner = new("name", "email", "password", notificationProviders);

         string projectTitle = "Project";
         string projectDescription = "Description";
         
         IVersionControlStrategy gitHub = new GitHub();

         // Act
         Project project = new(projectTitle, projectDescription, productOwner, gitHub);

         // Assert
         Assert.NotNull(project);
         Assert.IsType<Project>(project);
         Assert.Equal(project.Title, projectTitle);
         Assert.Equal(project.Description, projectDescription);
         Assert.Equal(project.ProductOwner, productOwner);
         Assert.Equal(project.VersionControl, gitHub);
         Assert.IsType<ProjectBacklog>(project.Backlog);
         Assert.IsType<ProductOwner>(project.ProductOwner);
     }
     
     [Fact]
     public void UpdateProject_GivenProperties_WhenNoPreConditions_ThenInstantiateProject()
     {
         // Arrange
         List<NotificationProvider> notificationProviders = new();
         notificationProviders.Add(NotificationProvider.MAIL);
         ProductOwner productOwner = new("name", "email", "password", notificationProviders);

         string projectTitle = "Project";
         string projectDescription = "Description";
             
         IVersionControlStrategy gitHub = new GitHub();

         // Act
         Project project = new(projectTitle, projectDescription, productOwner, gitHub);

         // Assert
         Assert.NotNull(project);
         Assert.IsType<Project>(project);
         Assert.Equal(project.Title, projectTitle);
         Assert.Equal(project.Description, projectDescription);
         Assert.Equal(project.ProductOwner, productOwner);
         Assert.Equal(project.VersionControl, gitHub);
         Assert.IsType<ProjectBacklog>(project.Backlog);
         Assert.IsType<ProductOwner>(project.ProductOwner);
     }
}