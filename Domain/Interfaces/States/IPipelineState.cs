namespace Domain.Interfaces.States
{
    public interface IPipelineState
    {
        void ExecutePipeline();
        void CancelPipeline();
        void FailPipeline();
        void FinalizePipeline();
    }
}
