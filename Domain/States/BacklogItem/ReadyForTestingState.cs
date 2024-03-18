using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces.States;

namespace Domain.States.BacklogItem
{
    public class ReadyForTestingState : IBacklogItemState
    {
        private Item _context { get; init; }
        public Item Context { get => _context; init => _context = value; }

        public ReadyForTestingState(Item context)
        {
            _context = context;
        }

        public void DevelopBacklogItem() => throw new NotImplementedException();

        public void FinalizeDevelopmentBacklogItem() => throw new NotImplementedException();

        public void TestingBacklogItem()
        {
            _context.PreviousStatus = this;
            _context.CurrentStatus = new TestingState(_context);

            Logger.DisplayCustomAlert(nameof(ReadyForTestingState), nameof(TestingBacklogItem), "Backlog item status changed to testing");
        }

        public void DenyDevelopedBacklogItem() => throw new NotImplementedException();

        public void FinalizeTestingBacklogItem() => throw new NotImplementedException();

        public void DenyTestedBacklogItem() => throw new NotImplementedException();

        public void FinalizeBacklogItem() => throw new NotImplementedException();

        public void ReceiveFeedback() => throw new NotImplementedException();

        public void CloseBacklogItem() => throw new NotImplementedException();
    }
}
