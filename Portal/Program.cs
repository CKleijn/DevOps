using Domain.Entities;
using DomainServices.Factories;
using DomainServices.Interfaces;
using Infrastructure.Libraries.VersionControls;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.MapGet("/", () =>
{
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
        
    //return reviewSprint.ToString();
});

app.Run();