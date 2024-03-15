using System.Text;
using Domain.Enums;

namespace Domain.Entities;

public class User
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