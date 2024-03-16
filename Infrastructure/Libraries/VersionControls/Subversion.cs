namespace Infrastructure.Libraries.VersionControls
{
    public class Subversion
    {
        public Subversion()
        {
            Console.WriteLine("Subversion integrated!");
        }

        public void CloneRepo(string url)
        {
            Console.WriteLine($"Clone Subversion repo from {url}...");
            Console.WriteLine("Succesfully cloned Subversion repo!");
        }

        public void CommitChanges(string message)
        {
            Console.WriteLine($"Commit ({message}) to Subversion repo...");
            Console.WriteLine("Succesfully committed to Subversion repo!");
        }

        public void PullChanges()
        {
            Console.WriteLine("Pull changes from Subversion repo...");
            Console.WriteLine("Succesfully pulled changes from Subversion repo!");
        }

        public void PushChanges()
        {
            Console.WriteLine("Push changes to Subversion repo...");
            Console.WriteLine("Succesfully pushed changed to Subversion repo!");
        }
    }
}
