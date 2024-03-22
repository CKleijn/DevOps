namespace Domain.Tests;

public class ProjectTests
{
     [Fact]
     public void CreateProject_GivenTitleDescriptionPasswordScrumMaster_WhenNoPreConditions_ThenCreateProject()
     {
         // Arrange
         List<NotificationProvider> notificationProviders = new();
         notificationProviders.Add(NotificationProvider.MAIL);
         ProductOwner productOwner = new("name", "email", "password", notificationProviders);

         string projectTitle = "Project";
         string projectDescription = "Description";
         
         IVersionControlStrategy versionControl = new GitHub();

         // Act
         Project project = new(projectTitle, projectDescription, productOwner, versionControl);

         // Assert
         Assert.NotNull(project);
         Assert.IsType<Project>(project);
         Assert.NotNull(project.Backlog);
         Assert.NotNull(project.VersionControl);
         Assert.Equal(project.Title, projectTitle);
         Assert.Equal(project.Description, projectDescription);
         Assert.Equal(project.ProductOwner, productOwner);
         Assert.Equal(project.VersionControl, versionControl);
     }
     
     [Fact]
     public void UpdateProject_GivenTitleDescriptionPasswordScrumMasterPipeline_WhenNoPreConditions_ThenUpdateProject()
     {
         // Arrange
         List<NotificationProvider> notificationProviders = new();
         notificationProviders.Add(NotificationProvider.MAIL);
         
         ProductOwner productOwner = new("name", "email", "password", notificationProviders);
         ProductOwner newProductOwner = new("newName", "newEmail", "newPassword", notificationProviders);

         string projectTitle = "Project";
         string newProjectTitle = "NewProject";
         
         
         string projectDescription = "Description";
         string newProjectDescription = "NewDescription";
         
         IVersionControlStrategy versionControl = new GitHub();
         IVersionControlStrategy newVersionControl = new GitLab();
         
         Project project = new(projectTitle, projectDescription, productOwner, versionControl);
             
         Developer scrumMaster = new("name", "email", "password", notificationProviders);
         
         ISprintFactory<SprintRelease> sprintFactory = new SprintReleaseFactory();
         SprintRelease sprint = sprintFactory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(14), scrumMaster, project);
         
         TestPipeline pipeline = new("Pipeline", sprint);
         ReleasePipeline newPipeline = new("Pipeline", sprint);

         // Act
         project.Title = newProjectTitle;
         project.Description = newProjectDescription;
         project.ProductOwner = newProductOwner;
         project.VersionControl = newVersionControl;
         
         project.AddPipeline(pipeline);
         project.RemovePipeline(pipeline);
         project.AddPipeline(newPipeline);

         // Assert
         Assert.NotNull(project);
         Assert.NotNull(project.Backlog);
         Assert.NotNull(project.VersionControl);
         Assert.Equal(newProjectTitle, project.Title);
         Assert.Equal(newProjectDescription, project.Description);
         Assert.Equal(newProductOwner, project.ProductOwner);
         Assert.Equal(newVersionControl, project.VersionControl);
         Assert.Equal(newPipeline, project.Pipelines![0]);
         Assert.Equal(1, project.Pipelines.Count);
     }
}