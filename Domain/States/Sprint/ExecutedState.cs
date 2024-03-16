using DomainServices.Interfaces;

namespace Domain.States.Sprint;

public class ExecutedState  : ISprintState
{
    private Entities.Sprint _context { get; init; }

    public ExecutedState(Entities.Sprint context)
    {
        _context = context;
    }
    
    //TODO: Implement methods
}