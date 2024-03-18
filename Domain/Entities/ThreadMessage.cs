using System.Text;
using Domain.Helpers;

namespace Domain.Entities
{
    public class ThreadMessage
    {
        private Guid _id { get; init; }
        public Guid Id { get => _id; init => _id = value; }

        private string _title { get; set; }
        public string Title 
        { 
            get => _title; 
            set {
                _title = value;
                Logger.DisplayUpdatedAlert(nameof(Title), _title);
            } 
        }

        private string _body { get; set; }
        public string Body 
        { 
            get => _body;
            set
            {
                _body = value;
                Logger.DisplayUpdatedAlert(nameof(Body), _body);
            }
        }

        private IList<ThreadMessage> _threadMessages { get; init; }
        public IList<ThreadMessage> ThreadMessages { get => _threadMessages; init => _threadMessages = value; }

        public ThreadMessage(string title, string body)
        {
            _id = Guid.NewGuid();
            _title = title;
            _body = body;
            _threadMessages = new List<ThreadMessage>();
            
            Logger.DisplayCreatedAlert(nameof(ThreadMessage), _title);
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
            sb.AppendLine($"Title: {_title}");
            sb.AppendLine($"Body: {_body}");
            sb.AppendLine($"ThreadMessages: {_threadMessages.Count}");

            return sb.ToString();
        }
    }
}
