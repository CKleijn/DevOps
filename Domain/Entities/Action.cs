using Domain.Interfaces.Composite;

namespace Domain.Entities
{
    public abstract class Action : IPipeline
    {
        private Guid _id { get; init; }
        public Guid Id { get => _id; init => _id = value; }

        private string? _command { get; set; }
        protected string? Command { get => _command; set => _command = value; }

        private Phase? _phase { get; set; }
        public Phase? Phase { get => _phase; set => _phase = value; }

        protected Action()
        {
            _id = Guid.NewGuid();
        }

        public abstract void ConnectToPhase();

        public abstract void Execute();

        public abstract void Print(int indentations);
    }
}
