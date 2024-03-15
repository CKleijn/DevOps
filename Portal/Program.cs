using Domain.Entities;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.MapGet("/", () =>
{
    var p = new Project("Teste", "Teste", new User("Teste", "Teste", "Teste", Domain.Enums.Role.PRODUCT_OWNER), new User("Teste", "Teste", "Teste", Domain.Enums.Role.PRODUCT_OWNER));

    p.ToString();

    return p.ToString();
});

app.Run();