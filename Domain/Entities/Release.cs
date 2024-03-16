using System.Text;

namespace Domain.Entities;

public class Release
{
    private Guid _id { get; init; }
    public Guid Id { get => _id; init => _id = value; }
    
    private Guid _sprintId { get; init; }
    public Guid SprintId { get => _sprintId; init => _sprintId = value; }
    
    private Guid _pipelineId { get; init; }
    public Guid PipelineId { get => _pipelineId; init => _pipelineId = value; }
    
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
        PipelineId = pipelineId;
        Tag = tag;
        CreatedBy = creator;
        CreatedAt = DateTime.Now;
    }
    
    //TODO: implement further functions

    public override string ToString()
    {
        StringBuilder sb = new();
        
        sb.AppendLine($"Id: {_id}");
        sb.AppendLine($"SprintId: {_sprintId}");
        sb.AppendLine($"PipelineId: {_pipelineId}");
        sb.AppendLine($"Tag: {_tag}");
        sb.AppendLine($"Creator: {_createdBy.ToString()}");
        sb.AppendLine($"UpdatedAt: {_updatedAt}");
        sb.AppendLine($"CreatedAt: {_createdAt}");

        return sb.ToString();
    }
}