using System.Text;

namespace Domain.Entities
{
    public class Item
    {
        private Guid _id {  get; init; }
        public Guid Id { get => _id; init => _id = value; }

        private string _title { get; set; }
        public string Title { get => _title; set => _title = value; }

        private string _description { get; set; }
        public string Description { get => _description; set => _description = value; }

        private Developer _developer { get; set; }
        public Developer Developer { get => _developer; set => _developer = value; }

        private IList<Activity> _activities { get; init; }
        public IList<Activity> Activities { get => _activities; init => _activities = value; }

        // TODO: Implement Status and Threads

        //private IList<Thread> _threads { get; init; }
        //public IList<Thread> Threads { get => _threads; init => _threads = value; }

        private int _storyPoints { get; set; }
        public int StoryPoints { get => _storyPoints; set => _storyPoints = value; }

        private DateTime? _updatedAt { get; set; }
        public DateTime? UpdatedAt { get => _updatedAt; set => _updatedAt = value; }

        private DateTime _createdAt { get; init; }
        public DateTime CreatedAt { get => _createdAt; init => _createdAt = value; }

        public Item(string title, string description, Developer developer, int storyPoints)
        {
            _id = Guid.NewGuid();
            _title = title;
            _description = description;
            _developer = developer;
            _activities = new List<Activity>();
            //_threads = new List<Thread>();
            _storyPoints = storyPoints;
            _createdAt = DateTime.Now;
        }

        //TODO: implement functions
        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"Id: {_id}");
            sb.AppendLine($"Title: {_title}");
            sb.AppendLine($"Description: {_description}");
            sb.AppendLine($"Developer: {_developer.ToString()}");
            sb.AppendLine($"Activities: {_activities.Count}");
            //sb.AppendLine($"Threads: {_threads.Count}");
            sb.AppendLine($"StoryPoints: {_storyPoints}");
            sb.AppendLine($"UpdatedAt: {_updatedAt}");
            sb.AppendLine($"CreatedAt: {_createdAt}");

            return sb.ToString();
        }
    }
}
