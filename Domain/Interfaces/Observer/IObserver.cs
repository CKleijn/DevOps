using Domain.Entities;

namespace Domain.Interfaces.Observer;

public interface IObserver
{
    Guid Id { get; }
    void Update(Notification notification);
}