using Domain.Helpers;
using Domain.Phases;

namespace Domain.Actions
{
    public class DotnetBuildAction : Entities.Action
    {
        public DotnetBuildAction() : base()
        {
            Command = "dotnet build";
        }

        public override void ConnectToPhase()
        {
            Phase = new BuildPhase();
        }

        public override void Execute()
        {
            Logger.DisplayCustomAlert(nameof(DotnetBuildAction), nameof(Execute), $"Execute {Command}!");
            Logger.DisplayCustomAlert(nameof(DotnetBuildAction), nameof(Execute), $"Successfully executed {Command} without any errors!");
        }

        public override string Print()
        {
            return $"{nameof(DotnetBuildAction)} {Command}";
        }
    }
}
