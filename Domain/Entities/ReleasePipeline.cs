namespace Domain.Entities
{
    public class ReleasePipeline : Pipeline
    {
        public ReleasePipeline(string name, User creator) : base(name, creator) { }

        protected override void Test()
        {
            // TODO: Forloop each test action
        }

        protected override void Deploy()
        {
            // TODO: Forloop each deploy action
        }
    }
}
