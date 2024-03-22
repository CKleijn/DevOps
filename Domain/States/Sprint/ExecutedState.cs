using Domain.Helpers;
using Domain.Interfaces.States;

namespace Domain.States.Sprint;

public class ExecutedState  : ISprintState
{
    private Entities.Sprint _context { get; init; }
    public Entities.Sprint Context { get => _context; init => _context = value; }
    
    public ExecutedState(Entities.Sprint context)
    {
        _context = context;
    }
    public void InitializeSprint() => throw new NotImplementedException();

    public void ExecuteSprint() => throw new NotImplementedException();

    public void FinishSprint()
    {
        _context.CurrentStatus = new FinishedState(_context);
        
        Logger.DisplayCustomAlert(nameof(ExecutedState), nameof(FinishSprint), "Sprint status changed to finished!");
    }

    public void ReleaseSprint() => throw new NotImplementedException();

    public void ReviewSprint() => throw new NotImplementedException();

    public void CancelSprint() => throw new NotImplementedException();

    public void CloseSprint() => throw new NotImplementedException();
}