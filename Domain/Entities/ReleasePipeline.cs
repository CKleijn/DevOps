using Domain.Helpers;
using Domain.Phases;

namespace Domain.Entities
{
    public class ReleasePipeline : Pipeline
    {
        public ReleasePipeline(string name) : base(name) 
        {
            Logger.DisplayCreatedAlert(nameof(ReleasePipeline), name);
        }

        protected override void Test() => RunAction(typeof(TestPhase));

        protected override void Deploy() => RunAction(typeof(DeployPhase));
    }
}
