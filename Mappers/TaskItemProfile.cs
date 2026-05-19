using AutoMapper;
using TaskManagementApi.DTOs.Request;
using TaskManagementApi.DTOs.Response;
using TaskManagementApi.Models;

namespace TaskManagementApi.Mappers;

public class TaskItemProfile: Profile
{
    public TaskItemProfile()
    {
        CreateMap<CreateTaskItemDto, TaskItem>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => Guid.NewGuid()));
        CreateMap<UpdateTaskItemDto, TaskItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ProjectId, opt => opt.Ignore());

        CreateMap<TaskItem, TaskItemResponseDto>();
    }
}