using Domain.Entities;
using Domain.Interfaces.States;

namespace Domain.States.BacklogItem
{
    public class ClosedState : IBacklogItemState
    {
        private Item _context { get; init; }
        public Item Context { get => _context; init => _context = value; }

        public ClosedState(Item context)
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

        public void ReceiveFeedback() => throw new NotImplementedException();

        public void CloseBacklogItem() => throw new NotImplementedException();

        
    }
}
