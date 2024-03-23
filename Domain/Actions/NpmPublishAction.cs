using Domain.Helpers;
using Domain.Phases;

namespace Domain.Actions
{
    public class NpmPublishAction : Entities.Action
    {
        public NpmPublishAction() : base()
        {
            Command = "npm publish";
        }

        public override void ConnectToPhase()
        {
            Phase = new DeployPhase();
        }

        public override void Execute()
        {
            Logger.DisplayCustomAlert(nameof(NpmPublishAction), nameof(Execute), $"Execute {Command}!");
            Logger.DisplayCustomAlert(nameof(NpmPublishAction), nameof(Execute), $"Successfully executed {Command} without any errors!");
        }

        public override string Print()
        {
            return $"{nameof(NpmPublishAction)} {Command}";
        }
    }
}
