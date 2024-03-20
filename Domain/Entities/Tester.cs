using Domain.Enums;
using Domain.Helpers;

namespace Domain.Entities
{
    public class Tester : User
    {
        public Tester(string name, string email, string password, List<NotificationProvider> notificationProviders) : base(name, email, password, notificationProviders)
        {
            Logger.DisplayCreatedAlert(nameof(Tester), name);
        }
    }
}
