namespace Domain.Helpers
{
    public static class Logger
    {
        public static void ShowCreatedAlert(string property, string title) => Console.WriteLine($"{property} {title} has been created!");
        public static void ShowUpdatedAlert(string property, string title) => Console.WriteLine($"{property} from {title} has been updated!");
    }
}
