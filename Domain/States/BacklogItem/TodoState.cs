using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces.States;

namespace Domain.States.BacklogItem
{
    public class TodoState : IBacklogItemState
    {
        private Item _context { get; init; }
        public Item Context { get => _context; init => _context = value; }

        public TodoState (Item context)
        {
            _context = context;
        }

        public void DevelopBacklogItem()
        {
            if (_context.PreviousStatus?.Context.Developer.Id != _context.Developer.Id)
            {
                Logger.DisplayCustomAlert(nameof(TodoState), nameof(DevelopBacklogItem), "The backlog item needs to be fixed by the same developer!");
                return;
            }

            _context.PreviousStatus = this;
            _context.CurrentStatus = new DoingState(_context);

            Logger.DisplayCustomAlert(nameof(TodoState), nameof(DevelopBacklogItem), "Backlog item status changed to doing");
        }

        public void FinalizeDevelopmentBacklogItem() => throw new NotImplementedException();

        public void TestingBacklogItem() => throw new NotImplementedException();

        public void DenyDevelopedBacklogItem() => throw new NotImplementedException();

        public void FinalizeTestingBacklogItem() => throw new NotImplementedException();

        public void DenyTestedBacklogItem() => throw new NotImplementedException();

        public void FinalizeBacklogItem() => throw new NotImplementedException();

        public void CloseBacklogItem() => throw new NotImplementedException();
    }
}
