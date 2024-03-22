using Domain.Entities;

namespace Domain.Interfaces.States
{
    public interface IBacklogItemState
    {
        Item Context { get; init; }
        void DevelopBacklogItem();
        void FinalizeDevelopmentBacklogItem();
        void TestingBacklogItem();
        void FinalizeTestingBacklogItem();
        void DenyDevelopedBacklogItem();
        void DenyTestedBacklogItem();
        void FinalizeBacklogItem();
        void ReceiveFeedbackBacklogItem();
        void CloseBacklogItem();
    }
}
