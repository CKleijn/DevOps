using System.Text;
using Domain.Helpers;

namespace Domain.Entities
{
    public class Thread
    {
        private Guid _id { get; init; }
        public Guid Id { get => _id; init => _id = value; }

        private string _subject { get; set; }

        public string Subject
        {
            get => _subject;
            set
            {
                _subject = value;
                Logger.DisplayUpdatedAlert(nameof(Subject), _subject);
            }
        }

        private string _description { get; set; }

        public string Description
        {
            get => _description; 
            set
            {
                _description = value;
                Logger.DisplayUpdatedAlert(nameof(Description), _description);
            }
        }

        private IList<ThreadMessage> _threadMessages { get; init; }
        public IList<ThreadMessage> ThreadMessages { get => _threadMessages; init => _threadMessages = value; }

        public Thread(string title, string body)
        {
            _id = Guid.NewGuid();
            _subject = title;
            _description = body;
            
            Logger.DisplayCreatedAlert(nameof(Thread), _subject);
        }
        
        public void AddThreadMessage(ThreadMessage threadMessage)
        {
            _threadMessages.Add(threadMessage);
            
            Logger.DisplayUpdatedAlert(nameof(ThreadMessages), $"Added: {threadMessage}");
        }
    
        public void RemoveThreadMessage(ThreadMessage threadMessage)
        {
            _threadMessages.Remove(threadMessage);
            Logger.DisplayUpdatedAlert(nameof(ThreadMessages), $"Removed: {threadMessage}");
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"Id: {_id}");
            sb.AppendLine($"Subject: {_subject}");
            sb.AppendLine($"Description: {_description}");
            sb.AppendLine($"ThreadMessages: {_threadMessages.Count}");

            return sb.ToString();
        }
    }
}
