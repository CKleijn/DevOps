using Domain.Entities;

public abstract class BacklogItemState
{
    public abstract Item Context { get; init; }
    
    public virtual void DevelopBacklogItem() => throw new NotImplementedException();

    public virtual void FinalizeDevelopmentBacklogItem() => throw new NotImplementedException();

    public virtual void TestingBacklogItem() => throw new NotImplementedException();

    public virtual void DenyDevelopedBacklogItem() => throw new NotImplementedException();

    public virtual void FinalizeTestingBacklogItem() => throw new NotImplementedException();

    public virtual void DenyTestedBacklogItem() => throw new NotImplementedException();

    public virtual void FinalizeBacklogItem() => throw new NotImplementedException();

    public virtual void ReceiveFeedbackBacklogItem() => throw new NotImplementedException();

    public virtual void CloseBacklogItem() => throw new NotImplementedException();
}