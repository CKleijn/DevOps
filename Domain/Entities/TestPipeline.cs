using Domain.Helpers;

namespace Domain.Entities
{
    public class TestPipeline : Pipeline
    {
        public TestPipeline(string name) : base(name)
        {
            Logger.DisplayCreatedAlert(nameof(TestPipeline), name);
        }

        protected override void Test()
        {
            // TODO: Forloop each test action
        }
    }
}
