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
            PhasePrintTemplate.PrintTemplate(indentations, nameof(UtilityPhase), Actions);
        }
    }
}
