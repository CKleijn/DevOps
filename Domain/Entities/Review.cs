using System.Text;

namespace Domain.Entities;

public class Review
{
    private Guid _id { get; init; }
    public Guid Id { get => _id; init => _id = value; }
    
    private Guid _sprintId { get; init; }
    public Guid SprintId { get => _sprintId; init => _sprintId = value; }
    
    private Guid _pipelineId { get; init; }
    public Guid PipelineId { get => _pipelineId; init => _pipelineId = value; }
    
    private string _title { get; set; }
    public string Title { get => _title; set => _title = value; }
    
    private string _filePath { get; set; }
    public string FilePath { get => _filePath; set => _filePath = value; }
    
    private User _createdBy { get; set; }
    public User CreatedBy { get => _createdBy; set => _createdBy = value; }

    private DateTime? _updatedAt { get; set; }
    public DateTime? UpdatedAt { get => _updatedAt; set => _updatedAt = value; }
    
    private DateTime _createdAt { get; init; }
    public DateTime CreatedAt { get => _createdAt; init => _createdAt = value;}
    
    public Review(string title, Guid sprintId, Guid pipelineId, string filePath, User creator)
    {
        _id = Guid.NewGuid();
        _title = title;
        _sprintId = sprintId;
        _pipelineId = pipelineId;
        _filePath = filePath;
        _createdBy = creator;
        _createdAt = DateTime.Now;
    }
    
    //TODO: implement further functions
    
    public override string ToString()
    {
        StringBuilder sb = new();
        
        sb.AppendLine($"Id: {Id}");
        sb.AppendLine($"Title: {Title}");
        sb.AppendLine($"SprintId: {SprintId}");
        sb.AppendLine($"PipelineId: {PipelineId}");
        sb.AppendLine($"FilePath: {FilePath}");
        sb.AppendLine($"Creator: {CreatedBy}");
        sb.AppendLine($"UpdatedAt: {UpdatedAt}");
        sb.AppendLine($"CreatedAt: {CreatedAt}");
        
        return sb.ToString();
    }
}