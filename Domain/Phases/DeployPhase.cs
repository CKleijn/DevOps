using Domain.Entities;
using Domain.Helpers;
using System.Text;

namespace Domain.Phases
{
    public class DeployPhase : Phase
    {
        public DeployPhase() : base()
        {
            SortIndex = 6;
        }

        public override string Print()
        {
            StringBuilder sb = new();

            sb.AppendLine(nameof(DeployPhase));

            foreach (var action in Actions)
            {
                sb.AppendLine(action.Print());
            }

            return sb.ToString();
        }
    }
}
