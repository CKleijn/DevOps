using Domain.Helpers;
using Domain.Interfaces.Observer;
using Domain.States.Sprint;
using System.Text;

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
                Logger.DisplayUpdatedAlert(nameof(StartDate), _startDate.ToString("dd/mm/yyyy"));
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
                Logger.DisplayUpdatedAlert(nameof(EndDate), _endDate.ToString("dd/mm/yyyy"));
            }
        }
    }
    
    private SprintBacklog _sprintBacklog { get; init; }
    public SprintBacklog SprintBacklog { get => _sprintBacklog; init => _sprintBacklog = value; }
    
    private SprintState? _previousStatus { get; set; }
    public SprintState? PreviousStatus
    {
        get => _previousStatus;
        set
        {
            if (CurrentStatus.GetType() == typeof(ClosedState))
            {
                _previousStatus = value;
                Logger.DisplayUpdatedAlert(nameof(PreviousStatus), _previousStatus!.GetType().ToString());
            }
        }
    }
    
    private SprintState _currentStatus { get; set; }
    public SprintState CurrentStatus
    {
        get => _currentStatus;
        set
        {
            if (CurrentStatus.GetType() != typeof(ClosedState))
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
                Logger.DisplayUpdatedAlert(nameof(ScrumMaster), _scrumMaster.Name);
            }
        } 
    }
    
    private IList<Developer> _developers { get; init; }
    public IList<Developer> Developers { get => _developers; init => _developers = value; }

    private IList<Developer> _testers { get; init; }
    public IList<Developer> Testers { get => _testers; init => _testers = value; }

    private Project _project { get; init; }
    public Project Project { get => _project; init => _project = value; }

    private Pipeline? _pipeline { get; set; }
    public Pipeline? Pipeline { get => _pipeline; set => _pipeline = value; }

    private IList<IObserver> _observers { get; init; }
    public IList<IObserver> Observers { get => _observers; init => _observers = value; }
    
    protected Sprint(string title, DateTime startDate, DateTime endDate, Developer scrumMaster, Project project)
    {
        _id = Guid.NewGuid();
        _title = title;
        _startDate = startDate;
        _endDate = endDate;
        _scrumMaster = scrumMaster;
        _developers = new List<Developer>();
        _testers = new List<Developer>();
        _currentStatus = new InitialState(this);
        _reports = new List<Report>();
        _sprintBacklog = new SprintBacklog(this);
        _project = project;
        _observers = new List<IObserver>();

        Register(scrumMaster);
        Register(project.ProductOwner);
    }
    
    public void AddDeveloper(Developer developer)
    {
        if (!ValidateChange())
        {
            return;
        }

        _developers.Add(developer);
        
        Logger.DisplayAddedAlert(nameof(Developers), developer.Name);

        Register(developer);
    }
    
    public void RemoveDeveloper(Developer developer)
    {
        if (!ValidateChange())
        {
            return;
        }

        _developers.Remove(developer);
        
        Logger.DisplayRemovedAlert(nameof(Developers), developer.Name);

        Unregister(developer);
    }

    public void AddTester(Developer tester)
    {
        if (!ValidateChange())
        {
            return;
        }

        _testers.Add(tester);

        Logger.DisplayAddedAlert(nameof(Testers), tester.Name);

        Register(tester);
    }

    public void RemoveTester(Developer tester)
    {
        if (!ValidateChange())
        {
            return;
        }

        _testers.Remove(tester);

        Logger.DisplayRemovedAlert(nameof(Testers), tester.Name);

        Unregister(tester);
    }

    public void AddReport(Report report)
    {
        if (!ValidateChange())
        {
            return;
        }

        _reports.Add(report);
        
        Logger.DisplayAddedAlert(nameof(Reports), report.Title);
    }
    
    public void RemoveReport(Report report)
    {
        if (!ValidateChange())
        {
            return;
        }

        _reports.Remove(report);
        
        Logger.DisplayRemovedAlert(nameof(Reports), report.Title);
    }

    public void InitializeSprint() => _currentStatus.InitializeSprint();
    public void ExecuteSprint() => _currentStatus.ExecuteSprint();
    public void FinishSprint() => _currentStatus.FinishSprint();
    public void ReleaseSprint() => _currentStatus.ReleaseSprint();
    public void ReviewSprint() => _currentStatus.ReviewSprint();
    public void CancelSprint() => _currentStatus.CancelSprint();
    public void CloseSprint() => _currentStatus.CloseSprint();
    
    public void Register(IObserver observer)
    {
        _observers.Add(observer);
        
        Logger.DisplayAddedAlert(nameof(Project), _title);
    }

    public void Unregister(IObserver observer)
    {
        _observers.Remove(observer);
        
        Logger.DisplayRemovedAlert(nameof(Project), _title);
    }

    public void NotifyObservers(Notification notification)
    {
        if (notification.TargetUsers.Count is 0)
        {
            return;
        }

        _observers
            .Where(observer => notification.TargetUsers.Any(targetUser => targetUser.Id == observer.Id))
            .ToList()
            .ForEach(o => o.Update(notification));
    }
    
    protected abstract bool ValidateChange();

    public override string ToString()
    {
        StringBuilder sb = new();

        sb.AppendLine($"Sprint: {Title}");
        sb.AppendLine($"Start Date: {StartDate.ToString("dd/mm/yyyy")}");
        sb.AppendLine($"End Date: {EndDate.ToString("dd/mm/yyyy")}");
        sb.AppendLine($"Status: {CurrentStatus.GetType()}");
        sb.AppendLine($"Scrum Master: {ScrumMaster.Name}");
        sb.AppendLine($"Developers: {Developers.Count}");

        foreach (var developer in Developers)
        {
            sb.AppendLine(developer.ToString());
        }

        sb.AppendLine($"Testers: {Testers.Count}");

        foreach (var tester in Testers)
        {
            sb.AppendLine(tester.ToString());
        }

        sb.AppendLine($"Items: {SprintBacklog.Items.Count}");

        foreach (var item in SprintBacklog.Items)
        {
            sb.AppendLine(item.ToString());
        }

        return sb.ToString();
    }
}