using Domain.Entities;

namespace Domain.Interfaces.States;

public interface ISprintState
{
    Sprint Context { get; init; }
    void ExecuteSprint();
    void FinishSprint();
    void ReleaseSprint();
    void ReviewSprint();
    void CancelSprint();
}