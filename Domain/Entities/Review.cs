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
        
        sb.AppendLine($"Id: {_id}");
        sb.AppendLine($"Title: {_title}");
        sb.AppendLine($"SprintId: {_sprintId}");
        sb.AppendLine($"PipelineId: {_pipelineId}");
        sb.AppendLine($"FilePath: {_filePath}");
        sb.AppendLine($"Creator: {_createdBy.ToString()}");
        sb.AppendLine($"UpdatedAt: {_updatedAt}");
        sb.AppendLine($"CreatedAt: {_createdAt}");
        
        return sb.ToString();
    }
}