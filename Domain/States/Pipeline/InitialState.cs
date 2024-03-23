using Domain.Helpers;

namespace Domain.States.Pipeline
{
    public class InitialState : PipelineState
    {
        private Entities.Pipeline _context {  get; init; }
        public override Entities.Pipeline Context { get => _context; init => _context = value; }

        public InitialState(Entities.Pipeline context)
        {
            _context = context;
        }

        public override void ExecutePipeline()
        {
            _context.CurrentStatus = new ExecutingState(_context);

            Logger.DisplayCustomAlert(nameof(InitialState), nameof(ExecutePipeline), "Pipeline status changed to executed!");

            _context.ExecutePipeline();
        }
    }
}
