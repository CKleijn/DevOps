using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces.States;

namespace Domain.States.BacklogItem
{
    public class TestedState : IBacklogItemState
    {
        private Item _context { get; init; }
        public Item Context { get => _context; init => _context = value; }

        public TestedState(Item context)
        {
            _context = context;
        }

        public void DevelopBacklogItem() => throw new NotImplementedException();

        public void FinalizeDevelopmentBacklogItem() => throw new NotImplementedException();

        public void TestingBacklogItem() => throw new NotImplementedException();

        public void DenyDevelopedBacklogItem() => throw new NotImplementedException();

        public void FinalizeTestingBacklogItem() => throw new NotImplementedException();

        public void DenyTestedBacklogItem()
        {
            _context.PreviousStatus = this;
            _context.CurrentStatus = new ReadyForTestingState(_context);

            Logger.DisplayCustomAlert(nameof(TestedState), nameof(DenyTestedBacklogItem), "Backlog item status changed to ready for testing");
        }

        public void FinalizeBacklogItem()
        {
            _context.PreviousStatus = this;
            _context.CurrentStatus = new DoneState(_context);

            Logger.DisplayCustomAlert(nameof(TestedState), nameof(FinalizeBacklogItem), "Backlog item status changed to done");
        }

        public void CloseBacklogItem() => throw new NotImplementedException();
    }
}
