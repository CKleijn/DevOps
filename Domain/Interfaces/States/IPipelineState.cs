using Domain.Entities;

namespace Domain.Interfaces.States
{
    public interface IPipelineState
    {
        Pipeline Context { get; init; }
        void ExecutePipeline();
        void CancelPipeline();
        void FailPipeline();
        void FinalizePipeline();
    }
}
