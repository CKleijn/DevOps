using Domain.Helpers;
using System.Text;

namespace Domain.Entities
{
    public abstract class Backlog
    {
        private Guid _id { get; init; }
        public Guid Id { get => _id; init => _id = value; }

        private IList<Item> _items { get; init; }
        public IList<Item> Items { get => _items; init => _items = value; }

        public Backlog()
        {
            _id = Guid.NewGuid();
            _items = new List<Item>();

            Logger.DisplayCreatedAlert(nameof(Backlog), $"Backlog: {_id}");
        }

        //TODO: implement functions
        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"Id: {_id}");
            sb.AppendLine($"Items: {_items.Count}");

            return sb.ToString();
        }
    }
}
