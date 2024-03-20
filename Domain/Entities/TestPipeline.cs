using Domain.Helpers;
using Domain.Interfaces.Composite;
using Domain.Phases;

namespace Domain.Entities
{
    public class TestPipeline : Pipeline
    {
        public TestPipeline(string name) : base(name)
        {
            Logger.DisplayCreatedAlert(nameof(TestPipeline), name);
        }

        protected override void FilterPhases(List<IPipeline> phases)
        {
            foreach (var phase in phases.Cast<Phase>().Where(p => p.GetType() != typeof(DeployPhase)).OrderBy(p => p.SortIndex))
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
