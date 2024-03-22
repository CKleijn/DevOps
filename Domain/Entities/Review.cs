using Domain.Helpers;
using System.Text;

namespace Domain.Entities;

public class Review
{
    private Guid _id { get; init; }
    public Guid Id { get => _id; init => _id = value; }
    
    private string _title { get; set; }
    public string Title { get => _title; set => _title = value; }

    private string _body { get; set; }
    public string Body { get => _body; set => _body = value; }

    public Review(string title, string body)
    {
        _id = Guid.NewGuid();
        _title = title;
        _body = body;

        Logger.DisplayCreatedAlert(nameof(Review), _title);
    }
    
    public override string ToString()
    {
        StringBuilder sb = new();
        
        sb.AppendLine($"Id: {_id}");
        sb.AppendLine($"Title: {_title}");
        sb.AppendLine($"Body: {_body}");

        return sb.ToString();
    }
}