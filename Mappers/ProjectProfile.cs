using AutoMapper;
using TaskManagementApi.DTOs.Request;
using TaskManagementApi.Models;

namespace TaskManagementApi.Mappers;

public class ProjectProfile: Profile
{
    public ProjectProfile()
    {
        CreateMap<CreateProjectDto, Project>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src=> Guid.NewGuid()));
        
        // add ignore methods to other fields of Project entity if it needs
        CreateMap<UpdateProjectDto, Project>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore());
    }
}