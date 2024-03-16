namespace Domain.Entities;

public class SprintReview : Sprint
{
    private IList<Review> _reviews { get; init; }
    public IList<Review> Reviews { get => _reviews; init => _reviews = value; }
    
    //TODO: implement functions

    public SprintReview(string title, DateTime startDate, DateTime endDate, User createdBy, User scrumMaster) : base(title, startDate, endDate, createdBy, scrumMaster)
    {
        _reviews = new List<Review>();
    }

    public override void Execute()
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }
}