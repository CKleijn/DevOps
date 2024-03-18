using System.Text;
using Domain.Helpers;

namespace Domain.Entities;

public class SprintReview : Sprint
{
    private IList<Review> _reviews { get; init; }
    public IList<Review> Reviews { get => _reviews; init => _reviews = value; }
    
    public SprintReview(string title, DateTime startDate, DateTime endDate, User scrumMaster) : base(title, startDate, endDate, scrumMaster)
    {
        _reviews = new List<Review>();
        Logger.DisplayCreatedAlert(nameof(SprintReview), Title);
    }

    public override void Execute()
    {
        if (Reviews.Count > 0)
        {
            Logger.DisplayCustomAlert(nameof(SprintReview), nameof(Execute), $"Execute Sprint ({Title}).");
        }
        else
        {
            Logger.DisplayCustomAlert(nameof(SprintReview), nameof(Execute), "Review can't be executed without any reviews. Provide at least one review.");
        }
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