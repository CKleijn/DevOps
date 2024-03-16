using Domain.Interfaces.States;

namespace Domain.States.Sprint;

public class ReviewState : ISprintState
{
    private Entities.Sprint _context { get; init; }

    public ReviewState(Entities.Sprint context)
    {
        _context = context;
    }
    
    //TODO: Implement methods
}