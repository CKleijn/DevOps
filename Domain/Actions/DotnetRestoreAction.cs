using Domain.Helpers;
using Domain.Phases;

namespace Domain.Actions
{
    public class DotnetRestoreAction : Entities.Action
    {
        public DotnetRestoreAction() : base()
        {
            Command = "dotnet restore";
        }

        public override void ConnectToPhase()
        {
            Phase = new PackagePhase();
        }

        public override void Execute()
        {
            Logger.DisplayCustomAlert(nameof(DotnetRestoreAction), nameof(Execute), $"Execute {Command}");
            Logger.DisplayCustomAlert(nameof(DotnetRestoreAction), nameof(Execute), $"Succesfully executed {Command} without any errors!");
        }

        public override void Print(int indentations)
        {
            Logger.DisplayCustomAlert(new string('\t', indentations) + nameof(DotnetRestoreAction), null, Command!);
        }
    }
}
