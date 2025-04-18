using HabitTracker.Models; // Make sure this matches your models namespace
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Data;

public class AppDbContext : DbContext
{
    // DbSet properties represent the tables in your database
    public DbSet<Project> Projects { get; set; }
    public DbSet<TaskItem> TaskItems { get; set; }

    // Constructor used by dependency injection
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Optional: Configure model relationships if needed (EF Core often infers them)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Example: Ensure Project Name is unique if desired
        // modelBuilder.Entity<Project>()
        //     .HasIndex(p => p.Name)
        //     .IsUnique();

        // Define the one-to-many relationship between Project and TaskItem
        // EF Core can usually infer this from navigation properties,
        // but explicit configuration is fine too.
        modelBuilder.Entity<Project>()
            .HasMany(p => p.Tasks) // Project has many Tasks
            .WithOne(t => t.Project) // Task has one Project
            .HasForeignKey(t => t.ProjectId) // Foreign key is TaskItem.ProjectId
            .OnDelete(DeleteBehavior.Cascade); // Optional: Delete tasks if project is deleted
    }
}