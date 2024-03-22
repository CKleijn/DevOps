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
            PhasePrintTemplate.PrintTemplate(indentations, nameof(BuildPhase), Actions);
        }
    }
}
