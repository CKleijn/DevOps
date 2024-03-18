namespace Domain.Helpers
{
    public static class Logger
    {
        public static void DisplayCreatedAlert(string property, string title) => Console.WriteLine($"{property} {title} has been created!");
        public static void DisplayUpdatedAlert(string property, string title) => Console.WriteLine($"{property} from {title} has been updated!");
        public static void DisplayCustomAlert(string property, string? action, string message) => Console.WriteLine(action == null ? $"{property} {message}" : $"{property} ({action}) {message}");
    }
}
