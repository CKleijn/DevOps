using DomainServices.Interfaces;
using DomainServices.States.Sprint;

namespace Domain.Entities;

public abstract class Sprint
{
    private Guid _id { get; init; }
    public  Guid Id { get => _id ; init => _id = value; }

    private string _title { get; set; }
    public string Title { get => _title; set => _title = value; }
    
    private DateTime _startDate { get; set; }
    private DateTime StartDate { get => _startDate; set => _startDate = value; }
    
    private DateTime _endDate { get; set; }
    private DateTime EndDate { get => _startDate; set => _startDate = value; }
    
    private SprintBacklog _sprintBacklog { get; init; }
    public SprintBacklog SprintBacklog { get => _sprintBacklog; init => _sprintBacklog = value; }
    
    private ISprintState _status { get; set; }
    public ISprintState Status { get => _status; set => _status = value; }
    
    private IList<Report> _reports { get; set; }
    public IList<Report> Reports { get => _reports; set => _reports = value; }
    
    private User _scrumMaster { get; set; }
    public User ScrumMaster { get => _scrumMaster; set => _scrumMaster = value; }
    
    //TODO: alter type to "Developer"
    private IList<User> _developers { get; init; }
    public IList<User> Developers { get => _developers; init => _developers = value; }
    
    private User _createdBy { get; set; }
    public User CreatedBy { get => _createdBy; set => _createdBy = value; }
    
    private DateTime? _updatedAt { get; set; }
    public DateTime? UpdatedAt { get => _updatedAt; set => _updatedAt = value; }
    
    private DateTime _createdAt { get; init; }
    public DateTime CreatedAt { get => _createdAt; init => _createdAt = value; }
    
    public Sprint(string title, DateTime startDate, DateTime endDate, User createdBy, User scrumMaster)
    {
        _id = Guid.NewGuid();
        _title = title;
        _startDate = startDate;
        _endDate = endDate;
        _createdBy = createdBy;
        _scrumMaster = scrumMaster;
        
        //TODO: alter type to "Developer"
        _developers = new List<User>();
        
        _status = new InitialState();
        _reports = new List<Report>();
        _sprintBacklog = new SprintBacklog();
        _createdAt = DateTime.Now;
    }
    
    //TODO: add state functions
    public void AddDeveloper(User developer)
    {
        _developers.Add(developer);
    }
    
    public void RemoveDeveloper(User developer)
    {
        _developers.Remove(developer);
    }
    
    public void AddReport(Report report)
    {
        _reports.Add(report);
    }
    
    public void RemoveReport(Report report)
    {
        _reports.Remove(report);
    }

    public abstract void Execute();
    public abstract override string ToString();
}