using System.Text;
using Domain.Enums;

namespace Domain.Entities;

public class User : IObserver<Project>
{
    private Guid _id { get; init; }
    private Guid Id { get => _id; init => _id = value; }
    
    private string _name { get; set; }
    public string Name { get => _name; set => _name = value; }
    
    private string _email { get; set; }
    public string Email { get => _email; set => _email = value; }
    
    private string _password { get; set; }
    public string Password { get => _password; set => _password = value; }
    
    private Role _role { get; set; }
    public Role Role { get => _role; set => _role = value; }

    private DateTime? _updatedAt { get; set; }
    public DateTime? UpdatedAt { get => _updatedAt; set => _updatedAt = value; }
    
    private DateTime _createdAt { get; init; }
    public DateTime CreatedAt { get => _createdAt; init => _createdAt = value;}
    
    private IDisposable _unsubscriber { get; set; }

    public User(string name, string email, string password, Role role)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Password = password;
        Role = role;
        CreatedAt = DateTime.Now;
    }
    
    //TODO: implement functions (Update observers, etc)
    
    public void Subscribe(IObservable<Project> provider)
    {
        if (provider != null)
            _unsubscriber = provider.Subscribe(this);
    }
    
    public void OnCompleted()
    {
        Console.WriteLine("All data has been transmitted.");
        Unsubscribe();
    }

    public void OnError(Exception error)
    {
        Console.WriteLine("An error has occurred transmitting the project: {0}", error.Message);
    }

    public void OnNext(Project value)
    {
        Console.WriteLine("New Project data: {0}", value);
    }
    
    public void Unsubscribe()
    {
        _unsubscriber.Dispose();
    }
    
    public override string ToString()
    {
        StringBuilder sb = new();
        
        sb.AppendLine($"Id: {Id}");
        sb.AppendLine($"Name: {Name}");
        sb.AppendLine($"Email: {Email}");
        sb.AppendLine($"Role: {Role}");
        sb.AppendLine($"UpdatedAt: {UpdatedAt}");
        sb.AppendLine($"CreatedAt: {CreatedAt}");
        
        return sb.ToString();
    }
}