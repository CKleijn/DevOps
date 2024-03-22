using Domain.Helpers;

namespace Infrastructure.Libraries.Notification
{
    public class Mail
    {
        public void SendMail(string title, string body, string recipientMail)
        {
            Logger.DisplayCustomAlert(nameof(Mail), nameof(SendMail), $"Send mail to {recipientMail} with title ({title}) and body ({body})");
        }
    }
}

