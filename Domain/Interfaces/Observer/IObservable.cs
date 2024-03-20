using Domain.Entities;

namespace Domain.Interfaces.Observer;

public interface IObservable
{
    void Register(IObserver observer);
    
    void Unregister(IObserver observer);
    
    void NotifyObservers(Notification changed);
}