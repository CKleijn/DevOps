using System.Text;
using System.Xml.Linq;
using Domain.Enums;
using Domain.Helpers;

namespace Domain.Entities;

public class Report
{
    private Guid _id { get; init; }
    public Guid Id { get => _id; init => _id = value; }
    
    private string _title { get; set; }
    public string Title { get => _title; set => _title = value; }
    
    private string _filePath { get; set; }
    public string FilePath { get => _filePath; set => _filePath = value; }
    
    private IList<ReportElement> _elements { get; init; }
    public IList<ReportElement> Elements { get => _elements; init => _elements = value; }
    
    private ReportExtension _extension { get; set; }
    public ReportExtension Extension { get => _extension; set => _extension = value; }
    
    public Report(string title, string filePath, ReportExtension extension)
    {
        _id = Guid.NewGuid();
        _title = title;
        _filePath = filePath;
        _extension = extension;
        _elements = new List<ReportElement>();

        Logger.DisplayCreatedAlert(nameof(Report), _title);
    }
    
    public void AddElement(ReportElement element)
    {
        _elements.Add(element);
        
        Logger.DisplayUpdatedAlert(nameof(Elements), $"Added element with id: {element.Id}");
    }
    
    public void RemoveElement(ReportElement element)
    {
        _elements.Remove(element);
        
        Logger.DisplayUpdatedAlert(nameof(Elements), $"Removed element with id: {element.Id}");
    }
    
    public override string ToString()
    {
        StringBuilder sb = new();
        
        sb.AppendLine($"Id: {_id}");
        sb.AppendLine($"Title: {_title}");
        sb.AppendLine($"FilePath: {_filePath}");
        sb.AppendLine($"Extension: {_extension}");
        
        return sb.ToString();
    }
}