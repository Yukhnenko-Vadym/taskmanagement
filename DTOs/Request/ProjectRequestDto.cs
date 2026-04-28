namespace TaskManagementApi.DTOs.Request;

public class ProjectRequestDto
{
    public required string Name { get; set; }
}

public class CreateProjectDto: ProjectRequestDto
{
}

public class UpdateProjectDto : ProjectRequestDto
{
}