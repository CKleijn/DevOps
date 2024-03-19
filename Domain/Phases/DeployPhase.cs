using Domain.Entities;
using Domain.Helpers;

namespace Domain.Phases
{
    public class DeployPhase : Phase
    {
        public DeployPhase() : base()
        {
            SortIndex = 6;
        }

        public override void Print(int indentations)
        {
            Logger.DisplayCustomAlert(new string('\t', indentations) + nameof(DeployPhase), null, nameof(DeployPhase));

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
