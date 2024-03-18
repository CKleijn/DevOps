using System.Text;
using System.Xml.Linq;
using Domain.Enums;
using Domain.Helpers;

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
    
    public Report(string title, Guid sprintId, string filePath, ReportExtension extension)
    {
        _id = Guid.NewGuid();
        _title = title;
        _sprintId = sprintId;
        _filePath = filePath;
        _extension = extension;

        Logger.DisplayCreatedAlert(nameof(Report), _title);
    }
    
    //TODO: implement functions (Update observers, etc)

    public override string ToString()
    {
        StringBuilder sb = new();
        
        sb.AppendLine($"Id: {_id}");
        sb.AppendLine($"Title: {_title}");
        sb.AppendLine($"SprintId: {_sprintId}");
        sb.AppendLine($"FilePath: {_filePath}");
        sb.AppendLine($"Extension: {_extension}");
        
        return sb.ToString();
    }
}