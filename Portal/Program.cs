using Domain.Actions;
using Domain.Entities;
using Domain.Enums;
using Domain.Factories;
using Domain.Interfaces.Factories;
using Infrastructure.Adapters.Notification;
using Infrastructure.Libraries.VersionControls;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.MapGet("/", () =>
{
    List<NotificationProvider> notificationProviders = new();
    notificationProviders.Add(NotificationProvider.MAIL);

    var productOwner = new ProductOwner("John Doe", "johndoe@gmail.com", "Password1", notificationProviders);
    var developer1 = new Developer("Kevin", "kevin@test.com", "Password2", notificationProviders);
    var developer2 = new Developer("Nick", "nick@test.com", "Password3", notificationProviders);
    var developer3 = new Developer("Don", "don@test.com", "Password4", notificationProviders);
    var developer4 = new Developer("Klaas", "klaas@test.com", "Password5", notificationProviders);
    var developer5 = new Developer("Dave", "dave@test.com", "Password6", notificationProviders);

    var project = new Project("Project1", "Description1", productOwner, new GitHub());
    project.VersionControl.CloneRepo("SOFA3-DevOps.git");
    project.VersionControl.PullChanges();
    project.VersionControl.CommitChanges("Added new functionality");
    project.VersionControl.PushChanges();

    var item1 = new Item("Item1", "Description1", developer1, 5);
    var item2 = new Item("Item2", "Description2", developer2, 3);

    project.Backlog.AddItemToBacklog(item1);
    project.Backlog.AddItemToBacklog(item2);

    SprintReleaseFactory sprintReleaseFactory = new SprintReleaseFactory();

    SprintRelease releaseSprint = sprintReleaseFactory.CreateSprint("Release Sprint", DateTime.Now.AddDays(-3), DateTime.Now.AddDays(4), developer3, project);

    var pipeline = new ReleasePipeline("Pipeline", releaseSprint);
    releaseSprint.Pipeline = pipeline;

    releaseSprint.AddDeveloper(developer1);
    releaseSprint.AddDeveloper(developer2);
    releaseSprint.AddDeveloper(developer3);
    releaseSprint.AddTester(developer4);
    releaseSprint.AddTester(developer5);

    releaseSprint.ExecuteSprint();

    item1.SprintBacklog = releaseSprint.SprintBacklog;
    item2.SprintBacklog = releaseSprint.SprintBacklog;

    releaseSprint.SprintBacklog.AddItemToBacklog(item1);
    releaseSprint.SprintBacklog.AddItemToBacklog(item2);

    var activity1 = new Activity("Activity 1", item1);
    item1.AddActivityToItem(activity1);

    var activity2 = new Activity("Activity 2", item1);
    item1.AddActivityToItem(activity2);

    var thread1 = new Domain.Entities.Thread("Thread 1", "Description 1", item1);
    item1.AddThreadToItem(thread1);

    var threadMessage1 = new ThreadMessage("ThreadMessage 1", "Body 1", thread1);
    thread1.AddThreadMessage(threadMessage1);

    Console.WriteLine(pipeline.Print());
    Console.WriteLine(pipeline.PrintSelectedActions());

    pipeline.AddAction(new GitCloneAction());
    pipeline.AddAction(new NpmInstallAction());
    pipeline.AddAction(new NpmRunCopyFilesAction());
    pipeline.AddAction(new NpmEslintAction());
    pipeline.AddAction(new NpmBuildAction());
    pipeline.AddAction(new NpmTestAction());
    pipeline.AddAction(new DotnetAnalyzeAction());
    pipeline.AddAction(new NpmPublishAction());

    Console.WriteLine(pipeline.PrintSelectedActions());

    pipeline.ExecutePipeline();
});

app.Run();