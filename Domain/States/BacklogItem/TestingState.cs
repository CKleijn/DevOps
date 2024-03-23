using Domain.Entities;
using Domain.Helpers;

namespace Domain.States.BacklogItem
{
    public class TestingState : BacklogItemState
    {
        private Item _context { get; init; }
        public override Item Context { get => _context; init => _context = value; }

        public TestingState(Item context)
        {
            _context = context;
        }

        public override void DenyDevelopedBacklogItem()
        {
            Notification notification = new Notification("Backlog item denied", $"The backlog item (with an id of {_context.Id}) has been denied!");

            notification.AddTargetUser(_context.SprintBacklog!.Sprint.ScrumMaster);

            _context.SprintBacklog.Sprint.NotifyObservers(notification);

            _context.CurrentStatus = new TodoState(_context);

            Logger.DisplayCustomAlert(nameof(TestingState), nameof(DenyDevelopedBacklogItem), "Backlog item status changed to todo!");
        }

        public override void FinalizeTestingBacklogItem()
        {
            _context.CurrentStatus = new TestedState(_context);

            Logger.DisplayCustomAlert(nameof(TestingState), nameof(FinalizeTestingBacklogItem), "Backlog item status changed to tested!");
        }
    }
}
