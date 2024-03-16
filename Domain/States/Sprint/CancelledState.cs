using Domain.Interfaces.States;

namespace Domain.States.Sprint;

public class CancelledState : ISprintState
{
    private Entities.Sprint _context { get; init; }

    public CancelledState(Entities.Sprint context)
    {
        _context = context;
    }
    
    //TODO: Implement methods
}