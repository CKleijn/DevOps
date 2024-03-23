using Domain.Entities;
using Domain.Helpers;

namespace Domain.States.BacklogItem
{
    public class ReadyForTestingState : BacklogItemState
    {
        private Item _context { get; init; }
        public override Item Context { get => _context; init => _context = value; }

        public ReadyForTestingState(Item context)
        {
            _context = context;
        }

        public override void TestingBacklogItem()
        {
            _context.CurrentStatus = new TestingState(_context);

            Logger.DisplayCustomAlert(nameof(ReadyForTestingState), nameof(TestingBacklogItem), "Backlog item status changed to testing!");
        }
    }
}
