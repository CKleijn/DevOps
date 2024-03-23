using Domain.States.BacklogItem;
using Thread = Domain.Entities.Thread;

namespace Domain.Tests;

public class ThreadMessageTests
{
    [Fact]
    public void CreateThreadMessage_TitleBodyBacklogItem_WhenNoPreConditions_ThenCreateThreadMessage()
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
        
        Thread thread = new("Thread", "Description", item);
        
        //Act
        ThreadMessage message = new("Title", "Body", thread);
        
        thread.AddThreadMessage(message);
        
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
        ISprintFactory<SprintRelease> factory = new SprintReleaseFactory();
                
        List<NotificationProvider> notificationProviders = new();
        notificationProviders.Add(NotificationProvider.MAIL);
        
        Developer scrumMaster = new("name", "email", "password", notificationProviders);
        IVersionControlStrategy versionControl = new GitHub();
        ProductOwner productOwner = new("name", "email", "password", notificationProviders);
                
        Project project = new("Project", "Description", productOwner, versionControl);
        SprintRelease sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
        
        Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
        
        Thread thread = new("Thread", "Description", item);
        
        string initialTitle = "Title";
        string newTitle = "newTitle";
        
        string initialBody = "Description";
        string newBody = "newDescription";
        
        //Act
        item.CurrentStatus = new ClosedState(item);

        ThreadMessage message = new(initialTitle, initialBody, thread);
        message.Title = newTitle;
        message.Body = newBody;
        
        thread.AddThreadMessage(message);
        
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
        ISprintFactory<SprintRelease> factory = new SprintReleaseFactory();
                
        List<NotificationProvider> notificationProviders = new();
        notificationProviders.Add(NotificationProvider.MAIL);
        
        Developer scrumMaster = new("name", "email", "password", notificationProviders);
        IVersionControlStrategy versionControl = new GitHub();
        ProductOwner productOwner = new("name", "email", "password", notificationProviders);
                
        Project project = new("Project", "Description", productOwner, versionControl);
        SprintRelease sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
        
        Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
        
        Thread thread = new("Thread", "Description", item);
        
        string initialTitle = "Title";
        string newTitle = "newTitle";
        
        string initialBody = "Description";
        string newBody = "newDescription";
        
        //Act
        ThreadMessage message = new(initialTitle, initialBody, thread);
        message.Title = newTitle;
        message.Body = newBody;
        
        thread.AddThreadMessage(message);
        thread.RemoveThreadMessage(message);
        thread.AddThreadMessage(message);
        
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
        ISprintFactory<SprintRelease> factory = new SprintReleaseFactory();
                
        List<NotificationProvider> notificationProviders = new();
        notificationProviders.Add(NotificationProvider.MAIL);
        
        Developer scrumMaster = new("name", "email", "password", notificationProviders);
        IVersionControlStrategy versionControl = new GitHub();
        ProductOwner productOwner = new("name", "email", "password", notificationProviders);
                
        Project project = new("Project", "Description", productOwner, versionControl);
        SprintRelease sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
        
        Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
        
        Thread thread = new("Thread", "Description", item);
        
        string initialTitle = "Title";
        string newTitle = "newTitle";
        
        string initialBody = "Description";
        string newBody = "newDescription";
        
        //Act
        ThreadMessage message = new(initialTitle, initialBody, thread);

        thread.AddThreadMessage(message);
        
        item.CurrentStatus = new ClosedState(item);
        
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