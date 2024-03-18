using Domain.Helpers;
using Domain.Interfaces.States;

namespace Domain.States.Sprint;

public class ReviewState : ISprintState
{
    private Entities.Sprint _context { get; init; }
    public Entities.Sprint Context { get => _context; init => _context = value; }
    
    public ReviewState(Entities.Sprint context)
    {
        _context = context;
    }
    
    public void ExecuteSprint() => throw new NotImplementedException();

    public void FinishSprint() => throw new NotImplementedException();

    public void ReleaseSprint() => throw new NotImplementedException();

    public void ReviewSprint() => throw new NotImplementedException();

    public void CancelSprint()
    {
        //TODO: add conditionals, when does it fail?
        
        _context.CurrentStatus = new CancelledState(_context);
        
        Logger.DisplayCustomAlert(nameof(ReviewState), nameof(CancelSprint), "Sprint status changed to cancelling");
    }
}