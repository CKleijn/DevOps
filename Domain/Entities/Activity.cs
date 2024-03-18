using Domain.Helpers;
using Domain.States.BacklogItem;
using System.Text;

namespace Domain.Entities
{
    public class Activity
    {
        private Guid _id { get; init; }
        public Guid Id { get => _id; init => _id = value; }

        private string _title { get; set; }
        public string Title { 
            get => _title; 
            set 
            { 
                if (ValidateUpdate())
                {
                    _title = value;
                    Logger.DisplayUpdatedAlert(nameof(Title), _title);
                }
            } 
        }

        private bool _isFinished { get; set; }
        public bool IsFinished { 
            get => _isFinished; 
            set 
            {
                if (ValidateUpdate())
                {
                    _isFinished = value;
                    Logger.DisplayUpdatedAlert(nameof(IsFinished), _title);
                }
            } 
        }

        private Item _item { get; init; }
        public Item Item { get => _item; init => _item = value; }

        private Developer? _developer { get; set; }
        public Developer? Developer 
        { 
            get => _developer; 
            set 
            {
                if (ValidateUpdate())
                {
                    _developer = value;
                    Logger.DisplayUpdatedAlert(nameof(Developer), _title);
                }
            } 
        }

        public Activity(string title, Item item, Developer developer)
        {
            _id = Guid.NewGuid();
            _title = title;
            _isFinished = false;
            _item = item;
            _developer = developer;

            Logger.DisplayCreatedAlert(nameof(Activity), _title);
        }

        public Activity(string title, Item item)
        {
            _id = Guid.NewGuid();
            _title = title;
            _isFinished = false;
            _item = item;

            Logger.DisplayCreatedAlert(nameof(Activity), _title);
        }

        //TODO: implement functions
        public bool ValidateUpdate()
        {
            if (_item.CurrentStatus.GetType() != typeof(DoneState) || _item.CurrentStatus.GetType() != typeof(ClosedState))
            {
                return true;
            }

            Logger.DisplayCustomAlert(nameof(Activity), nameof(ValidateUpdate), "Can't update activity when item status is done or closed.");
            return false;
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"Id: {_id}");
            sb.AppendLine($"Title: {_title}");
            sb.AppendLine($"IsFinished: {_isFinished}");
            sb.AppendLine($"Developer: {_developer?.ToString()}");

            return sb.ToString();
        }
    }
}
