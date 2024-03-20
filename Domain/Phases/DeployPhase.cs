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
            PhasePrintTemplate.PrintTemplate(indentations, nameof(DeployPhase), Actions);
        }
    }
}
