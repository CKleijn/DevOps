using Domain.Enums;
using Domain.Helpers;

namespace Domain.Entities
{
    public class Developer : User
    {
        public Developer(string name, string email, string password, List<NotificationProvider> notificationProviders) : base(name, email, password, notificationProviders) 
        {
            Logger.DisplayCreatedAlert(nameof(Developer), name);
        }
    }
}
