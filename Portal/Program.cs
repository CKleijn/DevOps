using System.Reflection;
using Domain.Entities;
using Domain.Enums;
using DomainServices.Factories;
using DomainServices.Interfaces;
using Infrastructure.Adapters.Notification;
using Infrastructure.Libraries.VersionControls;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.MapGet("/", () =>
{
    //***
    //Keep this line so AssemblyScanner can find assemblies within Infrastructure
    Assembly.Load("Infrastructure");
    //***
    
    //var productOwner = new ProductOwner("John Doe", "johndoe@gmail.com", "Password");
    //var project = new Project("Project1", "Description1", productOwner, new GitHub(), productOwner);
    //project.VersionControl.CloneRepo("https://github.com/CKleijn/SOFA3-DevOps.git");

    //var developer = new Developer("Kevin", "kevin@test.com", "Password");

    //var activity = new Activity("Activity 1", new Item("Test", "Test", developer, developer, 5), developer);

    //return activity.ToString();
    
    //Factory test
    //var developer = new Developer("Kevin", "kevin@test.com", "Password");
    
    //ISprintFactory<SprintRelease> sprintReleaseFactory = new SprintReleaseFactory();
    //ISprintFactory<SprintReview> sprintReviewFactory = new SprintReviewFactory();
    
    
    //SprintRelease releaseSprint = sprintReleaseFactory.CreateSprint("Release Sprint", DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1), developer, developer);
    //SprintReview reviewSprint = sprintReviewFactory.CreateSprint("Review Sprint", DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1), developer, developer);
    
    //reviewSprint.AddReview(new Review("test", Guid.NewGuid(), Guid.NewGuid(), "a", developer));
    
    //reviewSprint.Title = "New title";
    
    //Console.WriteLine(reviewSprint.Title);

    //var dummy = new MailAdapter();
    
    var recipient1 = new Developer("recipient1", "recipient1@test.com", "Password");
    var recipient2 = new Developer("recipient2", "recipient2@test.com", "Password");
    var recipient3 = new Developer("recipient3", "recipient3@test.com", "Password");

    var sender = new ProductOwner("Mike", "mikevandercaaij@hotmail.com", "Geheim");
    
    var notification = new Notification("Test", "Test", sender);
    
    notification.AddTargetUser(recipient1);
    notification.AddTargetUser(recipient2);
    notification.AddTargetUser(recipient3);
    
    notification.AddDestinationType(NotificationProvider.MAIL);
    notification.AddDestinationType(NotificationProvider.SLACK);
    notification.AddDestinationType(NotificationProvider.TEAMS);
    
    notification.RemoveDestinationType(NotificationProvider.SLACK);
    notification.RemoveTargetUser(recipient2);
    
    notification.SendNotification();

    //return reviewSprint.ToString();
});

app.Run();