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
            PhasePrintTemplate.PrintTemplate(indentations, nameof(SourcePhase), Actions);
        }
    }
}
