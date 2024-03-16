using System.Text;

namespace Domain.Entities;

public class SprintRelease : Sprint
{
    private IList<Release> _releases { get; init; }
    public IList<Release> Releases { get => _releases; init => _releases = value; }
    
    //TODO: implement functions

    public SprintRelease(string title, DateTime startDate, DateTime endDate, User createdBy, User scrumMaster) : base(title, startDate, endDate, createdBy, scrumMaster)
    {
        _releases = new List<Release>();   
        Console.WriteLine("Sprint with release type initialized.");
    }
    
    public override void Execute()
    {
        Console.WriteLine($"Execute Sprint ({Title}) with release type");
    }

    
    public void AddRelease(Release release)
    {
        _releases.Add(release);
    }
    
    public void RemoveRelease(Release release)
    {
        _releases.Remove(release);
    }
    
    public override string ToString()
    {
        StringBuilder sb = new();
        
        sb.AppendLine($"Sprint Release: {Title}");
        sb.AppendLine($"Start Date: {StartDate}");
        sb.AppendLine($"End Date: {EndDate}");
        sb.AppendLine($"Created By: {CreatedBy}");
        sb.AppendLine($"Scrum Master: {ScrumMaster}");
        sb.AppendLine($"Status: {Status.GetType()}");
        sb.AppendLine($"Releases: {Releases.Count}");
        
        return sb.ToString();
    }
}