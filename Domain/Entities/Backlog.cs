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

            Logger.DisplayCreatedAlert(nameof(Backlog), _id.ToString());
        }

        public virtual void AddItemToBacklog(Item item)
        {
            _items.Add(item);

            Logger.DisplayAddedAlert(nameof(Backlog), item.Title);
        }

        public void RemoveItemFromBacklog(Item item)
        {
            _items.Remove(item);

            Logger.DisplayRemovedAlert(nameof(Backlog), item.Title);
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"Id: {_id}");
            sb.AppendLine($"Items: {_items.Count}");

            foreach (var item in _items)
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString();
        }
    }
}
