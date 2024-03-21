using Domain.Helpers;
using Domain.Phases;

namespace Domain.Actions
{
    public class DotnetTestAction : Entities.Action
    {
        public DotnetTestAction() : base()
        {
            Command = "dotnet restore";
        }

        public override void ConnectToPhase()
        {
            Phase = new TestPhase();
        }

        public override void Execute()
        {
            Logger.DisplayCustomAlert(nameof(DotnetTestAction), nameof(Execute), $"Execute {Command}!");
            Logger.DisplayCustomAlert(nameof(DotnetTestAction), nameof(Execute), $"Succesfully executed {Command} without any errors!");
        }

        public override void Print(int indentations)
        {
            ActionPrintTemplate.PrintTemplate(indentations, nameof(DotnetTestAction), Command!);
        }
    }
}
