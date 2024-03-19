using Domain.Helpers;
using Domain.Interfaces.Composite;
using Domain.Interfaces.States;
using Domain.Phases;
using Domain.States.Pipeline;
using Domain.Tools;
using System.Text;

namespace Domain.Entities
{
    public abstract class Pipeline : IPipeline
    {
        private Guid _id { get; init; }
        public Guid Id { get => _id; init => _id = value; }

        private string _name { get; set; }
        public string Name { get => _name; set => _name = value; }

        private IPipelineState? _previousStatus { get; set; }
        public IPipelineState? PreviousStatus { get => _previousStatus; set => _previousStatus = value; }

        private IPipelineState _currentStatus { get; set; }
        public IPipelineState CurrentStatus { get => _currentStatus; set => _currentStatus = value; }

        private IList<IPipeline> _allActions { get; set; }
        public IList<IPipeline> AllActions { get => _allActions; set => _allActions = value; }

        private IList<IPipeline> _selectedActions { get; set; }
        public IList<IPipeline> SelectedActions { get => _selectedActions; set => _selectedActions = value; }

        public Pipeline(string name)
        {
            _id = Guid.NewGuid();
            _name = name;
            _currentStatus = new InitialState(this);
            _allActions = new List<IPipeline>();
            _selectedActions = new List<IPipeline>();

            InitializeAllActions();
            InitializeSelectedActions();
        }

        private void InitializeAllActions()
        {
            var actions = new List<Action>();
            var phases = new List<Phase>();

            foreach (var action in AssemblyScanner.GetSubclassesOf<Action>())
            {
                actions.Add((Action)Activator.CreateInstance(action)!);
            }

            foreach (var phase in AssemblyScanner.GetSubclassesOf<Phase>())
            {
                phases.Add((Phase)Activator.CreateInstance(phase)!);
            }

            foreach (var phase in phases.OrderBy(p => p.SortIndex))
            {
                actions.Where(a => a.Phase!.GetType() == phase.GetType()).ToList().ForEach(phase.Add);

                _allActions.Add(phase);
            }
        }

        protected virtual void InitializeSelectedActions()
        {
            var phases = new List<Phase>();

            foreach (var phase in AssemblyScanner.GetSubclassesOf<Phase>())
            {
                phases.Add((Phase)Activator.CreateInstance(phase)!);
            }

            foreach (var phase in phases.OrderBy(p => p.SortIndex))
            {
                _selectedActions.Add(phase);
            }
        }

        public virtual void Add(Action action)
        {
            var phase = _selectedActions.FirstOrDefault(a => a.GetType() == action.Phase!.GetType()) as Phase;
            phase!.Add(action);
        }

        public void Remove(Action action)
        {
            var phase = _selectedActions.FirstOrDefault(a => a.GetType() == action.Phase!.GetType()) as Phase;
            phase!.Remove(action);
        }

        public bool ExecutePipeline()
        {
            try
            {
                Start();
                Source();
                Package();
                Utility();
                Build();
                Test();
                Analyse();
                Deploy();
                Finish();

                return true;
            }
            catch (Exception e)
            {
                Logger.DisplayCustomAlert(nameof(Pipeline), nameof(ExecutePipeline), e.Message);
                return false;
            }
        }

        protected void RunAction(Type phaseType)
        {
            var phase = _selectedActions.FirstOrDefault(a => a.GetType() == phaseType) as Phase;

            if(phase!.Actions.Count > 0)
            {
                foreach (var action in phase.Actions.Cast<Action>())
                {
                    action.Execute();
                }
            }
            else
            {
                throw new Exception("Pipeline failed... need all required actions!");
            }
        }

        private void Start()
        {
            Logger.DisplayCustomAlert(nameof(Pipeline), nameof(Start), $"Start pipeline {_name} ...");
            Logger.DisplayCustomAlert(nameof(Pipeline), nameof(Start), $"Pipeline {_name} started!");
        }

        private void Source() => RunAction(typeof(SourcePhase));

        private void Package() => RunAction(typeof(PackagePhase));

        private void Utility() => RunAction(typeof(UtilityPhase));

        private void Build() => RunAction(typeof(BuildPhase));

        protected virtual void Test()
        {
            return;
        }

        private void Analyse() => RunAction(typeof(AnalysePhase));

        protected virtual void Deploy()
        {
            return;
        }

        private void Finish()
        {
            Logger.DisplayCustomAlert(nameof(Pipeline), nameof(Finish), $"Finish pipeline {_name} ...");
            Logger.DisplayCustomAlert(nameof(Pipeline), nameof(Finish), $"Pipeline {_name} finished!");
        }

        public void PrintAllActions()
        {
            Logger.DisplayCustomAlert(nameof(Pipeline), nameof(Print), "All pipeline actions");

            foreach (var action in _allActions)
            {
                action.Print(0);
            }
        }

        public void Print(int indentations = 0)
        {
            Logger.DisplayCustomAlert(nameof(Pipeline), nameof(Print), "Selected pipeline actions");

            foreach (var action in _selectedActions)
            {
                action.Print(0);
            }
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
