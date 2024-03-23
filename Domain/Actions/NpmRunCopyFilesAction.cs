using Domain.Helpers;
using Domain.Phases;

namespace Domain.Actions
{
    public class NpmRunCopyFilesAction : Entities.Action
    {
        public NpmRunCopyFilesAction() : base()
        {
            Command = "npm run copy files";
        }

        public override void ConnectToPhase()
        {
            Phase = new UtilityPhase();
        }

        public override void Execute()
        {
            Logger.DisplayCustomAlert(nameof(NpmRunCopyFilesAction), nameof(Execute), $"Execute {Command}!");
            Logger.DisplayCustomAlert(nameof(NpmRunCopyFilesAction), nameof(Execute), $"Successfully executed {Command} without any errors!");
        }

        public override string Print()
        {
            return $"{nameof(NpmRunCopyFilesAction)} {Command}";
        }
    }
}
