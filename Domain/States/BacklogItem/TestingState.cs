using Domain.Entities;
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
            // TODO: Send notification to Scrum Master

            _context.PreviousStatus = this;
            _context.CurrentStatus = new TodoState(_context);

            Console.WriteLine("Backlog item status changed to todo");
        }

        public void FinalizeTestingBacklogItem()
        {
            // TODO: Send notification to (Lead) developer

            _context.PreviousStatus = this;
            _context.CurrentStatus = new TestedState(_context);

            Console.WriteLine("Backlog item status changed to tested");
        }

        public void DenyTestedBacklogItem() => throw new NotImplementedException();

        public void FinalizeBacklogItem() => throw new NotImplementedException();

        public void CloseBacklogItem() => throw new NotImplementedException();
    }
}
