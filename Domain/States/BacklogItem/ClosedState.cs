using Domain.Entities;

namespace Domain.States.BacklogItem
{
    public class ClosedState : BacklogItemState
    {
        private Item _context { get; init; }
        public override Item Context { get => _context; init => _context = value; }

        public ClosedState(Item context)
        {
            _context = context;
        }
    }
}
