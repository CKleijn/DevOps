namespace Domain.Helpers
{
    public static class ActionPrintTemplate
    {
        public static void PrintTemplate(int indentations, string action, string command)
        {
            Logger.DisplayCustomAlert(new string('\t', indentations) + action, null, command!);
        }
    }
}
