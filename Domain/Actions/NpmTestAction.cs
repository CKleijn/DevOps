using Domain.Helpers;
using Domain.Phases;

namespace Domain.Actions
{
    public class NpmTestAction : Entities.Action
    {
        public NpmTestAction() : base()
        {
            Command = "dotnet restore";
        }

        public override void ConnectToPhase()
        {
            Phase = new TestPhase();
        }

        public override void Execute()
        {
            Logger.DisplayCustomAlert(nameof(NpmTestAction), nameof(Execute), $"Execute {Command}!");
            Logger.DisplayCustomAlert(nameof(NpmTestAction), nameof(Execute), $"Successfully executed {Command} without any errors!");
        }

        public override void Print(int indentations)
        {
            ActionPrintTemplate.PrintTemplate(indentations, nameof(NpmTestAction), Command!);
        }
    }
}
