using Domain.Entities;
using Domain.Interfaces.Factories;

namespace Domain.Factories;

public class SprintReviewFactory : ISprintFactory<SprintReview>
{
    public SprintReview CreateSprint(string title, DateTime startDate, DateTime endDate, Developer scrumMaster, Project project)
    {
        return new SprintReview(title, startDate, endDate, scrumMaster, project);
    }
}