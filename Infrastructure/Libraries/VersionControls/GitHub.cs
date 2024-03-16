using Domain.Interfaces.Strategies;

namespace Infrastructure.Libraries.VersionControls
{
    public class GitHub : IVersionControlStrategy
    {
        public GitHub()
        {
            Console.WriteLine("GitHub integrated!");
        }

        public void CloneRepo(string url)
        {
            Console.WriteLine($"Clone GitHub repo from {url}...");
            Console.WriteLine("Succesfully cloned GitHub repo!");
        }

        public void CommitChanges(string message)
        {
            Console.WriteLine($"Commit ({message}) to GitHub repo...");
            Console.WriteLine("Succesfully committed to GitHub repo!");
        }

        public void PullChanges()
        {
            Console.WriteLine("Pull changes from GitHub repo...");
            Console.WriteLine("Succesfully pulled changes from GitHub repo!");
        }

        public void PushChanges()
        {
            Console.WriteLine("Push changes to GitHub repo...");
            Console.WriteLine("Succesfully pushed changed to GitHub repo!");
        }
    }
}
