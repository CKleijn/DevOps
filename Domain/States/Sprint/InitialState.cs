using DomainServices.Interfaces;

namespace Domain.States.Sprint;

public class InitialState : ISprintState
{
    private Entities.Sprint _context { get; init; }

    public InitialState(Entities.Sprint context)
    {
        _context = context;
    }
    
    //TODO: Implement methods
}