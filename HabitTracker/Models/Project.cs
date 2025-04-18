using System.ComponentModel.DataAnnotations; // Required for validation attributes

namespace HabitTracker.Models;

public class Project
{
    public int Id { get; set; } // Primary Key

    [Required(ErrorMessage = "Project name is required")]
    [StringLength(100, ErrorMessage = "Project name cannot be longer than 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
    public string? Description { get; set; } // Optional description

    // Navigation property: A project can have multiple tasks
    public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();

    // Optional: Calculated property for progress (we can implement later)
    // public int ProgressPercentage => Tasks.Any() ? (int)Math.Round((double)Tasks.Count(t => t.IsDone) / Tasks.Count * 100) : 0;
}