using Domain.Helpers;

namespace Domain.Entities
{
    public class SprintBacklog : Backlog
    {
        public SprintBacklog()
        {
            Logger.DisplayCreatedAlert(nameof(SprintBacklog), $"Backlog: {Id}");
        }
    }
}
