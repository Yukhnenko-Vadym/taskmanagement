using TaskManagementApi.Models.Enums;

namespace TaskManagementApi.DTOs.Response;

public class UserResponseDto
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required Roles Role { get; set; }
}