using Domain.Entities;
using Domain.Helpers;

namespace Domain.Phases
{
    public class AnalysePhase : Phase
    {
        public AnalysePhase() : base()
        {
            SortIndex = 5;
        }

        public override void Print(int indentations)
        {
            Logger.DisplayCustomAlert(new string('\t', indentations) + nameof(AnalysePhase), null, nameof(AnalysePhase));

            if (Actions.Count > 0)
            {
                foreach (var action in Actions)
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
