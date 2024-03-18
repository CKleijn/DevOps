using Domain.Helpers;

namespace Domain.Entities
{
    public class ReleasePipeline : Pipeline
    {
        public ReleasePipeline(string name) : base(name) 
        {
            Logger.DisplayCreatedAlert(nameof(ReleasePipeline), name);
        }

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
