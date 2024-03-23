using Domain.States.BacklogItem;
using Moq;
using Thread = Domain.Entities.Thread;

namespace Domain.Tests;

public class ThreadTests
{
    [Fact]
    public void CreateThread_TitleBodyBacklogItem_WhenNoPreConditions_ThenCreateThread()
    {
        //Arrange
        ISprintFactory<SprintRelease> factory = new SprintReleaseFactory();
                
        List<NotificationProvider> notificationProviders = new();
        notificationProviders.Add(NotificationProvider.MAIL);
        
        Developer scrumMaster = new("name", "email", "password", notificationProviders);
        IVersionControlStrategy versionControl = new GitHub();
        ProductOwner productOwner = new("name", "email", "password", notificationProviders);
                
        Project project = new("Project", "Description", productOwner, versionControl);
        SprintRelease sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
        
        Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
        
        string initialTitle = "Title";
        string initialDescription = "Description";
        
        //Act
        Thread thread = new(initialTitle, initialDescription, item);
        
        //Assert
        Assert.NotNull(thread);
        Assert.IsType<Thread>(thread);
        Assert.Equal(initialTitle, thread.Subject);
        Assert.Equal(initialDescription, thread.Description);
        Assert.Equal(item, thread.Item);
        Assert.NotNull(thread.Item);
    }
    
    [Fact]
    public void UpdateThread_TitleBodyBacklogItem_WhenNoPreConditions_ThenUpdateThread()
    {
        //Arrange
        ISprintFactory<SprintRelease> factory = new SprintReleaseFactory();
                
        List<NotificationProvider> notificationProviders = new();
        notificationProviders.Add(NotificationProvider.MAIL);
        
        Developer scrumMaster = new("name", "email", "password", notificationProviders);
        IVersionControlStrategy versionControl = new GitHub();
        ProductOwner productOwner = new("name", "email", "password", notificationProviders);
                
        Project project = new("Project", "Description", productOwner, versionControl);
        SprintRelease sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
        
        Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
        
        string initialTitle = "Title";
        string newTitle = "newTitle";
        
        string initialDescription = "Description";
        string newDescription = "newDescription";
        
        Thread thread = new(initialTitle, initialDescription, item);
        
        //Act
        thread.Subject = newTitle;
        thread.Description = newDescription;
        
        //Assert
        Assert.NotNull(thread);
        Assert.IsType<Thread>(thread);
        Assert.Equal(newTitle, thread.Subject);
        Assert.NotEqual(initialTitle, thread.Subject);
        Assert.Equal(newDescription, thread.Description);
        Assert.NotEqual(initialDescription, thread.Description);
        Assert.Equal(item, thread.Item);
        Assert.NotNull(thread.Item);
    }
    
    [Fact]
    public void UpdateThread_TitleBodyBacklogItem_WhenBacklogItemIsClosed_ThenDontUpdateThread()
    {
        //Arrange
        ISprintFactory<SprintRelease> factory = new SprintReleaseFactory();
                
        List<NotificationProvider> notificationProviders = new();
        notificationProviders.Add(NotificationProvider.MAIL);
        
        Developer scrumMaster = new("name", "email", "password", notificationProviders);
        IVersionControlStrategy versionControl = new GitHub();
        ProductOwner productOwner = new("name", "email", "password", notificationProviders);
                
        Project project = new("Project", "Description", productOwner, versionControl);
        SprintRelease sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
        
        Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
        item.CurrentStatus = new ClosedState(item);
        
        string initialTitle = "Title";
        string newTitle = "newTitle";
        
        string initialDescription = "Description";
        string newDescription = "newDescription";
        
        Thread thread = new(initialTitle, initialDescription, item);
        
        //Act
        thread.Subject = newTitle;
        thread.Description = newDescription;
        
        //Assert
        Assert.NotNull(thread);
        Assert.IsType<Thread>(thread);
        Assert.NotEqual(newTitle, thread.Subject);
        Assert.Equal(initialTitle, thread.Subject);
        Assert.NotEqual(newDescription, thread.Description);
        Assert.Equal(initialDescription, thread.Description);
        Assert.Equal(item, thread.Item);
        Assert.NotNull(thread.Item);
    }
}