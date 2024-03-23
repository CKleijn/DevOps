namespace Domain.States.Pipeline;

public abstract class PipelineState
{
    public abstract Entities.Pipeline Context { get; init; }
    public virtual void ExecutePipeline() => throw new NotImplementedException();
    public virtual void CancelPipeline() => throw new NotImplementedException();

    public virtual void FailPipeline() => throw new NotImplementedException();

    public virtual void FinalizePipeline() => throw new NotImplementedException();
}