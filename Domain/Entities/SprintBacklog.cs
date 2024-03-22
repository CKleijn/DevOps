using Domain.Helpers;
using Domain.States.Sprint;

namespace Domain.Entities
{
    public class SprintBacklog : Backlog
    {
        private Sprint _sprint { get; init; }
        public Sprint Sprint { get => _sprint; init => _sprint = value; }
        
        public SprintBacklog(Sprint sprint)
        {
            _sprint = sprint;
            
            Logger.DisplayCreatedAlert(nameof(SprintBacklog), _sprint.Title);
        }
        
        public override void AddItemToBacklog(Item item)
        {
            if (_sprint.CurrentStatus.GetType() != typeof(InitialState))
            {
                Logger.DisplayCustomAlert(nameof(SprintBacklog), nameof(AddItemToBacklog), "Can't add item to sprint when it isn't in the initial state!");
                return;
            }
            
            Items.Add(item);

            Logger.DisplayAddedAlert(nameof(Backlog), item.Title);
        }
    }
}