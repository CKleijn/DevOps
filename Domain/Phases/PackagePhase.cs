using Domain.Entities;
using Domain.Helpers;
using System.Text;

namespace Domain.Phases
{
    public class PackagePhase : Phase
    {
        public PackagePhase() : base()
        {
            SortIndex = 2;
        }

        public override string Print()
        {
            StringBuilder sb = new();

            sb.AppendLine(nameof(PackagePhase));

            foreach (var action in Actions)
            {
                sb.AppendLine(action.Print());
            }

            return sb.ToString();
        }
    }
}
