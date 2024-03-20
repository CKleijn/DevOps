using System.Text;
using Domain.Enums;
using Domain.Helpers;

namespace Domain.Entities;

public class Report
{
    private Guid _id { get; init; }
    public Guid Id { get => _id; init => _id = value; }
    
    private string _title { get; set; }
    public string Title { get => _title; set => _title = value; }

    private Sprint _sprint { get; init; }
    public Sprint Sprint { get => _sprint; init => _sprint = value; }

    private ReportElement? _header { get; set; }
    public ReportElement? Header { get => _header; set => _header = value; }

    private ReportElement? _footer { get; set; }
    public ReportElement? Footer { get => _footer; set => _footer = value; }

    private ReportExtension _extension { get; set; }
    public ReportExtension Extension { get => _extension; set => _extension = value; }
    
    public Report(string title, Sprint sprint, ReportExtension extension, ReportElement header, ReportElement footer)
    {
        _id = Guid.NewGuid();
        _title = title;
        _sprint = sprint;
        _extension = extension;
        _header = header;
        _footer = footer;

        Logger.DisplayCreatedAlert(nameof(Report), _title);
    }

    public Report(string title, Sprint sprint, ReportExtension extension, ReportElement header)
    {
        _id = Guid.NewGuid();
        _title = title;
        _sprint = sprint;
        _extension = extension;
        _header = header;

        Logger.DisplayCreatedAlert(nameof(Report), _title);
    }

    public Report(string title, Sprint sprint, ReportExtension extension)
    {
        _id = Guid.NewGuid();
        _title = title;
        _sprint = sprint;
        _extension = extension;

        Logger.DisplayCreatedAlert(nameof(Report), _title);
    }

    public void GenerateReport()
    {
        StringBuilder sb = new();

        if (_header != null)
        {
            sb.AppendLine(_header.ToString());
        }

        sb.AppendLine($"Id: {_id}");
        sb.AppendLine($"Title: {_title}");
        sb.AppendLine($"Extension: {_extension}");
        sb.AppendLine(_sprint.ToString());

        if (_footer != null)
        {
            sb.AppendLine(_footer.ToString());
        }

        Console.WriteLine(sb.ToString());
    }
}