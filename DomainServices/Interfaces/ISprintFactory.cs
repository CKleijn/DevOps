using Domain.Entities;

namespace DomainServices.Interfaces;

public interface ISprintFactory
{
    public Sprint CreateSprint(string title, DateTime startDate, DateTime endDate, User createdBy, User scrumMaster);
}