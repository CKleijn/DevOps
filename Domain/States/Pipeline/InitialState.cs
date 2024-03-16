using Domain.Interfaces.States;

namespace Domain.States.Pipeline
{
    public class InitialState : IPipelineState
    {
        private Entities.Pipeline _context {  get; init; }
        public Entities.Pipeline Context { get => _context; init => _context = value; }

        public InitialState(Entities.Pipeline context)
        {
            _context = context;
        }

        public void ExecutePipeline()
        {
            _context.PreviousStatus = this;
            _context.CurrentStatus = new ExecutingState(_context);

            Console.WriteLine("Pipeline status changed to executing");
        }

        public void CancelPipeline() => throw new NotImplementedException();

        public void FailPipeline() => throw new NotImplementedException();

        public void FinalizePipeline() => throw new NotImplementedException();
    }
}
