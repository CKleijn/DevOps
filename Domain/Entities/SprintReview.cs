using System.Text;
using Domain.Helpers;
using Domain.States.Sprint;

namespace Domain.Entities;

public class SprintReview : Sprint
{
    private IList<Review> _reviews { get; init; }
    public IList<Review> Reviews { get => _reviews; init => _reviews = value; }
    
    private Pipeline _pipeline { get; set; }
    public Pipeline Pipeline { get => _pipeline; set => _pipeline = value; }
    
    
    public SprintReview(string title, DateTime startDate, DateTime endDate, User scrumMaster) : base(title, startDate, endDate, scrumMaster)
    {
        _reviews = new List<Review>();
        Logger.DisplayCreatedAlert(nameof(SprintReview), Title);
    }

    public void AddReview(Review review)
    {
        _reviews.Add(review);
        Logger.DisplayUpdatedAlert(nameof(Reviews), $"Added review with an id of: {review}");
    }
    
    public void RemoveReview(Review review)
    {
        _reviews.Remove(review);
        Logger.DisplayUpdatedAlert(nameof(Reviews), $"Removed review with an id of: {review}");
    }

    protected override bool ValidateChange()
    {
        //Don't allow mutation whenever state differs from the initial state
        if (CurrentStatus.GetType() != typeof(InitialState))
        {
            Logger.DisplayCustomAlert(nameof(Sprint), nameof(ValidateChange), $"Can't update sprint in current state ({CurrentStatus.GetType()}).");
            
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
        sb.AppendLine($"Start Date: {StartDate}");
        sb.AppendLine($"End Date: {EndDate}");
        sb.AppendLine($"Scrum Master: {ScrumMaster}");
        sb.AppendLine($"Status: {CurrentStatus.GetType()}");
        sb.AppendLine($"Reviews: {Reviews.Count}");
        
        return sb.ToString();
    }
}