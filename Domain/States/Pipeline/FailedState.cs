using Domain.Entities;
using Domain.Helpers;

namespace Domain.States.Pipeline
{
    public class FailedState : PipelineState
    {
        private Entities.Pipeline _context { get; init; }
        public override Entities.Pipeline Context { get => _context; init => _context = value; }

        public FailedState(Entities.Pipeline context)
        {
            _context = context;
        }

        public override void ExecutePipeline()
        {
            _context.CurrentStatus = new ExecutingState(_context);

            Logger.DisplayCustomAlert(nameof(FailedState), nameof(ExecutePipeline), "Pipeline status changed to executed (RERUN)!");

            _context.RerunPipeline();
        }

        public override void CancelPipeline()
        {
            Notification notification = new Notification("Pipeline cancelled", $"The release of the pipeline (with an id of {_context.Id}) has been cancelled after failure!");

            notification.AddTargetUser(_context.Sprint.ScrumMaster);

            _context.Sprint.NotifyObservers(notification);

            _context.CurrentStatus = new CancelledState(_context);

            Logger.DisplayCustomAlert(nameof(FailedState), nameof(CancelPipeline), "Pipeline status changed to cancelled!");
        }
    }
}
