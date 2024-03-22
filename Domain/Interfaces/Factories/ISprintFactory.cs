using Domain.Entities;

namespace Domain.Interfaces.Factories;

public interface ISprintFactory<out T> where T : Sprint
{
    public T CreateSprint(string title, DateTime startDate, DateTime endDate, Developer scrumMaster, Project project);
}