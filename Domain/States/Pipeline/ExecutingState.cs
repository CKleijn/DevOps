using Domain.Entities;
using Domain.Helpers;

namespace Domain.States.Pipeline
{
    public class ExecutingState : PipelineState
    {
        private Entities.Pipeline _context { get; init; }
        public override Entities.Pipeline Context { get => _context; init => _context = value; }

        public ExecutingState(Entities.Pipeline context)
        {
            _context = context;
        }

        public override void ExecutePipeline()
        {
            try
            {
                _context.PipelineTemplate();
            }
            catch (Exception e)
            {
                FailPipeline();
                Logger.DisplayCustomAlert(nameof(Pipeline), nameof(ExecutePipeline), e.Message);
            }
        }

        public override void CancelPipeline()
        {
            Notification notification = new Notification("Pipeline release cancelled", $"The release of the pipeline (with an id of {_context.Id}) has been cancelled!");

            notification.AddTargetUser(_context.Sprint.ScrumMaster);

            _context.Sprint.NotifyObservers(notification);

            _context.CurrentStatus = new CancelledState(_context);

            Logger.DisplayCustomAlert(nameof(ExecutingState), nameof(CancelPipeline), "Pipeline status changed to cancelled!");
        }

        public override void FailPipeline()
        {
            Notification notification = new Notification("Pipeline release failed", $"The release of the pipeline (with an id of {_context.Id}) has failed!");

            notification.AddTargetUser(_context.Sprint.ScrumMaster);

            _context.Sprint.NotifyObservers(notification);

            _context.CurrentStatus = new FailedState(_context);

            Logger.DisplayCustomAlert(nameof(ExecutingState), nameof(FailPipeline), "Pipeline status changed to failed!");
        }

        public override void FinalizePipeline()
        {
            _context.CurrentStatus = new FinishedState(_context);

            Logger.DisplayCustomAlert(nameof(ExecutingState), nameof(FinalizePipeline), "Pipeline status changed to finished!");

            Notification notification = new Notification("Pipeline release succeeded", $"The release of the pipeline (with an id of {_context.Id}) has succeeded!");

            notification.AddTargetUser(_context.Sprint.ScrumMaster);
            notification.AddTargetUser(_context.Sprint.Project.ProductOwner);

            _context.Sprint.NotifyObservers(notification);
        }
    }
}
