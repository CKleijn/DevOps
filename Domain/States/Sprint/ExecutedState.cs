using Domain.Helpers;

namespace Domain.States.Sprint;

public class ExecutedState  : SprintState
{
    private Entities.Sprint _context { get; init; }
    public override Entities.Sprint Context { get => _context; init => _context = value; }
    
    public ExecutedState(Entities.Sprint context)
    {
        _context = context;
    }
    public override void FinishSprint()
    {
        _context.CurrentStatus = new FinishedState(_context);
        
        Logger.DisplayCustomAlert(nameof(ExecutedState), nameof(FinishSprint), "Sprint status changed to finished!");
    }
}