using Domain.Entities;
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
    
    public void InitializeSprint() => throw new NotImplementedException();

    public void ExecuteSprint()
    {
        Logger.DisplayCustomAlert(nameof(SprintReview), nameof(ExecuteSprint), $"Successfully executed sprint ({_context.Title}), sprint will be closed.");
        
        CloseSprint();
    }

    public void FinishSprint() => throw new NotImplementedException();

    public void ReleaseSprint() => throw new NotImplementedException();

    public void ReviewSprint()
    {
        if (_context is SprintReview sprint)
        {
            if(sprint.Reviews.Count == 0)
            {
                Logger.DisplayCustomAlert(nameof(SprintReview), nameof(ReviewSprint), "Review can't be executed without any reviews. Provide at least one review.");
                return;
            }
            
            //Execute the sprint
            ExecuteSprint();

            return;
        }
        
        Logger.DisplayCustomAlert(nameof(SprintRelease), nameof(ReleaseSprint), "Sprint is of incorrect type");
    }

    public void CancelSprint()
    {
        _context.CurrentStatus = new CancelledState(_context);
        
        Logger.DisplayCustomAlert(nameof(ReviewState), nameof(CancelSprint), "Sprint status changed to cancelling");
    }
    public void CloseSprint()
    {
        _context.CurrentStatus = new ClosedState(_context);
        
        Logger.DisplayCustomAlert(nameof(ReviewState), nameof(CloseSprint), "Sprint status changed to closing");
    }
}