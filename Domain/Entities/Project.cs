using Domain.Helpers;
using Domain.Interfaces.Strategies;
using System.Text;
using Domain.Interfaces.Observer;

namespace Domain.Entities;

public class Project
{
    private Guid _id { get; init; }
    public Guid Id { get => _id; init => _id = value; }
    
    private string _title { get; set; }
    public string Title { get => _title; set => _title = value; }
    
    private string _description { get; set; }
    public string Description { get => _description; set => _description = value; }
    
    private ProductOwner _productOwner { get; set; }
    public ProductOwner ProductOwner { get => _productOwner; set => _productOwner = value;}

    private ProjectBacklog _backlog { get; init; }
    public ProjectBacklog Backlog { get => _backlog; init => _backlog = value; }

    private IVersionControlStrategy _versionControl { get; set; }
    public IVersionControlStrategy VersionControl { get => _versionControl; set => _versionControl = value; }

    private IList<Pipeline>? _pipelines { get; set; }
    public IList<Pipeline>? Pipelines { get => _pipelines; set => _pipelines = value; }
    
    public Project(string title, string description, ProductOwner productOwner, IVersionControlStrategy versionControl)
    {
        _id = Guid.NewGuid();
        _title = title;
        _description = description;
        _productOwner = productOwner;
        _backlog = new ProjectBacklog();
        _versionControl = versionControl;
        _pipelines = new List<Pipeline>();

        Logger.DisplayCreatedAlert(nameof(Project), _title);
    }

    public void AddPipeline(Pipeline pipeline)
    {
        _pipelines!.Add(pipeline);
    }

    public void RemovePipeline(Pipeline pipeline)
    {
        _pipelines!.Remove(pipeline);
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        
        sb.AppendLine($"Id: {_id}");
        sb.AppendLine($"Title: {_title}");
        sb.AppendLine($"Description: {_description}");
        sb.AppendLine($"ProductOwner: {_productOwner.ToString()}");
        sb.AppendLine($"Backlog: {_backlog.ToString()}");
        sb.AppendLine($"VersionControl: {_versionControl.GetType().Name}");
        sb.AppendLine($"Pipelines: {_pipelines?.Count ?? 0}");

        return sb.ToString();
    }
}