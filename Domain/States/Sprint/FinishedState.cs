using Domain.Interfaces.States;

namespace Domain.States.Sprint;

public class FinishedState : ISprintState
{
    private Entities.Sprint _context { get; init; }

    public FinishedState(Entities.Sprint context)
    {
        _context = context;
    }
    
    //TODO: Implement methods
}