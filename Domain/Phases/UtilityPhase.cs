using Domain.Entities;
using Domain.Helpers;

namespace Domain.Phases
{
    public class UtilityPhase : Phase
    {
        public UtilityPhase() : base()
        {
            SortIndex = 7;
        }

        public override void Print(int indentations)
        {
            Logger.DisplayCustomAlert(new string('\t', indentations) + nameof(UtilityPhase), null, nameof(UtilityPhase));

            if (Actions.Count > 0 )
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
