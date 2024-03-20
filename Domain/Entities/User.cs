using System.Text;
using Domain.Interfaces.Observer;

namespace Domain.Entities;

// public abstract class User : IObserver<>
public abstract class User : IObserver
{
    private Guid _id { get; init; }
    public Guid Id { get => _id; init => _id = value; }
    private string _name { get; set; }
    public string Name { get => _name; set => _name = value; }
    
    private string _email { get; set; }
    public string Email { get => _email; set => _email = value; }
    
    private string _password { get; set; }
    public string Password { get => _password; set => _password = value; }

    public User(string name, string email, string password)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Password = password;
    }
    
    public void Update(Notification notification)
    {
        //Set the current target user for the notification
        notification.CurrentTargetUser = this;
        
        //deliver message
        notification.SendNotification();
    }
    
    public override string ToString()
    {
        StringBuilder sb = new();
        
        sb.AppendLine($"Id: {_id}");
        sb.AppendLine($"Name: {_name}");
        sb.AppendLine($"Email: {_email}");
        
        return sb.ToString();
    }
}