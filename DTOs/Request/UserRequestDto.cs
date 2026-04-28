using TaskManagementApi.Models.Enums;

namespace TaskManagementApi.DTOs.Request;

public class UserRequestDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required Roles Role { get; set; }
}

public class CreateUserDto : UserRequestDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}

public class UpdateUserDto : UserRequestDto
{

}