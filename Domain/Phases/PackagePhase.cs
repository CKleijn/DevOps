using Domain.Entities;
using Domain.Helpers;

namespace Domain.Phases
{
    public class PackagePhase : Phase
    {
        public PackagePhase() : base()
        {
            SortIndex = 2;
        }

        public override void Print(int indentations)
        {
            Logger.DisplayCustomAlert(new string('\t', indentations) + nameof(PackagePhase), null, nameof(PackagePhase));

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
