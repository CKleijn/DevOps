using Domain.Helpers;
using System.Text;

namespace Domain.Entities;

public class Release
{
    private Guid _id { get; init; }
    public Guid Id { get => _id; init => _id = value; }
    
    private string _tag { get; set; }
    public string Tag { get => _tag; set => _tag = value; }
    
    public Release(string tag)
    {
        _id = Guid.NewGuid();
        _tag = tag;

        Logger.DisplayCreatedAlert(nameof(Release), tag);
    }
    
    public override string ToString()
    {
        StringBuilder sb = new();
        
        sb.AppendLine($"Id: {_id}");
        sb.AppendLine($"Tag: {_tag}");

        return sb.ToString();
    }
}