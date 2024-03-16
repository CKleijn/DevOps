using System.Text;

namespace Domain.Entities;

public class SprintReview : Sprint
{
    private IList<Review> _reviews { get; init; }
    public IList<Review> Reviews { get => _reviews; init => _reviews = value; }
    
    public SprintReview(string title, DateTime startDate, DateTime endDate, User createdBy, User scrumMaster) : base(title, startDate, endDate, createdBy, scrumMaster)
    {
        _reviews = new List<Review>();
        Console.WriteLine("Sprint with review type initialized.");
    }

    public override void Execute()
    {
        if (Reviews.Count > 0)
        {
            Console.WriteLine($"Execute Sprint ({Title}) with review type");
        }
        else
        {
            Console.WriteLine("Review can't be executed without any reviews. Provide at least one review.");
        }
    }
    
    public void AddReview(Review review)
    {
        _reviews.Add(review);
    }
    
    public void RemoveReview(Review review)
    {
        _reviews.Remove(review);
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        
        sb.AppendLine($"Sprint Review: {Title}");
        sb.AppendLine($"Start Date: {StartDate}");
        sb.AppendLine($"End Date: {EndDate}");
        sb.AppendLine($"Created By: {CreatedBy}");
        sb.AppendLine($"Scrum Master: {ScrumMaster}");
        sb.AppendLine($"Status: {Status.GetType()}");
        sb.AppendLine($"Reviews: {Reviews.Count}");
        
        return sb.ToString();
    }
}