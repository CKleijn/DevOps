using Domain.Helpers;
using Domain.Interfaces.Strategies;

namespace Infrastructure.Libraries.VersionControls
{
    public class GitHub : IVersionControlStrategy
    {
        public GitHub()
        {
            Logger.DisplayCustomAlert(nameof(GitHub), null, "GitHub integrated!");
        }

        public void CloneRepo(string url)
        {
            Logger.DisplayCustomAlert(nameof(GitHub), nameof(CloneRepo), $"Clone GitHub repo from {url}...");
            Logger.DisplayCustomAlert(nameof(GitHub), nameof(CloneRepo), "Succesfully cloned GitHub repo!");
        }

        public void CommitChanges(string message)
        {
            Logger.DisplayCustomAlert(nameof(GitHub), nameof(CommitChanges), $"Commit ({message}) to GitHub repo...");
            Logger.DisplayCustomAlert(nameof(GitHub), nameof(CommitChanges), "Succesfully committed to GitHub repo!");
        }

        public void PullChanges()
        {
            Logger.DisplayCustomAlert(nameof(GitHub), nameof(PullChanges), "Pull changes from GitHub repo...");
            Logger.DisplayCustomAlert(nameof(GitHub), nameof(PullChanges), "Succesfully pulled changes from GitHub repo!");
        }

        public void PushChanges()
        {
            Logger.DisplayCustomAlert(nameof(GitHub), nameof(PushChanges), "Push changes to GitHub repo...");
            Logger.DisplayCustomAlert(nameof(GitHub), nameof(PushChanges), "Succesfully pushed changed to GitHub repo!");
        }
    }
}
