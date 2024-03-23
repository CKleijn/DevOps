using Domain.Helpers;

namespace Domain.States.Sprint;

public class InitialState : SprintState
{
    private Entities.Sprint _context { get; init; }
    public override Entities.Sprint Context { get => _context; init => _context = value; }
    
    public InitialState(Entities.Sprint context)
    {
        _context = context;
    }

    public override void ExecuteSprint()
    {
        _context.CurrentStatus = new ExecutedState(_context);
        
        Logger.DisplayCustomAlert(nameof(InitialState), nameof(ExecuteSprint), "Sprint status changed to executed!");
    }
}