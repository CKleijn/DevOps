using DomainServices.Interfaces;

namespace Domain.States.Sprint;

public class ReleaseState : ISprintState
{
    private Entities.Sprint _context { get; init; }

    public ReleaseState(Entities.Sprint context)
    {
        _context = context;
    }
    
    //TODO: Implement methods
}