using System.ComponentModel.DataAnnotations.Schema;
using TaskManagementApi.Models.Enums;

namespace TaskManagementApi.Models;

public class Project
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime StartedAt { get; set; }
    
    public Guid OwnerId { get; set; }
    public User Owner { get; set; } = null!;

    public ICollection<ProjectUser> ProjectUsers { get; set; } = new List<ProjectUser>();
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}