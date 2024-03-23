namespace Domain.States.Pipeline
{
    public class FinishedState : PipelineState
    {
        private Entities.Pipeline _context { get; init; }
        public override Entities.Pipeline Context { get => _context; init => _context = value; }

        public FinishedState(Entities.Pipeline context)
        {
            _context = context;
        }
    }
}
