using Domain.Interfaces.States;
using Domain.States.Pipeline;
using System.Text;

namespace Domain.Entities
{
    public abstract class Pipeline
    {
        private Guid _id { get; init; }
        public Guid Id { get => _id; init => _id = value; }

        private string _name { get; set; }
        public string Name { get => _name; set => _name = value; }

        // TODO: Implement Actions
        private IPipelineState? _previousStatus { get; set; }
        public IPipelineState? PreviousStatus { get => _previousStatus; set => _previousStatus = value; }

        private IPipelineState _currentStatus { get; set; }
        public IPipelineState CurrentStatus { get => _currentStatus; set => _currentStatus = value; }

        private User _creator { get; init; }
        public User Creator { get => _creator; init => _creator = value; }

        private DateTime? _updatedAt { get; set; }
        public DateTime? UpdatedAt { get => _updatedAt; set => _updatedAt = value; }

        private DateTime _createdAt { get; init; }
        public DateTime CreatedAt { get => _createdAt; init => _createdAt = value; }

        public Pipeline(string name, User creator)
        {
            _id = Guid.NewGuid();
            _name = name;
            _currentStatus = new InitialState(this);
            _creator = creator;
            _createdAt = DateTime.Now;
        }

        public void ExecutePipeline()
        {
            Start();
            Build();
            Test();
            Deploy();
            Finish();
        }

        private void Start()
        {
            Console.WriteLine("Start pipeline...");
            Console.WriteLine("Pipeline started!");
        }

        private void Build()
        {
            // TODO: Forloop each build action
        }

        protected virtual void Test()
        {
            return;
        }

        protected virtual void Deploy()
        {
            return;
        }

        private void Finish()
        {
            Console.WriteLine("Finish pipeline...");
            Console.WriteLine("Pipeline finished!");
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"Id: {_id}");
            sb.AppendLine($"Name: {_name}");
            sb.AppendLine($"PreviousStatus: {_previousStatus?.GetType().Name}");
            sb.AppendLine($"CurrentStatus: {_currentStatus.GetType().Name}");
            sb.AppendLine($"Creator: {_creator.ToString()}");
            sb.AppendLine($"UpdatedAt: {_updatedAt}");
            sb.AppendLine($"CreatedAt: {_createdAt}");

            return sb.ToString();
        }
    }
}
