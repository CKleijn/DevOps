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
            PhasePrintTemplate.PrintTemplate(indentations, nameof(TestPhase), Actions);
        }
    }
}
