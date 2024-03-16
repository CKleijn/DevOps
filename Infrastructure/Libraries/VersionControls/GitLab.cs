using Domain.Interfaces.Strategies;

namespace Infrastructure.Libraries.VersionControls
{
    public class GitLab : IVersionControlStrategy
    {
        public GitLab()
        {
            Console.WriteLine("GitLab integrated!");
        }

        public void CloneRepo(string url)
        {
            Console.WriteLine($"Clone GitLab repo from {url}...");
            Console.WriteLine("Succesfully cloned GitLab repo!");
        }

        public void CommitChanges(string message)
        {
            Console.WriteLine($"Commit ({message}) to GitLab repo...");
            Console.WriteLine("Succesfully committed to GitLab repo!");
        }

        public void PullChanges()
        {
            Console.WriteLine("Pull changes from GitLab repo...");
            Console.WriteLine("Succesfully pulled changes from GitLab repo!");
        }

        public void PushChanges()
        {
            Console.WriteLine("Push changes to GitLab repo...");
            Console.WriteLine("Succesfully pushed changed to GitLab repo!");
        }
    }
}
