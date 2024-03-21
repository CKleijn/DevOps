using Domain.Helpers;
using Domain.Phases;

namespace Domain.Actions
{
    public class DotnetPublishAction : Entities.Action
    {
        public DotnetPublishAction() : base()
        {
            Command = "dotnet publish";
        }

        public override void ConnectToPhase()
        {
            Phase = new DeployPhase();
        }

        public override void Execute()
        {
            Logger.DisplayCustomAlert(nameof(DotnetPublishAction), nameof(Execute), $"Execute {Command}!");
            Logger.DisplayCustomAlert(nameof(DotnetPublishAction), nameof(Execute), $"Succesfully executed {Command} without any errors!");
        }

        public override void Print(int indentations)
        {
            ActionPrintTemplate.PrintTemplate(indentations, nameof(DotnetPublishAction), Command!);
        }
    }
}
