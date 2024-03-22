using Domain.Helpers;
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

    public void InitializeSprint() 
    {
        _context.CurrentStatus = new InitialState(_context);
        
        Logger.DisplayCustomAlert(nameof(CancelledState), nameof(InitializeSprint), "Sprint status changed to initial!");
    }
    
    public void ExecuteSprint() => throw new NotImplementedException();
    
    public void FinishSprint() => throw new NotImplementedException();
    
    public void ReleaseSprint() => throw new NotImplementedException();
    
    public void ReviewSprint() => throw new NotImplementedException();
    
    public void CancelSprint() => throw new NotImplementedException();
    
    public void CloseSprint() => throw new NotImplementedException();
}