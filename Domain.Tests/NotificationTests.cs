namespace Domain.Tests;

[Collection("SequentialTest")]
public class NotificationTests
{
     [Fact]
     public void CreateNotification_GivenTitleBody_WhenNoPreConditions_ThenCreateNotification()
     {
         //Arrange
         string title = "Title";
         string body = "Content";
         
         // Act
         Notification notification = new(title, body);

         // Assert
         Assert.NotNull(notification);
         Assert.IsType<Notification>(notification);
         Assert.NotNull(notification.TargetUsers);
         Assert.Equal(title, notification.Title);
         Assert.Equal(body, notification.Body);
     }
     
     [Fact]
     public void UpdateProject_GivenTitleDescriptionPasswordScrumMasterPipeline_WhenNoPreConditions_ThenUpdateNotification()
     {
         //Arrange
         List<NotificationProvider> notificationProviders = new();
         notificationProviders.Add(NotificationProvider.MAIL);
         
         Developer user = new("Name", "Email", "Password", notificationProviders);
         Developer newUser = new("newName", "newEmail", "newPassword", notificationProviders);
         
         string title = "Title";
         string body = "Content";
         
         string newTitle = "newTitle";
         string newBody = "newContent";
         
         Notification notification = new(title, body);

         // Act
         notification.Title = newTitle;
         notification.Body = newBody;
         notification.AddTargetUser(user);
         notification.RemoveTargetUser(user);
         notification.AddTargetUser(newUser);


         // Assert
         Assert.NotNull(notification);
         Assert.Equal(newTitle, notification.Title);
         Assert.NotEqual(title, notification.Title);
         Assert.Equal(newBody, notification.Body);
         Assert.NotEqual(body, notification.Body);
         Assert.Equal(newUser, notification.TargetUsers[0]);
         Assert.Equal(1, notification.TargetUsers.Count);
     }
}