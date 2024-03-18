using Domain.Helpers;

namespace Infrastructure.Libraries.VersionControls
{
    public class Subversion
    {
        public Subversion()
        {
            Logger.DisplayCustomAlert(nameof(Subversion), null, "Subversion integrated!");
        }

        public void CloneRepo(string url)
        {
            Logger.DisplayCustomAlert(nameof(Subversion), nameof(CloneRepo), $"Clone Subversion repo from {url}...");
            Logger.DisplayCustomAlert(nameof(Subversion), nameof(CloneRepo), "Succesfully cloned Subversion repo!");
        }

        public void CommitChanges(string message)
        {
            Logger.DisplayCustomAlert(nameof(Subversion), nameof(CommitChanges), $"Commit ({message}) to Subversion repo...");
            Logger.DisplayCustomAlert(nameof(Subversion), nameof(CommitChanges), "Succesfully committed to Subversion repo!");
        }

        public void PullChanges()
        {
            Logger.DisplayCustomAlert(nameof(Subversion), nameof(PullChanges), "Pull changes from Subversion repo...");
            Logger.DisplayCustomAlert(nameof(Subversion), nameof(PullChanges), "Succesfully pulled changes from Subversion repo!");
        }

        public void PushChanges()
        {
            Logger.DisplayCustomAlert(nameof(Subversion), nameof(PushChanges), "Push changes to Subversion repo...");
            Logger.DisplayCustomAlert(nameof(Subversion), nameof(PushChanges), "Succesfully pushed changed to Subversion repo!");
        }
    }
}
