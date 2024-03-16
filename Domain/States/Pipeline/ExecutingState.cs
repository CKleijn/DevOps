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

        public void ExecutePipeline() => throw new NotImplementedException();

        public void CancelPipeline()
        {
            _context.PreviousStatus = this;
            _context.CurrentStatus = new CancelledState(_context);

            Console.WriteLine("Pipeline status changed to cancelled");
        }

        public void FailPipeline()
        {
            _context.PreviousStatus = this;
            _context.CurrentStatus = new FailedState(_context);

            Console.WriteLine("Pipeline status changed to failed");
        }

        public void FinalizePipeline()
        {
            _context.PreviousStatus = this;
            _context.CurrentStatus = new FinishedState(_context);

            Console.WriteLine("Pipeline status changed to finished");
        }
    }
}
