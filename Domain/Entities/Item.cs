using Domain.Helpers;
using Domain.Interfaces.States;
using Domain.States.BacklogItem;
using System.Diagnostics;
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

        private IBacklogItemState? _previousStatus { get; set; }
        public IBacklogItemState? PreviousStatus { get => _previousStatus; set => _previousStatus = value; }

        private IBacklogItemState _currentStatus { get; set; }

        public IBacklogItemState CurrentStatus
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
            _currentStatus = new TodoState(this);
            _threads = new List<Thread>();
            _storyPoints = storyPoints;
            _sprintBacklog = sprintBacklog;

            Logger.DisplayCreatedAlert(nameof(Item), _title);
        }
        
        public Item(string title, string description, Developer developer, int storyPoints)
        {
            _id = Guid.NewGuid();
            _title = title;
            _description = description;
            _developer = developer;
            _activities = new List<Activity>();
            _currentStatus = new TodoState(this);
            _threads = new List<Thread>();
            _storyPoints = storyPoints;

            Logger.DisplayCreatedAlert(nameof(Item), _title);
        }

        public void AddThreadToItem(Thread thread)
        {
            if (_currentStatus.GetType() != typeof(DoneState) || _currentStatus.GetType() != typeof(ClosedState))
            {
                _threads.Add(thread);
            }

            Logger.DisplayUpdatedAlert(nameof(Item), $"Added: {thread.Subject}");
        }

        public void RemoveThreadToItem(Thread thread)
        {
            if (_currentStatus.GetType() != typeof(DoneState) || _currentStatus.GetType() != typeof(ClosedState))
            {
                _threads.Remove(thread);
            }

            Logger.DisplayUpdatedAlert(nameof(Item), $"Removed: {thread.Subject}");
        }

        public void AddActivityToItem(Activity activity) 
        {
            _activities.Add(activity);

            Logger.DisplayUpdatedAlert(nameof(Item), $"Added: {activity.Title}");
        }

        public void RemovedActivityToItem(Activity activity)
        {
            _activities.Remove(activity);

            Logger.DisplayUpdatedAlert(nameof(Item), $"Removed: {activity.Title}");
        }

        public void CloseItem()
        {
            if (_currentStatus.GetType() == typeof(DoneState))
            {
                return;
            }

            _currentStatus.CloseBacklogItem();

            // Send notification to product owner

            Logger.DisplayCustomAlert(nameof(Item), nameof(CloseItem), $"Closed: {_title}");
        }
        
        //** Start State functions **//

        public void DevelopBacklogItem() => _currentStatus.DevelopBacklogItem();
        public void FinalizeDevelopmentBacklogItem() => _currentStatus.FinalizeDevelopmentBacklogItem();
        public void TestingBacklogItem() => _currentStatus.TestingBacklogItem();
        public void DenyDevelopedBacklogItem() => _currentStatus.DenyDevelopedBacklogItem();
        public void FinalizeTestingBacklogItem() => _currentStatus.FinalizeTestingBacklogItem();
        public void DenyTestedBacklogItem() => _currentStatus.DenyTestedBacklogItem();
        public void FinalizeBacklogItem() => _currentStatus.FinalizeBacklogItem();
        public void ReceiveFeedback() => _currentStatus.ReceiveFeedback();
        public void CloseBacklogItem() => _currentStatus.CloseBacklogItem();
    
        //** End State functions **//
        
        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"Id: {_id}");
            sb.AppendLine($"Title: {_title}");
            sb.AppendLine($"Description: {_description}");
            sb.AppendLine($"Developer: {_developer.ToString()}");
            sb.AppendLine($"Activities: {_activities.Count}");
            sb.AppendLine($"PreviousStatus: {_previousStatus?.GetType().Name}");
            sb.AppendLine($"CurrentStatus: {_currentStatus.GetType().Name}");
            sb.AppendLine($"Threads: {_threads.Count}");
            sb.AppendLine($"StoryPoints: {_storyPoints}");

            return sb.ToString();
        }
    }
}
