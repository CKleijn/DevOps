using Domain.Entities;
using Domain.Helpers;

namespace Domain.States.BacklogItem
{
    public class DoneState : BacklogItemState
    {
        private Item _context { get; init; }
        public override Item Context { get => _context; init => _context = value; }

        public DoneState(Item context)
        {
            _context = context;
        }

        public override void ReceiveFeedbackBacklogItem()
        {
            _context.CurrentStatus = new TodoState(_context);

            Logger.DisplayCustomAlert(nameof(DoneState), nameof(ReceiveFeedbackBacklogItem), "Backlog item status changed to todo!");
        }

        public override void CloseBacklogItem()
        {
            _context.CurrentStatus = new ClosedState(_context);

            Logger.DisplayCustomAlert(nameof(DoneState), nameof(CloseBacklogItem), "Backlog item status changed to closed!");
        }
    }
}
