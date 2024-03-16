using Domain.Entities;
using DomainServices.Interfaces;

namespace DomainServices.Factories;

public class SprintReleaseFactory : ISprintFactory
{
    public Sprint CreateSprint(string title, DateTime startDate, DateTime endDate, User createdBy, User scrumMaster)
    {
        return new SprintRelease(title, startDate, endDate, createdBy, scrumMaster);
    }
}