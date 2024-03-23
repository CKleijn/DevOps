using Domain.Entities;
using Domain.Helpers;

namespace Domain.States.BacklogItem
{
    public class TodoState : BacklogItemState
    {
        private Item _context { get; init; }
        public override Item Context { get => _context; init => _context = value; }

        public TodoState (Item context)
        {
            _context = context;
        }

        public override void DevelopBacklogItem()
        {
            if (_context.SprintBacklog is null)
            {
                Logger.DisplayCustomAlert(nameof(TodoState), nameof(DevelopBacklogItem), "The backlog item is not part of a sprint backlog!");
                return;
            }
            
            if (_context.PreviousStatus!.Context.Developer.Id != _context.Developer.Id)
            {
                Notification notification = new Notification("Different developer on backlog item", $"The backlog item (with an id of {_context.Id}) has a different developer!");
            
                notification.AddTargetUser(_context.SprintBacklog!.Sprint.ScrumMaster);
            
                _context.SprintBacklog.Sprint.NotifyObservers(notification);
            
                Logger.DisplayCustomAlert(nameof(TodoState), nameof(DevelopBacklogItem), "The backlog item needs to be developed by the same developer!");
            
                return;
            }

            _context.CurrentStatus = new DoingState(_context);

            Logger.DisplayCustomAlert(nameof(TodoState), nameof(DevelopBacklogItem), "Backlog item status changed to doing!");
        }
    }
}
