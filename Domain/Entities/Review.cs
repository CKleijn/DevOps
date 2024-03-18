using Domain.Helpers;
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
    
    public Review(string title, Guid sprintId, Guid pipelineId, string filePath)
    {
        _id = Guid.NewGuid();
        _title = title;
        _sprintId = sprintId;
        _pipelineId = pipelineId;
        _filePath = filePath;

        Logger.DisplayCreatedAlert(nameof(Review), _title);
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
        
        return sb.ToString();
    }
}