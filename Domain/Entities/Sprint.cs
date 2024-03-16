using Domain.Interfaces.States;
using Domain.States.Sprint;

namespace Domain.Entities;

public abstract class Sprint
{
    private Guid _id { get; init; }
    public  Guid Id { get => _id ; init => _id = value; }

    private string _title { get; set; }
    public string Title
    {
        get => _title;
        set
        {
            if (ValidateChange())
            {
                _title = value;
            }
        }
    }
    
    private DateTime _startDate { get; set; }
    public DateTime StartDate
    {
        get => _startDate;
        set
        {
            if (ValidateChange())
            {
                _startDate = value;
            }
        }
    }
    
    private DateTime _endDate { get; set; }
    public DateTime EndDate 
    {
        get => _endDate;
        set
        {
            if (ValidateChange())
            {
                _endDate = value;
            }
        }
    }
    
    private SprintBacklog _sprintBacklog { get; init; }
    public SprintBacklog SprintBacklog { get => _sprintBacklog; init => _sprintBacklog = value; }
    
    private ISprintState _status { get; set; }

    public ISprintState Status
    {
        get => _status;
        set
        {
            if (ValidateChange())
            {
                _status = value;
            }
        }
    }

    private IList<Report> _reports { get; init; }
    public IList<Report> Reports { get => _reports; init => _reports = value; }
    
    private User _scrumMaster { get; set; }
    public User ScrumMaster 
    { 
        get => _scrumMaster; 
        set
        {
            if (ValidateChange())
            {
                _createdBy = value;
            }
        } 
    }
    
    private IList<Developer> _developers { get; init; }
    public IList<Developer> Developers { get => _developers; init => _developers = value; }
    
    private User _createdBy { get; set; }

    public User CreatedBy
    {
        get => _createdBy;
        set
        {
            if (ValidateChange())
            {
                _createdBy = value;
            }
        }
    }

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
        _developers = new List<Developer>();
        _status = new InitialState(this);
        _reports = new List<Report>();
        _sprintBacklog = new SprintBacklog();
        _createdAt = DateTime.Now;
    }
    
    //TODO: add state functions
    public void AddDeveloper(Developer developer)
    {
        if (!ValidateChange())
            return;
        
        _developers.Add(developer);
    }
    
    public void RemoveDeveloper(Developer developer)
    {
        if (!ValidateChange())
            return;
        
        _developers.Remove(developer);
    }
    
    public void AddReport(Report report)
    {
        if (!ValidateChange())
            return;
        
        _reports.Add(report);
    }
    
    public void RemoveReport(Report report)
    {
        if (!ValidateChange())
            return;
        
        _reports.Remove(report);
    }
    
    private bool ValidateChange()
    {
        if (_status.GetType() == typeof(ReleaseState) || _status.GetType() == typeof(FinishedState))
        {
            Console.WriteLine("Can't update sprint in current state.");
            return false;
        }
        
        //Perform actions alteration is done on a sprint that has already ended
        if (_endDate < DateTime.Now)
        {
            //Check if sprint is a sprint review and has reviews
            if (this is SprintReview { Reviews.Count: 0 })
            {
                Console.WriteLine("Can't finish sprint review without any reviews. Provide at least one review.");
                return false;
            }
            
            //Execute sprint
            Execute();
            
            //Set sprint to close if it isn't already
            if(_status.GetType() != typeof(FinishedState))
                _status = new FinishedState(this);
            
            Console.WriteLine("Can't update sprint after end date. Sprint will be set to close if it isn't already.");
            
            return false;
        }
        
        return true;
    }
    
    public abstract void Execute();
    public abstract override string ToString();
}