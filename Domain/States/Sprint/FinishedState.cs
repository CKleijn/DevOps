using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces.States;

namespace Domain.States.Sprint;

public class FinishedState : ISprintState
{
    private Entities.Sprint _context { get; init; }
    public Entities.Sprint Context { get => _context; init => _context = value; }

    public FinishedState(Entities.Sprint context)
    {
        _context = context;
    }
    
    public void InitializeSprint() => throw new NotImplementedException();

    public void ExecuteSprint() => throw new NotImplementedException();

    public void FinishSprint() => throw new NotImplementedException();

    public void ReleaseSprint() 
    {
        if (_context is not SprintRelease)
        {
            return;
        }

        // Cancel sprint when results aren't "good enough" a.k.a not all backlog items from the sprint are closed
        if (_context.SprintBacklog.Items.Any(item => item.CurrentStatus.GetType() != typeof(ClosedState)))
        {
            CancelSprint();
            return;
        }
        
        _context.CurrentStatus = new ReleaseState(_context);
        
        Logger.DisplayCustomAlert(nameof(FinishedState), nameof(ReleaseSprint), "Sprint status changed to release!");
        
        _context.ReleaseSprint();
    }

    public void ReviewSprint()
    {
        if (_context is not SprintReview)
        {
            return;
        }
        
        // Cancel sprint when results aren't "good enough" a.k.a not all backlog items from the sprint are closed
        if (_context.SprintBacklog.Items.Any(item => item.CurrentStatus.GetType() != typeof(ClosedState)))
        {
            CancelSprint();
            return;
        }
        
        _context.CurrentStatus = new ReviewState(_context);
        
        Logger.DisplayCustomAlert(nameof(FinishedState), nameof(ReviewSprint), "Sprint status changed to review!");
    }

    public void CancelSprint()
    {
        _context.CurrentStatus = new CancelledState(_context);
        
        Logger.DisplayCustomAlert(nameof(FinishedState), nameof(CancelSprint), "Sprint status changed to cancelled!");

        Notification notification = new Notification("Sprint cancelled", "Sprint has been cancelled!");

        notification.AddTargetUser(_context.ScrumMaster);
        notification.AddTargetUser(_context.Project.ProductOwner);

        _context.NotifyObservers(notification);
    }

    public void CloseSprint() => throw new NotImplementedException();
}