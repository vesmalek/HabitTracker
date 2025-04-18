using Microsoft.EntityFrameworkCore;
using HabitTracker.Data; // Namespace for your AppDbContext
using HabitTracker.Models; // Namespace for your models (if needed elsewhere)
using HabitTracker.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(); // Or AddInteractiveWebAssemblyComponents

// --- Add DbContext registration ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? "Data Source=habittracker.db"; // Fallback connection string

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));
// --- End DbContext registration ---

var app = builder.Build();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();