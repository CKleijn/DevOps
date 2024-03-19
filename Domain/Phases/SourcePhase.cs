using Domain.Entities;
using Domain.Helpers;

namespace Domain.Phases
{
    public class SourcePhase : Phase
    {
        public SourcePhase() : base()
        {
            SortIndex = 1;
        }

        public override void Print(int indentations)
        {
            Logger.DisplayCustomAlert(new string('\t', indentations) + nameof(SourcePhase), null, nameof(SourcePhase));

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
