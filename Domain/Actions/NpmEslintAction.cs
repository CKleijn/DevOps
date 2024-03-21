using Domain.Helpers;
using Domain.Phases;

namespace Domain.Actions
{
    public class NpmEslintAction : Entities.Action
    {
        public NpmEslintAction() : base()
        {
            Command = "npm eslint";
        }

        public override void ConnectToPhase()
        {
            Phase = new AnalysePhase();
        }

        public override void Execute()
        {
            Logger.DisplayCustomAlert(nameof(NpmEslintAction), nameof(Execute), $"Execute {Command}!");
            Logger.DisplayCustomAlert(nameof(NpmEslintAction), nameof(Execute), $"Succesfully executed {Command} without any errors!");
        }

        public override void Print(int indentations)
        {
            ActionPrintTemplate.PrintTemplate(indentations, nameof(NpmEslintAction), Command!);
        }
    }
}
