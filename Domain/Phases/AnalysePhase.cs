using Domain.Entities;
using System.Text;

namespace Domain.Phases
{
    public class AnalysePhase : Phase
    {
        public AnalysePhase() : base()
        {
            SortIndex = 5;
        }

        public override string Print()
        {
            StringBuilder sb = new();

            sb.AppendLine(nameof(AnalysePhase));

            foreach (var action in Actions)
            {
                sb.AppendLine(action.Print());
            }

            return sb.ToString();
        }
    }
}
