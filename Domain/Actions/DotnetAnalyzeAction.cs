using Domain.Helpers;
using Domain.Phases;

namespace Domain.Actions
{
    public class DotnetAnalyzeAction : Entities.Action
    {
        public DotnetAnalyzeAction() : base()
        {
            Command = "dotnet analyze";
        }

        public override void ConnectToPhase()
        {
            Phase = new AnalysePhase();
        }

        public override void Execute()
        {
            Logger.DisplayCustomAlert(nameof(DotnetAnalyzeAction), nameof(Execute), $"Execute {Command}");
            Logger.DisplayCustomAlert(nameof(DotnetAnalyzeAction), nameof(Execute), $"Succesfully executed {Command} without any errors!");
        }

        public override void Print(int indentations)
        {
            Logger.DisplayCustomAlert(new string('\t', indentations) + nameof(DotnetAnalyzeAction), null, Command!);
        }
    }
}
