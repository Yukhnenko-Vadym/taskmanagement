namespace TaskManagementApi.DTOs.Response;

public class ProjectResponseDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime StartedAt { get; set; }
    
    public Guid OwnerId { get; set; }
}