namespace Domain.Entities;

public class SprintRelease : Sprint
{
    private IList<Release> _releases { get; init; }
    public IList<Release> Releases { get => _releases; init => _releases = value; }
    
    //TODO: implement functions

    public SprintRelease(string title, DateTime startDate, DateTime endDate, User createdBy, User scrumMaster) : base(title, startDate, endDate, createdBy, scrumMaster)
    {
        _releases = new List<Release>();   
    }
    
    public override void Execute()
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }
}