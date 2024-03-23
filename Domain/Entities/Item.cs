using Domain.Helpers;
using Domain.States.BacklogItem;
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

        private BacklogItemState? _previousStatus { get; set; }
        public BacklogItemState? PreviousStatus { get => _previousStatus; set => _previousStatus = value; }

        private BacklogItemState _currentStatus { get; set; }

        public BacklogItemState CurrentStatus
        {
            get => _currentStatus;
            set
            {
                _previousStatus = _currentStatus;
                _currentStatus = value;
            }
        }

        private IList<Thread> _threads { get; init; }
        public IList<Thread> Threads { get => _threads; init => _threads = value; }

        private int _storyPoints { get; set; }
        public int StoryPoints { get => _storyPoints; set => _storyPoints = value; }
        
        private SprintBacklog? _sprintBacklog { get; set; }
        public SprintBacklog? SprintBacklog { get => _sprintBacklog; set => _sprintBacklog = value; }
        
        public Item(string title, string description, Developer developer, int storyPoints, SprintBacklog sprintBacklog)
        {
            _id = Guid.NewGuid();
            _title = title;
            _description = description;
            _developer = developer;
            _activities = new List<Activity>();
            _previousStatus = new TodoState(this);
            _currentStatus = new TodoState(this);
            _threads = new List<Thread>();
            _storyPoints = storyPoints;
            _sprintBacklog = sprintBacklog;

            Logger.DisplayCreatedAlert(nameof(Item), _id.ToString());
        }
        
        public Item(string title, string description, Developer developer, int storyPoints)
        {
            _id = Guid.NewGuid();
            _title = title;
            _description = description;
            _developer = developer;
            _activities = new List<Activity>();
            _previousStatus = new TodoState(this);
            _currentStatus = new TodoState(this);
            _threads = new List<Thread>();
            _storyPoints = storyPoints;

            Logger.DisplayCreatedAlert(nameof(Item), _id.ToString());
        }

        public void AddThreadToItem(Thread thread)
        {
            if (_currentStatus.GetType() != typeof(ClosedState))
            {
                _threads.Add(thread);
            }

            Logger.DisplayAddedAlert(nameof(Item), thread.Subject);
        }

        public void RemoveThreadFromItem(Thread thread)
        {
            if (_currentStatus.GetType() != typeof(ClosedState))
            {
                _threads.Remove(thread);
            }

            Logger.DisplayRemovedAlert(nameof(Item), thread.Subject);
        }

        public void AddActivityToItem(Activity activity) 
        {
            if (_currentStatus.GetType() == typeof(TodoState) || _currentStatus.GetType() == typeof(DoingState))
            {
                _activities.Add(activity);
            }

            Logger.DisplayAddedAlert(nameof(Item), activity.Title);
        }

        public void RemoveActivityFromItem(Activity activity)
        {
            if (_currentStatus.GetType() == typeof(TodoState) || _currentStatus.GetType() == typeof(DoingState))
            {
                _activities.Remove(activity);
            }

            Logger.DisplayRemovedAlert(nameof(Item), activity.Title);
        }

        public void DevelopBacklogItem() => _currentStatus.DevelopBacklogItem();
        public void FinalizeDevelopmentBacklogItem() => _currentStatus.FinalizeDevelopmentBacklogItem();
        public void TestingBacklogItem() => _currentStatus.TestingBacklogItem();
        public void DenyDevelopedBacklogItem() => _currentStatus.DenyDevelopedBacklogItem();
        public void FinalizeTestingBacklogItem() => _currentStatus.FinalizeTestingBacklogItem();
        public void DenyTestedBacklogItem() => _currentStatus.DenyTestedBacklogItem();
        public void FinalizeBacklogItem() => _currentStatus.FinalizeBacklogItem();
        public void ReceiveFeedback() => _currentStatus.ReceiveFeedbackBacklogItem();
        public void CloseBacklogItem() => _currentStatus.CloseBacklogItem();
        
        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"Id: {_id}");
            sb.AppendLine($"Title: {_title}");
            sb.AppendLine($"Description: {_description}");
            sb.AppendLine($"Developer: {_developer.ToString()}");
            sb.AppendLine($"StoryPoints: {_storyPoints}");
            sb.AppendLine($"PreviousStatus: {_previousStatus?.GetType().Name}");
            sb.AppendLine($"CurrentStatus: {_currentStatus.GetType().Name}");
            sb.AppendLine($"Activities: {_activities.Count}");

            foreach (var activity in _activities)
            {
                sb.AppendLine(activity.ToString());
            }

            sb.AppendLine($"Threads: {_threads.Count}");

            foreach (var thread in _threads)
            {
                sb.AppendLine(thread.ToString());
            }

            return sb.ToString();
        }
    }
}
