using Domain.Entities;
using Domain.Helpers;

namespace Domain.States.Sprint;

public class FinishedState : SprintState
{
    private Entities.Sprint _context { get; init; }
    public override Entities.Sprint Context { get => _context; init => _context = value; }

    public FinishedState(Entities.Sprint context)
    {
        _context = context;
    }
    
    public override void ReleaseSprint() 
    {
        if (_context is not SprintRelease)
        {
            return;
        }

        // Cancel sprint when results aren't "good enough" a.k.a not all backlog items from the sprint are closed
        if (_context.SprintBacklog.Items.Any(item => item.CurrentStatus.GetType() != typeof(BacklogItem.ClosedState)))
        {
            CancelSprint();
            return;
        }
        
        _context.CurrentStatus = new ReleaseState(_context);
        
        Logger.DisplayCustomAlert(nameof(FinishedState), nameof(ReleaseSprint), "Sprint status changed to release!");
    }

    public override void ReviewSprint()
    {
        if (_context is not SprintReview)
        {
            return;
        }
        
        // Cancel sprint when results aren't "good enough" a.k.a not all backlog items from the sprint are closed
        if (_context.SprintBacklog.Items.Any(item => item.CurrentStatus.GetType() != typeof(BacklogItem.ClosedState)))
        {
            CancelSprint();
            return;
        }
        
        _context.CurrentStatus = new ReviewState(_context);
        
        Logger.DisplayCustomAlert(nameof(FinishedState), nameof(ReviewSprint), "Sprint status changed to review!");
    }

    public override void CancelSprint()
    {
        _context.CurrentStatus = new CancelledState(_context);
        
        Logger.DisplayCustomAlert(nameof(FinishedState), nameof(CancelSprint), "Sprint status changed to cancelled!");

        Notification notification = new Notification("Sprint cancelled", "Sprint has been cancelled!");

        notification.AddTargetUser(_context.ScrumMaster);
        notification.AddTargetUser(_context.Project.ProductOwner);

        _context.NotifyObservers(notification);
    }
}