namespace Domain.Tests;

[Collection("SequentialTest")]
public class UserTests
{
     [Fact]
     public void CreateUser_GivenNameEmailPasswordNotificationProviders_WhenNoPreConditions_ThenCreateUser()
     {
         // Arrange
         List<NotificationProvider> notificationProviders = new();
         notificationProviders.Add(NotificationProvider.MAIL);
         
         //Act
         Developer user = new("Name", "Email", "Password", notificationProviders);

         // Assert
         Assert.NotNull(user);
         Assert.IsType<Developer>(user);
         Assert.NotNull(user.DestinationTypes);
         Assert.Single(user.DestinationTypes);
     }
     
     [Fact]
     public void UpdateUser_GivenNameEmailPasswordNotificationProviders_WhenNoPreConditions_ThenUpdateUser()
     {
         // Arrange
         List<NotificationProvider> notificationProviders = new();
         
         NotificationProvider oldNotificationProvider = NotificationProvider.MAIL;
         NotificationProvider newNotificationProvider = NotificationProvider.SLACK;
         
         notificationProviders.Add(oldNotificationProvider);
         
         string name = "Name";
         string newName = "newName";
         string email = "Email";
         string newEmail = "newEmail";
         string password = "Password";
         string newPassword = "newPassword";
         
         Developer user = new(name, email, password, notificationProviders);

         // Act
         user.Name = newName;
         user.Email = newEmail;
         user.Password = newPassword;
         user.RemoveDestinationType(oldNotificationProvider);
         user.AddDestinationType(newNotificationProvider);

         // Assert
         Assert.NotNull(user);
         Assert.IsType<Developer>(user);
         Assert.Equal(newName, user.Name);
         Assert.NotEqual(name, user.Name);
         Assert.Equal(newEmail, user.Email);
         Assert.NotEqual(email, user.Email);
         Assert.Equal(newPassword, user.Password);
         Assert.NotEqual(password, user.Password);
         Assert.Equal(newNotificationProvider, user.DestinationTypes[0]);
         Assert.Single(user.DestinationTypes);
     }
}