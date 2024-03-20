using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces.States;

namespace Domain.States.BacklogItem
{
    public class TestingState : IBacklogItemState
    {
        private Item _context { get; init; }
        public Item Context { get => _context; init => _context = value; }

        public TestingState(Item context)
        {
            _context = context;
        }

        public void DevelopBacklogItem() => throw new NotImplementedException();

        public void FinalizeDevelopmentBacklogItem() => throw new NotImplementedException();

        public void TestingBacklogItem() => throw new NotImplementedException();

        public void DenyDevelopedBacklogItem()
        {
            Notification notification = new Notification("Backlog item denied", $"The backlog item (with an id of {_context.Id}) has been denied!");

            notification.AddTargetUser(_context.SprintBacklog!.Sprint.ScrumMaster);

            _context.SprintBacklog.Sprint.NotifyObservers(notification);

            _context.CurrentStatus = new TodoState(_context);

            Logger.DisplayCustomAlert(nameof(TestingState), nameof(DenyDevelopedBacklogItem), "Backlog item status changed to todo!");
        }

        public void FinalizeTestingBacklogItem()
        {
            _context.CurrentStatus = new TestedState(_context);

            Logger.DisplayCustomAlert(nameof(TestingState), nameof(FinalizeTestingBacklogItem), "Backlog item status changed to tested!");
        }

        public void DenyTestedBacklogItem() => throw new NotImplementedException();

        public void FinalizeBacklogItem() => throw new NotImplementedException();

        public void ReceiveFeedbackBacklogItem() => throw new NotImplementedException();

        public void CloseBacklogItem() => throw new NotImplementedException();
    }
}
