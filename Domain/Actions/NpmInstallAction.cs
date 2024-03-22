using Domain.Helpers;
using Domain.Phases;

namespace Domain.Actions
{
    public class NpmInstallAction : Entities.Action
    {
        public NpmInstallAction() : base()
        {
            Command = "npm install";
        }

        public override void ConnectToPhase()
        {
            Phase = new PackagePhase();
        }

        public override void Execute()
        {
            Logger.DisplayCustomAlert(nameof(NpmInstallAction), nameof(Execute), $"Execute {Command}!");
            Logger.DisplayCustomAlert(nameof(NpmInstallAction), nameof(Execute), $"Successfully executed {Command} without any errors!");
        }

        public override void Print(int indentations)
        {
            ActionPrintTemplate.PrintTemplate(indentations, nameof(NpmInstallAction), Command!);
        }
    }
}
