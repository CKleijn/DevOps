using System.Text;
using Domain.Helpers;

namespace Domain.Entities;

public class SprintRelease : Sprint
{
    private IList<Release> _releases { get; init; }
    public IList<Release> Releases { get => _releases; init => _releases = value; }
    
    //TODO: implement functions

    public SprintRelease(string title, DateTime startDate, DateTime endDate, User scrumMaster) : base(title, startDate, endDate, scrumMaster)
    {
        _releases = new List<Release>();   
        
        Logger.DisplayCreatedAlert(nameof(SprintRelease), Title);
    }
    
    public override void Execute()
    {
        Logger.DisplayCustomAlert(nameof(SprintRelease), nameof(Execute), $"Execute Sprint ({Title}).");
    }
    
    public void AddRelease(Release release)
    {
        _releases.Add(release);
        Logger.DisplayUpdatedAlert(nameof(Releases), $"Added release with an id of: {release.Id}");
    }
    
    public void RemoveRelease(Release release)
    {
        _releases.Remove(release);
        Logger.DisplayUpdatedAlert(nameof(Releases), $"Removed release with an id of: {release.Id}");
    }
    
    public override string ToString()
    {
        StringBuilder sb = new();
        
        sb.AppendLine($"Sprint Release: {Title}");
        sb.AppendLine($"Start Date: {StartDate}");
        sb.AppendLine($"End Date: {EndDate}");
        sb.AppendLine($"Scrum Master: {ScrumMaster}");
        sb.AppendLine($"Status: {CurrentStatus.GetType()}");
        sb.AppendLine($"Releases: {Releases.Count}");
        
        return sb.ToString();
    }
}