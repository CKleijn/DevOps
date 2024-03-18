using Domain.Helpers;

namespace Domain.Entities
{
    public class Tester : User
    {
        public Tester(string name, string email, string password) : base(name, email, password)
        {
            Logger.DisplayCreatedAlert(nameof(Tester), name);
        }
    }
}
