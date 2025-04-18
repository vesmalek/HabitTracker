using HabitTracker.Data;
using HabitTracker.Models;
using Microsoft.EntityFrameworkCore; // Required for Include, ToListAsync etc.

namespace HabitTracker.Services;

public class ProjectService : IProjectService
{
    private readonly AppDbContext _context;

    // Inject the AppDbContext via the constructor
    public ProjectService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Project>> GetProjectsAsync()
    {
        // Return all projects, ordered by Name
        return await _context.Projects.OrderBy(p => p.Name).ToListAsync();
    }

    public async Task<Project?> GetProjectByIdAsync(int id)
    {
        // Find a project by its Id and include its related TaskItems
        return await _context.Projects
                             .Include(p => p.Tasks) // Eagerly load the Tasks collection
                             .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddProjectAsync(Project project)
    {
        if (project == null) throw new ArgumentNullException(nameof(project));

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProjectAsync(Project project)
    {
         if (project == null) throw new ArgumentNullException(nameof(project));

        // Find the existing project in the database
        var existingProject = await _context.Projects.FindAsync(project.Id);

        if (existingProject != null)
        {
            // Update properties
            existingProject.Name = project.Name;
            existingProject.Description = project.Description;
            // Note: We are not updating the Tasks list here directly.
            // Managing related entities often requires more specific logic.

            _context.Projects.Update(existingProject);
            await _context.SaveChangesAsync();
        }
        else
        {
            // Handle case where project to update isn't found
            throw new KeyNotFoundException($"Project with Id {project.Id} not found.");
        }
    }

    public async Task DeleteProjectAsync(int id)
    {
        var projectToDelete = await _context.Projects.FindAsync(id);
        if (projectToDelete != null)
        {
            _context.Projects.Remove(projectToDelete);
            await _context.SaveChangesAsync();
            // Note: Due to Cascade delete configured in AppDbContext,
            // related TaskItems should also be deleted automatically.
        }
         else
        {
            // Handle case where project to delete isn't found
            throw new KeyNotFoundException($"Project with Id {id} not found.");
        }
    }
}