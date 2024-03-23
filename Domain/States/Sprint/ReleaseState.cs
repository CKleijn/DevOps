using Domain.Entities;
using Domain.Helpers;

namespace Domain.States.Sprint;

public class ReleaseState : SprintState
{
    private Entities.Sprint _context { get; init; }
    public override Entities.Sprint Context { get => _context; init => _context = value; }

    public ReleaseState(Entities.Sprint context)
    {
        _context = context;
    }

    public override void ReleaseSprint()
    {
        if (_context is SprintRelease sprint)
        {
            if (sprint.Pipeline is not null)
            {
                Logger.DisplayCustomAlert(nameof(SprintRelease), nameof(ReleaseSprint), "Execute pipeline to release sprint!");

                sprint.Pipeline.ExecutePipeline();

                return;
            }

            Logger.DisplayCustomAlert(nameof(SprintRelease), nameof(ReleaseSprint), "Sprint has no pipeline to execute!");

            return;
        }
        
        Logger.DisplayCustomAlert(nameof(SprintRelease), nameof(ReleaseSprint), "Sprint is of an incorrect type!");
    }

    public override void CancelSprint()
    {
        _context.CurrentStatus = new CancelledState(_context);
        
        Logger.DisplayCustomAlert(nameof(ReleaseState), nameof(CancelSprint), "Sprint status changed to cancelled!");
    }
}