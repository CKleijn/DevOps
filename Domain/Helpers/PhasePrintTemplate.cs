using Domain.Interfaces.Composite;

namespace Domain.Helpers
{
    public static class PhasePrintTemplate
    {
        public static void PrintTemplate(int indentations, string phase, IList<IPipeline> actions)
        {
            Logger.DisplayCustomAlert(new string('\t', indentations) + phase, null, phase);

            if (actions.Count > 0)
            {
                foreach (var action in actions)
                {
                    action.Print(1);
                }
            }
            else
            {
                Logger.DisplayNotSelected();
            }
        }
    }
}
