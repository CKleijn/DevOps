using Domain.Entities;
using Domain.Interfaces.Factories;

namespace Domain.Factories;

public class SprintReleaseFactory : ISprintFactory<SprintRelease>
{
    public SprintRelease CreateSprint(string title, DateTime startDate, DateTime endDate, Developer scrumMaster, Project project)
    {
        return new SprintRelease(title, startDate, endDate, scrumMaster, project);
    }
}