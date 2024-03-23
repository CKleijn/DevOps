using Domain.Entities;
using Domain.Helpers;
using System.Text;

namespace Domain.Phases
{
    public class BuildPhase : Phase
    {
        public BuildPhase() : base()
        {
            SortIndex = 3;
        }

        public override string Print()
        {
            StringBuilder sb = new();

            sb.AppendLine(nameof(BuildPhase));

            foreach (var action in Actions)
            {
                sb.AppendLine(action.Print());
            }

            return sb.ToString();
        }
    }
}
