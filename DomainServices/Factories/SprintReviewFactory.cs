using Domain.Entities;
using DomainServices.Interfaces;

namespace DomainServices.Factories;

public class SprintReviewFactory : ISprintFactory<SprintReview>
{
    public SprintReview CreateSprint(string title, DateTime startDate, DateTime endDate, User scrumMaster, Project project)
    {
        return new SprintReview(title, startDate, endDate, scrumMaster, project);
    }
}