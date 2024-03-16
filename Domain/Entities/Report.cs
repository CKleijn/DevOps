using System.Text;
using Domain.Enums;

namespace Domain.Entities;

public class Report
{
    private Guid _id { get; init; }
    public Guid Id { get => _id; init => _id = value; }
    
    private Guid _sprintId { get; init; }
    public Guid SprintId { get => _sprintId; init => _sprintId = value; }
    
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
        _id = Guid.NewGuid();
        _title = title;
        _sprintId = sprintId;
        _filePath = filePath;
        _createdBy = creator;
        _extension = extension;
        _createdAt = DateTime.Now;
    }
    
    //TODO: implement functions (Update observers, etc)

    public override string ToString()
    {
        StringBuilder sb = new();
        
        sb.AppendLine($"Id: {_id}");
        sb.AppendLine($"Title: {_title}");
        sb.AppendLine($"SprintId: {_sprintId}");
        sb.AppendLine($"FilePath: {_filePath}");
        sb.AppendLine($"Creator: {_createdBy.ToString()}");
        sb.AppendLine($"Extension: {_extension}");
        sb.AppendLine($"UpdatedAt: {_updatedAt}");
        sb.AppendLine($"CreatedAt: {_createdAt}");
        
        return sb.ToString();
    }
}