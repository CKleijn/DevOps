using System.Text;

namespace Domain.Entities
{
    public class ThreadMessage
    {
        private Guid _id { get; init; }
        public Guid Id { get => _id; init => _id = value; }

        private string _title { get; set; }
        public string Title { get => _title; set => _title = value; }

        private string _body { get; set; }
        public string Body { get => _body; set => _body = value; }

        private User _creator { get; init; }
        public User Creator { get => _creator; init => _creator = value; }

        private IList<ThreadMessage> _threadMessages { get; init; }
        public IList<ThreadMessage> ThreadMessages { get => _threadMessages; init => _threadMessages = value; }

        private DateTime? _updatedAt { get; set; }
        public DateTime? UpdatedAt { get => _updatedAt; set => _updatedAt = value; }

        private DateTime _createdAt { get; init; }
        public DateTime CreatedAt { get => _createdAt; init => _createdAt = value; }

        public ThreadMessage(string title, string body, User creator)
        {
            _id = Guid.NewGuid();
            _title = title;
            _body = body;
            _creator = creator;
            _threadMessages = new List<ThreadMessage>();
            _createdAt = DateTime.Now;
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"Id: {_id}");
            sb.AppendLine($"Title: {_title}");
            sb.AppendLine($"Body: {_body}");
            sb.AppendLine($"Creator: {_creator.ToString()}");
            sb.AppendLine($"ThreadMessages: {_threadMessages.Count}");
            sb.AppendLine($"UpdatedAt: {_updatedAt}");
            sb.AppendLine($"CreatedAt: {_createdAt}");

            return sb.ToString();
        }
    }
}
