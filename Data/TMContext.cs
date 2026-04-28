using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManagementApi.Data;

public class TMContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<TaskItem> TaskItems { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectUser> ProjectUsers { get; set; }
    
    public TMContext(DbContextOptions<TMContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var users  = modelBuilder.Entity<User>();
        var tasks = modelBuilder.Entity<TaskItem>();
        var projects = modelBuilder.Entity<Project>();
        var projectUsers = modelBuilder.Entity<ProjectUser>();
        
        projectUsers.HasKey(pu => new {pu.ProjectId, pu.UserId});
        
        projectUsers
            .HasOne(pu => pu.Project)
            .WithMany(p => p.ProjectUsers)
            .HasForeignKey(pu => pu.ProjectId);
        
        projectUsers
            .HasOne(pu => pu.User)
            .WithMany(u => u.ProjectUsers)
            .HasForeignKey(pu => pu.UserId);
        
        projects.HasMany(p => p.Tasks)
            .WithOne(t => t.Project)
            .HasForeignKey(t => t.ProjectId);

        tasks.HasKey(t => t.Id);
        projects.HasKey(p => p.Id);
        users.HasKey(u => u.Id);
    }
}