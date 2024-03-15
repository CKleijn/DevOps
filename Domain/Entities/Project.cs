using System.Text;

namespace Domain.Entities;

public class Project : IObservable<Project>
{
    private Guid _id { get; init; }
    private Guid Id { get => _id; init => _id = value; }
    
    private string _title { get; set; }
    public string Title { get => _title; set => _title = value; }
    
    private string _description { get; set; }
    public string Description
    {
        get => _description;
        set { _description = value; }
    }

    private User _productOwner { get; init; }
    public User ProductOwner { get => _productOwner; init => _productOwner = value;}
    
    private User _createdBy { get; init; }
    public User CreatedBy { get => _createdBy; init => _createdBy = value;}
    
    private DateTime? _updatedAt { get; set; }
    public DateTime? UpdatedAt { get => _updatedAt; set => _updatedAt = value; }
    
    private DateTime _createdAt { get; init; }
    public DateTime CreatedAt { get => _createdAt; init => _createdAt = value;}
    
    private List<IObserver<Project>> Observers { get; set; }
    
    //TODO: implement Backlog, Pipeline and VersionControl

    public Project(string title, string description, User productOwner, User createdBy)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        ProductOwner = productOwner;
        CreatedBy = createdBy;
        CreatedAt = DateTime.Now;
        Observers = new();
    }

    public IDisposable Subscribe(IObserver<Project> observer)
    {
        if(!Observers.Contains(observer))
        {
            Observers.Add(observer);
        }
        
        return new Unsubscriber(Observers, observer);
    }
    
    //TODO: When will notify be executed?
    public void Notify()
    {
        foreach (var observer in Observers)
        {
            observer.OnNext(this);
        }
    }
    
    public void RemoveObservers()
    {
        foreach (var observer in Observers)
            if (Observers.Contains(observer))
                observer.OnCompleted();
        
        Observers.Clear();
    }
    
    public override string ToString()
    {
        StringBuilder sb = new();
        
        sb.AppendLine($"Id: {Id}");
        sb.AppendLine($"Title: {Title}");
        sb.AppendLine($"Description: {Description}");
        sb.AppendLine($"ProductOwner: {ProductOwner}");
        sb.AppendLine($"CreatedBy: {CreatedBy}");
        sb.AppendLine($"UpdatedAt: {UpdatedAt}");
        sb.AppendLine($"CreatedAt: {CreatedAt}");

        return sb.ToString();
    }
    
    private class Unsubscriber : IDisposable
    {
        private List<IObserver<Project>>_observers;
        private IObserver<Project> _observer;

        public Unsubscriber(List<IObserver<Project>> observers, IObserver<Project> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
            {
                _observers.Remove(_observer);
            }
        }
    }
}