using Domain.Entities;
using Infrastructure.Libraries.VersionControls;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.MapGet("/", () =>
{
    var user = new ProductOwner("John Doe", "johndoe@gmail.com", "Password");
    var project = new Project("Project1", "Description1", user, new GitHub(), user);
    project.VersionControl.CloneRepo("https://github.com/CKleijn/SOFA3-DevOps.git");

    return project.ToString();
});

app.Run();