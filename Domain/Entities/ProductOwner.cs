using Domain.Enums;
using Domain.Helpers;

namespace Domain.Entities
{
    public class ProductOwner : User
    {
        public ProductOwner(string name, string email, string password, List<NotificationProvider> notificationProviders) : base(name, email, password, notificationProviders) 
        {
            Logger.DisplayCreatedAlert(nameof(ProductOwner), name);
        }
    }
}
