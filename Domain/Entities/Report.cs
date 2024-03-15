using System.Text;
using Domain.Enums;

namespace Domain.Entities;

public class Report
{
    private Guid _id { get; init; }
    private Guid Id { get => _id; init => _id = value; }
    
    private Guid _sprintId { get; init; }
    private Guid SprintId { get => _sprintId; init => _sprintId = value; }
    
    private string _title { get; set; }
    public string Title { get => _title; set => _title = value; }
    
    private string _filePath { get; set; }
    public string FilePath { get => _filePath; set => _filePath = value; }
    
    private ReportExtension _extension { get; set; }
    public ReportExtension Extension { get => _extension; set => _extension = value; }
    
    private User _createdBy { get; set; }
    public User CreatedBy { get => _createdBy; set => _createdBy = value; }

    private DateTime? _updatedAt { get; set; }
    public DateTime? UpdatedAt { get => _updatedAt; set => _updatedAt = value; }
    
    private DateTime _createdAt { get; init; }
    public DateTime CreatedAt { get => _createdAt; init => _createdAt = value;}
    
    public Report(string title, Guid sprintId, string filePath, User creator, ReportExtension extension)
    {
        Id = Guid.NewGuid();
        Title = title;
        SprintId = sprintId;
        FilePath = filePath;
        CreatedBy = creator;
        Extension = extension;
        CreatedAt = DateTime.Now;
    }
    
    //TODO: implement functions (Update observers, etc)

    public override string ToString()
    {
        StringBuilder sb = new();
        
        sb.AppendLine($"Id: {Id}");
        sb.AppendLine($"Title: {Title}");
        sb.AppendLine($"SprintId: {SprintId}");
        sb.AppendLine($"FilePath: {FilePath}");
        sb.AppendLine($"Creator: {CreatedBy}");
        sb.AppendLine($"Extension: {Extension}");
        sb.AppendLine($"UpdatedAt: {UpdatedAt}");
        sb.AppendLine($"CreatedAt: {CreatedAt}");
        
        return sb.ToString();
    }
}