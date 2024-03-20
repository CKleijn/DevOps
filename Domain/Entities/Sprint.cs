using Domain.Helpers;
using Domain.Interfaces.Observer;
using Domain.Interfaces.States;
using Domain.States.Sprint;

namespace Domain.Entities;

public abstract class Sprint : IObservable
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
    
    private Developer _scrumMaster { get; set; }
    public Developer ScrumMaster 
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

    private Project _project { get; init; }
    public Project Project { get => _project; init => _project = value; }

    private Pipeline? _pipeline { get; set; }
    public Pipeline? Pipeline { get => _pipeline; set => _pipeline = value; }
    
    private IList<IObserver> _observers { get; init; }
    public IList<IObserver> Observers { get => _observers; init => _observers = value; }
    
    public Sprint(string title, DateTime startDate, DateTime endDate, Developer scrumMaster, Project project)
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
        _project = project;
        _observers = new List<IObserver>();
    }
    
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

    public void SetPipeline(Pipeline pipeline)
    {
        _pipeline = pipeline;
    }
    
    //** Start State functions **//

    public void InitializeSprint() => _currentStatus.InitializeSprint();
    public void ExecuteSprint() => _currentStatus.ExecuteSprint();
    public void FinishSprint() => _currentStatus.FinishSprint();
    public void ReleaseSprint() => _currentStatus.ReleaseSprint();
    public void ReviewSprint() => _currentStatus.ReviewSprint();
    public void CancelSprint() => _currentStatus.ReviewSprint();
    public void CloseSprint() => _currentStatus.CloseSprint();
    
    //** End State functions **//
    
    //** Observer functions **//
    public void Register(IObserver observer)
    {
        _observers.Add(observer);
        
        Logger.DisplayUpdatedAlert(nameof(Project), $"Added observer to project: {Title}");
    }

    public void Unregister(IObserver observer)
    {
        _observers.Remove(observer);
        
        Logger.DisplayUpdatedAlert(nameof(Project), $"Removed observer from project: {Title}");
    }

    public void NotifyObservers(Notification notification)
    {
        if (notification.TargetUsers.Count == 0)
            return;
        
        _observers
            .Where(observer => notification.TargetUsers.Any(targetUser => targetUser.Id == observer.Id))
            .ToList()
            .ForEach(o => o.Update(notification));
    }
    
    //** End observer functions **//

    protected abstract bool ValidateChange();

    public abstract override string ToString();
}