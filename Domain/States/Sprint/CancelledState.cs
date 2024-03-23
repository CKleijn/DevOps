using Domain.Helpers;

namespace Domain.States.Sprint;

public class CancelledState : SprintState
{
    private Entities.Sprint _context { get; init; }
    public override Entities.Sprint Context { get => _context; init => _context = value; }
    
    public CancelledState(Entities.Sprint context)
    {
        _context = context;
    }

    public override void InitializeSprint() 
    {
        _context.CurrentStatus = new InitialState(_context);
        
        Logger.DisplayCustomAlert(nameof(CancelledState), nameof(InitializeSprint), "Sprint status changed to initial!");
    }
}