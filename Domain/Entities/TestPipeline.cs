using Domain.Helpers;
using Domain.Phases;
using Domain.Tools;

namespace Domain.Entities
{
    public class TestPipeline : Pipeline
    {
        public TestPipeline(string name) : base(name)
        {
            Logger.DisplayCreatedAlert(nameof(TestPipeline), name);
        }

        protected override void InitializeSelectedActions()
        {
            var phases = new List<Phase>();

            foreach (var phase in AssemblyScanner.GetSubclassesOf<Phase>())
            {
                phases.Add((Phase)Activator.CreateInstance(phase)!);
            }

            foreach (var phase in phases.Where(p => p.GetType() != typeof(DeployPhase)).OrderBy(p => p.SortIndex))
            {
                SelectedActions.Add(phase);
            }
        }

        public override void AddAction(Action action)
        {
            if(action.Phase!.GetType() == typeof(DeployPhase))
            {
                Logger.DisplayCustomAlert(nameof(TestPipeline), nameof(AddAction), "Can't add deploy action to test pipeline");
                return;
            }

            base.AddAction(action);
        }

        protected override void Test() => RunAction(typeof(TestPhase));
    }
}
