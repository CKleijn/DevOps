using Domain.Enums;
using Domain.Helpers;
using System.Text;

namespace Domain.Entities;

public class ReportElement
{
    private Guid _id { get; init; }
    public Guid Id { get => _id; init => _id = value; }
    
    private string _companyName { get; set; }
    public string CompanyName { get => _companyName; set => _companyName = value; }
    
    private byte[] _logo { get; set; }
    public byte[] Logo { get => _logo; set => _logo = value; }
    
    private string _projectName { get; set; }
    public string ProjectName { get => _projectName; set => _projectName = value; }
    
    private int _versionNumber { get; set; }
    public int VersionNumber { get => _versionNumber; set => _versionNumber = value; }
    
    private DateTime _date { get; set; }
    public DateTime Date { get => _date; set => _date = value; }
    
    private ReportElementType _type { get; set; }
    public ReportElementType Type { get => _type; set => _type = value; }
    
    public ReportElement(string companyName, byte[] logo, string projectName, int versionNumber, DateTime date, ReportElementType type)
    {
        _id = Guid.NewGuid();
        _companyName = companyName;
        _logo = logo;
        _projectName = projectName;
        _versionNumber = versionNumber;
        _date = date;
        _type = type;

        if (_type == ReportElementType.HEADER)
        {
            Logger.DisplayCreatedAlert(nameof(ReportElement), $"Header for {_projectName}");
        }
        else
        {
            Logger.DisplayCreatedAlert(nameof(ReportElement), $"Footer for {_projectName}");
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new();

        sb.AppendLine(_versionNumber.ToString());
        sb.AppendLine(_date.ToString("dd/mm/yyyy"));
        sb.AppendLine(_companyName);
        sb.AppendLine(_projectName);

        return sb.ToString();
    }
}