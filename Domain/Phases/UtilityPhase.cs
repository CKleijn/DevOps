using Domain.Entities;
using Domain.Helpers;
using System.Text;

namespace Domain.Phases
{
    public class UtilityPhase : Phase
    {
        public UtilityPhase() : base()
        {
            SortIndex = 7;
        }

        public override string Print()
        {
            StringBuilder sb = new();

            sb.AppendLine(nameof(UtilityPhase));

            foreach (var action in Actions)
            {
                sb.AppendLine(action.Print());
            }

            return sb.ToString();
        }
    }
}
