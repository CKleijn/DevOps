using Domain.Interfaces.Composite;

namespace Domain.Entities
{
    public abstract class Phase : IPipeline
    {
        private Guid _id { get; init; }
        public Guid Id { get => _id; init => _id = value; }

        private int _sortIndex { get; set; }
        public int SortIndex { get => _sortIndex; set => _sortIndex = value; }

        private IList<IPipeline> _actions { get; set; }
        public IList<IPipeline> Actions { get => _actions; set => _actions = value; }

        protected Phase()
        {
            _id = Guid.NewGuid();
            _actions = new List<IPipeline>();
        }

        public void Add(IPipeline action)
        {
            _actions.Add(action);
        }

        public void Remove(IPipeline action)
        {
            _actions.Remove(action);
        }

        public abstract void Print(int indentations);
    }
}
