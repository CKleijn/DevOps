using Domain.Entities;
using Domain.Helpers;

namespace Domain.States.BacklogItem
{
    public class DoingState : BacklogItemState
    {
        private Item _context { get; init; }
        public override Item Context { get => _context; init => _context = value; }

        public DoingState (Item context) 
        {
            _context = context;
        }

        public override void FinalizeDevelopmentBacklogItem()
        {
            Notification notification = new Notification("Development of backlog item finished", $"The development of the backlog item (with an id of: {_context.Id}) has been finished!");

            foreach (var tester in _context.SprintBacklog!.Sprint.Testers)
            {
                notification.AddTargetUser(tester);
            }

            _context.SprintBacklog.Sprint.NotifyObservers(notification);
            
            _context.CurrentStatus = new ReadyForTestingState(_context);

            Logger.DisplayCustomAlert(nameof(DoingState), nameof(FinalizeBacklogItem), "Backlog item status changed to ready for testing!");
        }
    }
}
