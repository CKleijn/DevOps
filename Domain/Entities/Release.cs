using System.Text;

namespace Domain.Entities;

public class Release
{
    private Guid _id { get; init; }
    private Guid Id { get => _id; init => _id = value; }
    
    private Guid _sprintId { get; init; }
    private Guid SprintId { get => _sprintId; init => _sprintId = value; }
    
    private Guid _pipelineId { get; init; }
    private Guid PinelineId { get => _pipelineId; init => _pipelineId = value; }
    
    private string _tag { get; set; }
    public string Tag { get => _tag; set => _tag = value; }
    
    private User _createdBy { get; set; }
    public User CreatedBy { get => _createdBy; set => _createdBy = value; }

    private DateTime? _updatedAt { get; set; }
    public DateTime? UpdatedAt { get => _updatedAt; set => _updatedAt = value; }
    
    private DateTime _createdAt { get; init; }
    public DateTime CreatedAt { get => _createdAt; init => _createdAt = value;}
    
    public Release(Guid sprintId, Guid pipelineId, string tag, User creator)
    {
        Id = Guid.NewGuid();
        SprintId = sprintId;
        PinelineId = pipelineId;
        Tag = tag;
        CreatedBy = creator;
        CreatedAt = DateTime.Now;
    }
    
    //TODO: implement further functions

    public override string ToString()
    {
        StringBuilder sb = new();
        
        sb.AppendLine($"Id: {Id}");
        sb.AppendLine($"SprintId: {SprintId}");
        sb.AppendLine($"PipelineId: {PinelineId}");
        sb.AppendLine($"Tag: {Tag}");
        sb.AppendLine($"Creator: {CreatedBy}");
        sb.AppendLine($"UpdatedAt: {UpdatedAt}");
        sb.AppendLine($"CreatedAt: {CreatedAt}");

        return sb.ToString();
    }
}