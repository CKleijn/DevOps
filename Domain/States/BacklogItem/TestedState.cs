using Domain.Entities;
using Domain.Helpers;

namespace Domain.States.BacklogItem
{
    public class TestedState : BacklogItemState
    {
        private Item _context { get; init; }
        public override Item Context { get => _context; init => _context = value; }

        public TestedState(Item context)
        {
            _context = context;
        }

        public override void DenyTestedBacklogItem()
        {
            _context.CurrentStatus = new ReadyForTestingState(_context);

            Logger.DisplayCustomAlert(nameof(TestedState), nameof(DenyTestedBacklogItem), "Backlog item status changed to ready for testing!");
        }

        public override void FinalizeBacklogItem()
        {
            if (_context.Activities.Count is not 0 && _context.Activities.Any(activity => !activity.IsFinished))
            {
                Logger.DisplayCustomAlert(nameof(TestedState), nameof(FinalizeBacklogItem), "The backlog item can't be set to done when all activities aren't finished!");
                return;
            }

            _context.CurrentStatus = new DoneState(_context);

            Logger.DisplayCustomAlert(nameof(TestedState), nameof(FinalizeBacklogItem), "Backlog item status changed to done!");
        }
    }
}
