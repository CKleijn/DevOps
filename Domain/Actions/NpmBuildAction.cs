using Domain.Helpers;
using Domain.Phases;

namespace Domain.Actions
{
    public class NpmBuildAction : Entities.Action
    {
        public NpmBuildAction() : base()
        {
            Command = "npm build";
        }

        public override void ConnectToPhase()
        {
            Phase = new BuildPhase();
        }

        public override void Execute()
        {
            Logger.DisplayCustomAlert(nameof(NpmBuildAction), nameof(Execute), $"Execute {Command}!");
            Logger.DisplayCustomAlert(nameof(NpmBuildAction), nameof(Execute), $"Successfully executed {Command} without any errors!");
        }

        public override string Print()
        {
            return $"{nameof(NpmBuildAction)} {Command}";
        }
    }
}
