using Domain.Entities;
using Domain.Helpers;

namespace Domain.Phases
{
    public class BuildPhase : Phase
    {
        public BuildPhase() : base()
        {
            SortIndex = 3;
        }

        public override void Print(int indentations)
        {
            Logger.DisplayCustomAlert(new string('\t', indentations) + nameof(BuildPhase), null, nameof(BuildPhase));

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
