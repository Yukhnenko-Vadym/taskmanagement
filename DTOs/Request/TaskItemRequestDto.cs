namespace TaskManagementApi.DTOs.Request;

public class TaskItemRequestDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required TaskStatus Status { get; set; }
    public DateTime Deadline { get; set; }
}

public class CreateTaskItemDto: TaskItemRequestDto
{
    public Guid ProjectId { get; set; } 
}

public class UpdateTaskItemDto: TaskItemRequestDto
{
    
}