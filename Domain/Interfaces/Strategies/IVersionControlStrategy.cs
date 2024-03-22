namespace Domain.Interfaces.Strategies
{
    public interface IVersionControlStrategy
    {
        void CloneRepo(string url);
        void CommitChanges(string message);
        void PushChanges();
        void PullChanges();
    }
}
