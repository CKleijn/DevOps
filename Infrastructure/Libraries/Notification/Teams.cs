using Domain.Helpers;

namespace Infrastructure.Libraries.Notification
{
    public class Teams
    {
        public void SendTeamsMessage(string text, string recipientId)
        {
            Logger.DisplayCustomAlert(nameof(Mail), nameof(SendTeamsMessage), $"Send Teams message to {recipientId} with text ({text})");
        }
    }
}

