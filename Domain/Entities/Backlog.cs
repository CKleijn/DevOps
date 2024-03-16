using System.Text;

namespace Domain.Entities
{
    public abstract class Backlog
    {
        private Guid _id { get; init; }
        public Guid Id { get => _id; init => _id = value; }

        private IList<Item> _items { get; init; }
        public IList<Item> Items { get => _items; init => _items = value; }

        private DateTime? _updatedAt { get; set; }
        public DateTime? UpdatedAt { get => _updatedAt; set => _updatedAt = value; }

        private DateTime _createdAt { get; init; }
        public DateTime CreatedAt { get => _createdAt; init => _createdAt = value; }

        public Backlog()
        {
            _id = Guid.NewGuid();
            _items = new List<Item>();
            _createdAt = DateTime.Now;
        }

        //TODO: implement functions
        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"Id: {_id}");
            sb.AppendLine($"Items: {_items.Count}");
            sb.AppendLine($"UpdatedAt: {_updatedAt}");
            sb.AppendLine($"CreatedAt: {_createdAt}");

            return sb.ToString();
        }
    }
}
