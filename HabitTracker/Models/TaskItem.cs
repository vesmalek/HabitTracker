using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // Required for ForeignKey

namespace HabitTracker.Models;

public class TaskItem
{
    public int Id { get; set; } // Primary Key

    [Required(ErrorMessage = "Task name is required")]
    [StringLength(200, ErrorMessage = "Task name cannot be longer than 200 characters.")]
    public string Name { get; set; } = string.Empty;

    public DateTime? DueDate { get; set; } // Optional due date

    public bool IsDone { get; set; } = false; // Status if the task is completed

    // Foreign Key property to link back to the Project
    public int ProjectId { get; set; }

    // Navigation property (optional but good practice)
    [ForeignKey("ProjectId")]
    public Project? Project { get; set; }
}