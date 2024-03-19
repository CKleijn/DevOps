using Domain.Helpers;

namespace Infrastructure.Libraries.Notification
{
    public class Mail
    {
        public void SendMail(string title, string body, string recipientMail, string senderMail)
        {
            Logger.DisplayCustomAlert(nameof(Mail), nameof(SendMail), $"Send mail to {recipientMail} from {senderMail} with title ({title}) and body ({body})");
        }
    }
}

