using HabitTracker.Models;

namespace HabitTracker.Services;

public interface IProjectService
{
    Task<List<Project>> GetProjectsAsync();
    Task<Project?> GetProjectByIdAsync(int id); // Include tasks when getting by ID
    Task AddProjectAsync(Project project);
    Task UpdateProjectAsync(Project project);
    Task DeleteProjectAsync(int id);
}