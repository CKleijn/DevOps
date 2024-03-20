using Domain.Helpers;
using Domain.Interfaces.States;

namespace Domain.States.Pipeline
{
    public class ExecutingState : IPipelineState
    {
        private Entities.Pipeline _context { get; init; }
        public Entities.Pipeline Context { get => _context; init => _context = value; }

        public ExecutingState(Entities.Pipeline context)
        {
            _context = context;
        }

        public void ExecutePipeline()
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

        public void CancelPipeline()
        {
            _context.CurrentStatus = new CancelledState(_context);

            Logger.DisplayCustomAlert(nameof(ExecutingState), nameof(CancelPipeline), "Pipeline status changed to cancelled");
        }

        public void FailPipeline()
        {
            // Send notification to scrum master

            _context.CurrentStatus = new FailedState(_context);

            Logger.DisplayCustomAlert(nameof(ExecutingState), nameof(FailPipeline), "Pipeline status changed to failed");
        }

        public void FinalizePipeline()
        {
            // Send notification to scrum master and product owner

            _context.CurrentStatus = new FinishedState(_context);

            Logger.DisplayCustomAlert(nameof(ExecutingState), nameof(FinalizePipeline), "Pipeline status changed to finished");
        }
    }
}
