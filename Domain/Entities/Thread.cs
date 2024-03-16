using System.Text;

namespace Domain.Entities
{
    public class Thread
    {
        private Guid _id { get; init; }
        public Guid Id { get => _id; init => _id = value; }

        private string _subject { get; set; }
        public string Subject { get => _subject; set => _subject = value; }

        private string _description { get; set; }
        public string Description { get => _description; set => _description = value; }

        private User _creator { get; init; }
        public User Creator { get => _creator; init => _creator = value; }

        private IList<ThreadMessage> _threadMessages { get; init; }
        public IList<ThreadMessage> ThreadMessages { get => _threadMessages; init => _threadMessages = value; }

        private DateTime? _updatedAt { get; set; }
        public DateTime? UpdatedAt { get => _updatedAt; set => _updatedAt = value; }

        private DateTime _createdAt { get; init; }
        public DateTime CreatedAt { get => _createdAt; init => _createdAt = value; }

        public Thread(string title, string body, User creator)
        {
            _id = Guid.NewGuid();
            _subject = title;
            _description = body;
            _creator = creator;
            _createdAt = DateTime.Now;
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"Id: {_id}");
            sb.AppendLine($"Subject: {_subject}");
            sb.AppendLine($"Description: {_description}");
            sb.AppendLine($"Creator: {_creator.ToString()}");
            sb.AppendLine($"ThreadMessages: {_threadMessages.Count}");
            sb.AppendLine($"UpdatedAt: {_updatedAt}");
            sb.AppendLine($"CreatedAt: {_createdAt}");

            return sb.ToString();
        }
    }
}
