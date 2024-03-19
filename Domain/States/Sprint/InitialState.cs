using Domain.Helpers;
using Domain.Interfaces.States;

namespace Domain.States.Sprint;

public class InitialState : ISprintState
{
    private Entities.Sprint _context { get; init; }
    public Entities.Sprint Context { get => _context; init => _context = value; }
    
    public InitialState(Entities.Sprint context)
    {
        _context = context;
    }
    
    public void InitializeSprint() => throw new NotImplementedException();

    public void ExecuteSprint()
    {
        _context.CurrentStatus = new ExecutedState(_context);
        
        Logger.DisplayCustomAlert(nameof(InitialState), nameof(ExecuteSprint), "Sprint status changed to executing");
    }

    public void FinishSprint() => throw new NotImplementedException();

    public void ReleaseSprint() => throw new NotImplementedException();

    public void ReviewSprint() => throw new NotImplementedException();

    public void CancelSprint() => throw new NotImplementedException();
    
    public void CloseSprint() => throw new NotImplementedException();
}