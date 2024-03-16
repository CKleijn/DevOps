namespace Domain.Entities
{
    public class ProductOwner : User
    {
        public ProductOwner(string name, string email, string password) : base(name, email, password) { }
    }
}
