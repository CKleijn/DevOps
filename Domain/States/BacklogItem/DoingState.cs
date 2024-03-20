using Domain.Entities;
using Domain.Helpers;
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
            
            // START NOTIFICATION
            //Send notifications to Testers (developers)
            
            Notification notification = new Notification("Development of backlog item finished", $"The development of the backlog item (with an id of: {_context.Id}) has been finished.");

            var developers = _context.SprintBacklog!.Sprint.Developers;
            var scrumMaster = _context.SprintBacklog!.Sprint.ScrumMaster;

            if(!developers.Contains(scrumMaster))
                developers.Add(scrumMaster);
            
            foreach(var developer in developers)
            {
                notification.AddTargetUser(developer);
            }

            _context.SprintBacklog.Sprint.NotifyObservers(notification);
            // END NOTIFICATION
            
            _context.CurrentStatus = new ReadyForTestingState(_context);

            Logger.DisplayCustomAlert(nameof(DoingState), nameof(FinalizeBacklogItem), "Backlog item status changed to ready for testing");
        }

        public void TestingBacklogItem() => throw new NotImplementedException();

        public void DenyDevelopedBacklogItem() => throw new NotImplementedException();

        public void FinalizeTestingBacklogItem() => throw new NotImplementedException();

        public void DenyTestedBacklogItem() => throw new NotImplementedException();

        public void FinalizeBacklogItem() => throw new NotImplementedException();

        public void ReceiveFeedback() => throw new NotImplementedException();

        public void CloseBacklogItem() => throw new NotImplementedException();
    }
}
