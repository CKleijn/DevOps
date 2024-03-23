namespace Domain.States.Sprint;

public abstract class SprintState
{
    public abstract Entities.Sprint Context { get; init; }
    
    public virtual void InitializeSprint() => throw new NotImplementedException();
    
    public virtual void ExecuteSprint() => throw new NotImplementedException();
    
    public virtual void FinishSprint() => throw new NotImplementedException();
    
    public virtual void ReleaseSprint() => throw new NotImplementedException();
    
    public virtual void ReviewSprint() => throw new NotImplementedException();
    
    public virtual void CancelSprint() => throw new NotImplementedException();
    
    public virtual void CloseSprint() => throw new NotImplementedException();
}