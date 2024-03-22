using Domain.Helpers;

namespace Infrastructure.Libraries.Notification
{
    public class Slack
    {
        public void SendSlack(string text, string recipientId)
        {
            Logger.DisplayCustomAlert(nameof(Slack), nameof(SendSlack), $"Send slack to {recipientId} with text ({text})");
        }
    }
}