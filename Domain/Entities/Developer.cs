namespace Domain.Entities
{
    public class Developer : User
    {
        public Developer(string name, string email, string password) : base(name, email, password) { }
    }
}
