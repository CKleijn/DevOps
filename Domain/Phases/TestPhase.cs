using Domain.Entities;
using Domain.Helpers;
using System.Text;

namespace Domain.Phases
{
    public class TestPhase : Phase
    {
        public TestPhase() : base()
        {
            SortIndex = 4;
        }

        public override string Print()
        {
            StringBuilder sb = new();

            sb.AppendLine(nameof(TestPhase));

            foreach (var action in Actions)
            {
                sb.AppendLine(action.Print());
            }

            return sb.ToString();
        }
    }
}
