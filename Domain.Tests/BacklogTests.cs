namespace Domain.Tests;

public class BacklogTests
{
         [Fact]
     public void CreateBacklog_GivenNoProperties_WhenNoPreConditions_ThenCreateBacklog()
     {
         //Arrange
         ISprintFactory<SprintRelease> factory = new SprintReleaseFactory();
            
         List<NotificationProvider> notificationProviders = new();
         notificationProviders.Add(NotificationProvider.MAIL);
         
         Developer scrumMaster = new("name", "email", "password", notificationProviders);
         ProductOwner productOwner = new("name", "email", "password", notificationProviders);
                 
         Project project = new("Project", "Description", productOwner, new GitHub());
         
         SprintRelease sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
         
         // Act
         Backlog backlog = new SprintBacklog(sprint);
         

         // Assert
         Assert.NotNull(backlog);
         Assert.IsType<SprintBacklog>(backlog);
     }

     [Fact]
     public void UpdateBacklog_GivenBacklogItem_WhenNoPreConditions_ThenUpdateBacklog()
     {
         // Arrange
         ISprintFactory<SprintRelease> factory = new SprintReleaseFactory();

         List<NotificationProvider> notificationProviders = new();
         notificationProviders.Add(NotificationProvider.MAIL);

         Developer scrumMaster = new("name", "email", "password", notificationProviders);
         ProductOwner productOwner = new("name", "email", "password", notificationProviders);

         Project project = new("Project", "Description", productOwner, new GitHub());

         SprintRelease sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);

         Item initialItem = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
         Item newItem = new("newItem", "Description", scrumMaster, 1, sprint.SprintBacklog);

         // Act
         Backlog backlog = new SprintBacklog(sprint);
    
         backlog.AddItemToBacklog(initialItem);
         backlog.RemoveItemFromBacklog(initialItem);
         backlog.AddItemToBacklog(newItem);


         // Assert
         Assert.NotNull(backlog);
         Assert.Equal(1, backlog.Items.Count);
         Assert.Equal(newItem, backlog.Items[0]);
         Assert.NotEqual(initialItem, backlog.Items[0]);
     }
}