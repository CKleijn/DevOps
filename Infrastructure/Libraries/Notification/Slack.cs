using Domain.Helpers;

namespace Infrastructure.Libraries.Notification
{
    public class Slack
    {
        public void SendSlack(string text, string recipientId, string senderId)
        {
            Logger.DisplayCustomAlert(nameof(Mail), nameof(SendSlack), $"Send slack to {recipientId} from {senderId} with text ({text})");
        }
    }
}