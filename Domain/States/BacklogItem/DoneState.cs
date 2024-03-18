using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces.States;

namespace Domain.States.BacklogItem
{
    public class DoneState : IBacklogItemState
    {
        private Item _context { get; init; }
        public Item Context { get => _context; init => _context = value; }

        public DoneState(Item context)
        {
            _context = context;
        }

        public void DevelopBacklogItem() => throw new NotImplementedException();

        public void FinalizeDevelopmentBacklogItem() => throw new NotImplementedException();

        public void TestingBacklogItem() => throw new NotImplementedException();

        public void DenyDevelopedBacklogItem() => throw new NotImplementedException();

        public void FinalizeTestingBacklogItem() => throw new NotImplementedException();

        public void DenyTestedBacklogItem() => throw new NotImplementedException();

        public void FinalizeBacklogItem() => throw new NotImplementedException();

        public void ReceiveFeedback()
        {
            _context.PreviousStatus = this;
            _context.CurrentStatus = new TodoState(_context);

            Logger.DisplayCustomAlert(nameof(DoneState), nameof(ReceiveFeedback), "Backlog item status changed to todo");
        }

        public void CloseBacklogItem()
        {
            _context.PreviousStatus = this;
            _context.CurrentStatus = new ClosedState(_context);

            Logger.DisplayCustomAlert(nameof(DoneState), nameof(CloseBacklogItem), "Backlog item status changed to closed");
        }
    }
}
