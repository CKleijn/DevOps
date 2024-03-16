namespace Domain.Entities
{
    public class TestPipeline : Pipeline
    {
        public TestPipeline(string name, User creator) : base(name, creator) { }

        protected override void Test()
        {
            // TODO: Forloop each test action
        }
    }
}
