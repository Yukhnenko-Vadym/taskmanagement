using System.ComponentModel.DataAnnotations.Schema;
using TaskManagementApi.DTOs.Request;

namespace TaskManagementApi.Models;

public class TaskItem
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required TaskStatus Status { get; set; }
    public DateTime Deadline { get; set; }
    
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = null!;
    
}