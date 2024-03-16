using Domain.Entities;
using Domain.Interfaces.States;

namespace Domain.States.BacklogItem
{
    public class DoingState : IBacklogItemState
    {
        private Item _context { get; init; }
        public Item Context { get => _context; init => _context = value; }

        public DoingState (Item context) 
        {
            _context = context;
        }

        public void DevelopBacklogItem() => throw new NotImplementedException();

        public void FinalizeDevelopmentBacklogItem()
        {
            // TODO: Send notifications to Testers

            _context.PreviousStatus = this;
            _context.CurrentStatus = new ReadyForTestingState(_context);

            Console.WriteLine("Backlog item status changed to ready for testing");
        }

        public void TestingBacklogItem() => throw new NotImplementedException();

        public void DenyDevelopedBacklogItem() => throw new NotImplementedException();

        public void FinalizeTestingBacklogItem() => throw new NotImplementedException();

        public void DenyTestedBacklogItem() => throw new NotImplementedException();

        public void FinalizeBacklogItem() => throw new NotImplementedException();

        public void CloseBacklogItem() => throw new NotImplementedException();
    }
}
