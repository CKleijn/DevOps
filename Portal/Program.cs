using Domain.Actions;
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
    Console.ForegroundColor = ConsoleColor.Green;

    var productOwner = new ProductOwner("John Doe", "johndoe@gmail.com", "Password", [NotificationProvider.MAIL, NotificationProvider.SLACK]);
    var project = new Project("Project1", "Description1", productOwner, new GitHub());
    project.VersionControl.CloneRepo("https://github.com/CKleijn/SOFA3-DevOps.git");

    var developer = new Developer("Kevin", "kevin@test.com", "Password", [NotificationProvider.TEAMS, NotificationProvider.SLACK]);

    //var activity = new Activity("Activity 1", new Item("Test", "Test", developer, developer, 5), developer);

    //return activity.ToString();

    // Factory test

    ISprintFactory<SprintRelease> sprintReleaseFactory = new SprintReleaseFactory();
    ISprintFactory<SprintReview> sprintReviewFactory = new SprintReviewFactory();


    SprintRelease releaseSprint = sprintReleaseFactory.CreateSprint("Release Sprint", DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1), developer, project);
    //SprintReview reviewSprint = sprintReviewFactory.CreateSprint("Review Sprint", DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1), developer, project);

    // reviewSprint.AddReview(new Review("test", Guid.NewGuid(), Guid.NewGuid(), "a", developer));

    //reviewSprint.Title = "New title";

    //Console.WriteLine(reviewSprint.Title);

    //return reviewSprint.ToString();

    releaseSprint.Register(productOwner);
    releaseSprint.Register(developer);
    
    var pipeline = new TestPipeline("Pipeline", releaseSprint);

    pipeline.Print();

    Console.WriteLine("");

    pipeline.AddAction(new GitCloneAction());
    pipeline.AddAction(new NpmInstallAction());
    pipeline.AddAction(new NpmRunCopyFilesAction());
    pipeline.AddAction(new NpmEslintAction());
    pipeline.AddAction(new NpmBuildAction());
    pipeline.AddAction(new NpmTestAction());
    pipeline.AddAction(new DotnetAnalyzeAction());
    pipeline.AddAction(new NpmPublishAction());
    Console.WriteLine("");
    pipeline.Print();
    Console.WriteLine("");
    pipeline.ExecutePipeline();
    //pipeline.RerunPipeline();
});

app.Run();