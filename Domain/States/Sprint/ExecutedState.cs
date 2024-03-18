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
    public void ExecuteSprint() => throw new NotImplementedException();

    public void FinishSprint()
    {
        //TODO: conditionals like checking whether end date has been reached(, or all tasks are done)?
        
        _context.CurrentStatus = new FinishedState(_context);
        
        Logger.DisplayCustomAlert(nameof(ExecutedState), nameof(FinishSprint), "Sprint status changed to finishing");
    }

    public void ReleaseSprint() => throw new NotImplementedException();

    public void ReviewSprint() => throw new NotImplementedException();

    public void CancelSprint() => throw new NotImplementedException();
}