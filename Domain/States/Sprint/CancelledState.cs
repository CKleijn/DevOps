using Domain.Interfaces.States;

namespace Domain.States.Sprint;

public class CancelledState : ISprintState
{
    private Entities.Sprint _context { get; init; }
    public Entities.Sprint Context { get => _context; init => _context = value; }
    
    public CancelledState(Entities.Sprint context)
    {
        _context = context;
    }
    
    public void ExecuteSprint() => throw new NotImplementedException();

    public void FinishSprint() => throw new NotImplementedException(); //TODO: can finish sprint be reactivated?

    public void ReleaseSprint() => throw new NotImplementedException();

    public void ReviewSprint() => throw new NotImplementedException();

    public void CancelSprint() => throw new NotImplementedException();
}