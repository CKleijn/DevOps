using Domain.Entities;
using Domain.Helpers;

namespace Domain.Phases
{
    public class TestPhase : Phase
    {
        public TestPhase() : base()
        {
            SortIndex = 4;
        }

        public override void Print(int indentations)
        {
            Logger.DisplayCustomAlert(new string('\t', indentations) + nameof(TestPhase), null, nameof(TestPhase));

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
