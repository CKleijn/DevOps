using Domain.Helpers;
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
    
    public Release(Guid sprintId, Guid pipelineId, string tag)
    {
        _id = Guid.NewGuid();
        _sprintId = sprintId;
        _pipelineId = pipelineId;
        _tag = tag;

        Logger.DisplayCreatedAlert(nameof(Release), $"Sprint: {_id}");
    }
    
    //TODO: implement further functions

    public override string ToString()
    {
        StringBuilder sb = new();
        
        sb.AppendLine($"Id: {_id}");
        sb.AppendLine($"SprintId: {_sprintId}");
        sb.AppendLine($"PipelineId: {_pipelineId}");
        sb.AppendLine($"Tag: {_tag}");

        return sb.ToString();
    }
}