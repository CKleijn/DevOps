using System.Text;
using Domain.Helpers;
using Domain.States.Sprint;

namespace Domain.Entities;

public class SprintReview : Sprint
{
    private IList<Review> _reviews { get; init; }
    public IList<Review> Reviews { get => _reviews; init => _reviews = value; }
    
    public SprintReview(string title, DateTime startDate, DateTime endDate, Developer scrumMaster, Project project) : base(title, startDate, endDate, scrumMaster, project)
    {
        _reviews = new List<Review>();
        Logger.DisplayCreatedAlert(nameof(SprintReview), Title);
    }
    
    public void AddReview(Review review)
    {
        if (CurrentStatus.GetType() != typeof(ReviewState))
            return;
        
        _reviews.Add(review);
        Logger.DisplayUpdatedAlert(nameof(Reviews), $"Added review with an id of: {review}");
    }
    
    public void RemoveReview(Review review)
    {
        if (CurrentStatus.GetType() != typeof(ReviewState))
            return;
        
        _reviews.Remove(review);
        Logger.DisplayUpdatedAlert(nameof(Reviews), $"Removed review with an id of: {review}");
    }

    protected override bool ValidateChange()
    {
        //Don't allow mutation whenever sprint's state differs from the initial state
        if (CurrentStatus.GetType() != typeof(InitialState))
        {
            Logger.DisplayCustomAlert(nameof(Sprint), nameof(ValidateChange), $"Can't update sprint in current state ({CurrentStatus.GetType()}).");
            
            return false;
        }
        
        //Don't allow mutation whenever pipeline's state differs from the initial state
        if (Pipeline?.CurrentStatus.GetType() != typeof(States.Pipeline.InitialState))
        {
            Logger.DisplayCustomAlert(nameof(Sprint), nameof(ValidateChange), $"Can't update sprint when it's corresponding pipeline isn't in its initial state ({Pipeline?.CurrentStatus.GetType()}).");
            
            return false;
        }
        
        //Perform actions alteration is done on a sprint that has already ended
        if (EndDate < DateTime.Now)
        {
            //Set sprint to finished state if it isn't already
            if(CurrentStatus.GetType() != typeof(FinishedState))
                CurrentStatus = new FinishedState(this);
            
            Logger.DisplayCustomAlert(nameof(SprintReview), nameof(ValidateChange), $"Can't update sprint after end date. Sprint will be set to finished and corresponding actions will be performed.");
    
            //Check if sprint input is valid
            CurrentStatus.ReviewSprint();
            
            //Review sprint 
            if (CurrentStatus.GetType() == typeof(ReviewState))
            {
                ReviewSprint();
            }
            
            return false;
        }
        
        return true;
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        
        sb.AppendLine($"Sprint Review: {Title}");
        sb.AppendLine($"Start Date: {StartDate.ToString("dd/mm/yyyy")}");
        sb.AppendLine($"End Date: {EndDate.ToString("dd/mm/yyyy")}");
        sb.AppendLine($"Scrum Master: {ScrumMaster.Name}");
        sb.AppendLine($"Amount of developers: {Developers.Count}");
        sb.AppendLine($"Amount of testers: {Testers.Count}");
        sb.AppendLine($"Status: {CurrentStatus.GetType()}");
        sb.AppendLine($"Amount of reviews: {Reviews.Count}");
        sb.AppendLine($"Amount of items: {SprintBacklog.Items.Count}");

        return sb.ToString();
    }
}
