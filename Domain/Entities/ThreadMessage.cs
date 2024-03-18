using System.Text;
using Domain.Helpers;
using Domain.States.BacklogItem;

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
                if (ValidateUpdate())
                {
                    _title = value;
                    Logger.DisplayUpdatedAlert(nameof(Title), _title);
                }
            } 
        }

        private string _body { get; set; }
        public string Body 
        { 
            get => _body;
            set
            {
                if (ValidateUpdate())
                {
                    _body = value;
                    Logger.DisplayUpdatedAlert(nameof(Body), _body);
                }
            }
        }

        private Thread _thread { get; init; }
        public Thread Thread { get => _thread; init => _thread = value; }

        public ThreadMessage(string title, string body, Thread thread)
        {
            _id = Guid.NewGuid();
            _title = title;
            _body = body;
            _thread = thread;
            
            Logger.DisplayCreatedAlert(nameof(ThreadMessage), _title);
        }

        public bool ValidateUpdate()
        {
            if (_thread.Item.CurrentStatus.GetType() == typeof(ClosedState))
            {
                Logger.DisplayCustomAlert(nameof(ThreadMessage), nameof(ValidateUpdate), "Can't update message when thread is closed.");
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"Id: {_id}");
            sb.AppendLine($"Title: {_title}");
            sb.AppendLine($"Body: {_body}");

            return sb.ToString();
        }
    }
}
