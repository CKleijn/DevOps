using Domain.Helpers;
using Domain.Phases;

namespace Domain.Actions
{
    public class GitCloneAction : Entities.Action
    {
        public GitCloneAction() : base() 
        {
            Command = "git clone";
        }

        public override void ConnectToPhase()
        {
            Phase = new SourcePhase();
        }

        public override void Execute()
        {
            Logger.DisplayCustomAlert(nameof(GitCloneAction), nameof(Execute), $"Execute {Command}!");
            Logger.DisplayCustomAlert(nameof(GitCloneAction), nameof(Execute), $"Successfully executed {Command} without any errors!");
        }

        public override void Print(int indentations)
        {
            ActionPrintTemplate.PrintTemplate(indentations, nameof(GitCloneAction), Command!);
        }
    }
}
