using Domain.Helpers;
using Domain.Interfaces.Strategies;

namespace Infrastructure.Libraries.VersionControls
{
    public class GitLab : IVersionControlStrategy
    {
        public GitLab()
        {
            Logger.DisplayCustomAlert(nameof(GitLab), null, "GitLab integrated!");
        }

        public void CloneRepo(string url)
        {
            Logger.DisplayCustomAlert(nameof(GitLab), nameof(CloneRepo), $"Clone GitLab repo from {url}...");
            Logger.DisplayCustomAlert(nameof(GitLab), nameof(CloneRepo), "Succesfully cloned GitLab repo!");
        }

        public void CommitChanges(string message)
        {
            Logger.DisplayCustomAlert(nameof(GitLab), nameof(CommitChanges), $"Commit ({message}) to GitLab repo...");
            Logger.DisplayCustomAlert(nameof(GitLab), nameof(CommitChanges), "Succesfully committed to GitLab repo!");
        }

        public void PullChanges()
        {
            Logger.DisplayCustomAlert(nameof(GitLab), nameof(PullChanges), "Pull changes from GitLab repo...");
            Logger.DisplayCustomAlert(nameof(GitLab), nameof(PullChanges), "Succesfully pulled changes from GitLab repo!");
        }

        public void PushChanges()
        {
            Logger.DisplayCustomAlert(nameof(GitLab), nameof(PushChanges), "Push changes to GitLab repo...");
            Logger.DisplayCustomAlert(nameof(GitLab), nameof(PushChanges), "Succesfully pushed changed to GitLab repo!");
        }
    }
}
