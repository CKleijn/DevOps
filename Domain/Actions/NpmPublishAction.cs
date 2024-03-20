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
            Logger.DisplayCustomAlert(nameof(NpmPublishAction), nameof(Execute), $"Succesfully executed {Command} without any errors!");
        }

        public override void Print(int indentations)
        {
            Logger.DisplayCustomAlert(new string('\t', indentations) + nameof(NpmPublishAction), null, Command!);
        }
    }
}
