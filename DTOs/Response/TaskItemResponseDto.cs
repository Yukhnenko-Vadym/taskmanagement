namespace TaskManagementApi.DTOs.Response;

public class TaskItemResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TaskStatus Status { get; set; }
    public DateTime Deadline { get; set; }
    
    public Guid ProjectId { get; set; }
    public Guid? AssigneeId { get; set; }
}