using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces.States;

namespace Domain.States.Sprint;

public class ReleaseState : ISprintState
{
    private Entities.Sprint _context { get; init; }
    public Entities.Sprint Context { get => _context; init => _context = value; }

    public ReleaseState(Entities.Sprint context)
    {
        _context = context;
    }

    public void InitializeSprint() => throw new NotImplementedException();
    
    public void ExecuteSprint()
    {
        Logger.DisplayCustomAlert(nameof(SprintRelease), nameof(ExecuteSprint), $"Execute Sprint ({_context.Title}).");
    }

    public void FinishSprint() => throw new NotImplementedException();

    public void ReleaseSprint()
    {
        if (_context is SprintRelease sprint)
        {
            sprint.Pipeline.ExecutePipeline();
            
            Logger.DisplayCustomAlert(nameof(SprintRelease), nameof(ReleaseSprint), "Execute pipeline for sprint releasing");
            
            return;
        }
        
        Logger.DisplayCustomAlert(nameof(SprintRelease), nameof(ReleaseSprint), "Sprint is of incorrect type");
    }

    public void ReviewSprint() => throw new NotImplementedException();

    public void CancelSprint()
    {
        _context.CurrentStatus = new CancelledState(_context);
        
        Logger.DisplayCustomAlert(nameof(ReleaseState), nameof(CancelSprint), "Sprint status changed to cancelling");
    }
    public void CloseSprint() => throw new NotImplementedException();
}