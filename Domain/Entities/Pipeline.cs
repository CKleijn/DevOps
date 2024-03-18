using Domain.Helpers;
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

        public Pipeline(string name)
        {
            _id = Guid.NewGuid();
            _name = name;
            _currentStatus = new InitialState(this);
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
            Logger.DisplayCustomAlert(nameof(Pipeline), nameof(Start), $"Start pipeline {_name} ...");
            Logger.DisplayCustomAlert(nameof(Pipeline), nameof(Start), $"Pipeline {_name} started!");
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
            Logger.DisplayCustomAlert(nameof(Pipeline), nameof(Finish), $"Finish pipeline {_name} ...");
            Logger.DisplayCustomAlert(nameof(Pipeline), nameof(Finish), $"Pipeline {_name} finished!");
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"Id: {_id}");
            sb.AppendLine($"Name: {_name}");
            sb.AppendLine($"PreviousStatus: {_previousStatus?.GetType().Name}");
            sb.AppendLine($"CurrentStatus: {_currentStatus.GetType().Name}");

            return sb.ToString();
        }
    }
}
