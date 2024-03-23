namespace Domain.States.Sprint;

public class ClosedState : SprintState
{
    private Entities.Sprint _context { get; init; }
    public override Entities.Sprint Context { get => _context; init => _context = value; }
    
    public ClosedState(Entities.Sprint context)
    {
        _context = context;
    }
}