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
            Logger.DisplayCustomAlert(nameof(DotnetRestoreAction), nameof(Execute), $"Execute {Command}!");
            Logger.DisplayCustomAlert(nameof(DotnetRestoreAction), nameof(Execute), $"Successfully executed {Command} without any errors!");
        }

        public override string Print()
        {
            return $"{nameof(DotnetRestoreAction)} {Command}";
        }
    }
}
