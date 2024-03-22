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
            PhasePrintTemplate.PrintTemplate(indentations, nameof(PackagePhase), Actions);
        }
    }
}
