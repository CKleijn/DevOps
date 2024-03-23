using Domain.Entities;
using Domain.Helpers;
using System.Text;

namespace Domain.Phases
{
    public class SourcePhase : Phase
    {
        public SourcePhase() : base()
        {
            SortIndex = 1;
        }

        public override string Print()
        {
            StringBuilder sb = new();

            sb.AppendLine(nameof(SourcePhase));

            foreach (var action in Actions)
            {
                sb.AppendLine(action.Print());
            }

            return sb.ToString();
        }
    }
}
