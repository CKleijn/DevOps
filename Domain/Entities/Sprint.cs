using Domain.Helpers;
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
                Logger.DisplayUpdatedAlert(nameof(Title), _title);
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
                Logger.DisplayUpdatedAlert(nameof(StartDate), _startDate.ToString());
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
                Logger.DisplayUpdatedAlert(nameof(EndDate), _endDate.ToString());
            }
        }
    }
    
    private SprintBacklog _sprintBacklog { get; init; }
    public SprintBacklog SprintBacklog { get => _sprintBacklog; init => _sprintBacklog = value; }
    
    private ISprintState? _previousStatus { get; set; }
    public ISprintState? PreviousStatus
    {
        get => _previousStatus;
        set
        {
            if (ValidateChange())
            {
                _previousStatus = value;
                Logger.DisplayUpdatedAlert(nameof(PreviousStatus), _previousStatus!.GetType().ToString());
            }
        }
    }
    
    private ISprintState _currentStatus { get; set; }
    public ISprintState CurrentStatus
    {
        get => _currentStatus;
        set
        {
            if (ValidateChange())
            {
                _previousStatus = _currentStatus;
                _currentStatus = value;
                Logger.DisplayUpdatedAlert(nameof(CurrentStatus), _currentStatus.GetType().ToString());
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
                _scrumMaster = value;
                Logger.DisplayUpdatedAlert(nameof(ScrumMaster), _scrumMaster.ToString());
            }
        } 
    }
    
    private IList<Developer> _developers { get; init; }
    public IList<Developer> Developers { get => _developers; init => _developers = value; }
    
    public Sprint(string title, DateTime startDate, DateTime endDate, User scrumMaster)
    {
        _id = Guid.NewGuid();
        _title = title;
        _startDate = startDate;
        _endDate = endDate;
        _scrumMaster = scrumMaster;
        _developers = new List<Developer>();
        _currentStatus = new InitialState(this);
        _reports = new List<Report>();
        _sprintBacklog = new SprintBacklog(this);
    }
    
    //TODO: add state functions
    public void AddDeveloper(Developer developer)
    {
        if (!ValidateChange())
            return;
        
        _developers.Add(developer);
        
        Logger.DisplayUpdatedAlert(nameof(Developers), $"Added: {developer.Name}");
    }
    
    public void RemoveDeveloper(Developer developer)
    {
        if (!ValidateChange())
            return;
        
        _developers.Remove(developer);
        
        Logger.DisplayUpdatedAlert(nameof(Developers), $"Removed: {developer.Name}");
    }
    
    public void AddReport(Report report)
    {
        if (!ValidateChange())
            return;
        
        _reports.Add(report);
        
        Logger.DisplayUpdatedAlert(nameof(Reports), $"Added: {report.Title}");
    }
    
    public void RemoveReport(Report report)
    {
        if (!ValidateChange())
            return;
        
        _reports.Remove(report);
        
        Logger.DisplayUpdatedAlert(nameof(Reports), $"Removed: {report.Title}");
    }
    
    //** Start State functions **//

    public void ExecuteSprint() => _currentStatus.ExecuteSprint();
    public void FinishSprint() => _currentStatus.FinishSprint();
    public void ReleaseSprint() => _currentStatus.ReleaseSprint();
    public void ReviewSprint() => _currentStatus.ReviewSprint();
    public void CancelSprint() => _currentStatus.ReviewSprint();
    
    //** End State functions **//
    
    private bool ValidateChange()
    {
        //Don't allow mutation whenever state differs from the initial state
        if (_currentStatus.GetType() != typeof(InitialState))
        {
            Logger.DisplayCustomAlert(nameof(Sprint), nameof(ValidateChange), $"Can't update sprint in current state ({_currentStatus.GetType()}).");
            return false;
        }
        
        //Perform actions alteration is done on a sprint that has already ended
        if (_endDate < DateTime.Now)
        {
            //Check if sprint is a sprint review and has reviews
            if (this is SprintReview { Reviews.Count: 0 })
            {
                Logger.DisplayCustomAlert(nameof(Sprint), nameof(ValidateChange), $"Can't finish sprint review without any reviews. Provide at least one review.");
                
                //Set sprint to review state if it isn't already
                if(_currentStatus.GetType() != typeof(ReviewState))
                    _currentStatus = new ReviewState(this);
                
                return false;
            }
            
            //Execute sprint
            Execute();
            
            //Set sprint to finished state if it isn't already
            if(_currentStatus.GetType() != typeof(FinishedState))
                _currentStatus = new FinishedState(this);
            
            Logger.DisplayCustomAlert(nameof(Sprint), nameof(ValidateChange), $"Can't update sprint after end date. Sprint will be set to close if it isn't already.");
            
            return false;
        }
        
        return true;
    }
    
    public abstract void Execute();
    public abstract override string ToString();
}