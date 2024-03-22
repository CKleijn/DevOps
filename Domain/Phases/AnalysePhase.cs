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
            PhasePrintTemplate.PrintTemplate(indentations, nameof(AnalysePhase), Actions);
        }
    }
}
