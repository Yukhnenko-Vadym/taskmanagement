using AutoMapper;
using TaskManagementApi.DTOs.Request;
using TaskManagementApi.Models;

namespace TaskManagementApi.Mappers;

public class ProjectProfile: Profile
{
    public ProjectProfile()
    {
        CreateMap<CreateProjectDto, Project>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Owner, opt => opt.Ignore())
            .ForMember(dest => dest.ProjectUsers, opt => opt.Ignore())
            .ForMember(dest => dest.Tasks, opt => opt.Ignore());

        CreateMap<UpdateProjectDto, Project>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.OwnerId, opt => opt.Ignore())
            .ForMember(dest => dest.Owner, opt => opt.Ignore())
            .ForMember(dest => dest.ProjectUsers, opt => opt.Ignore())
            .ForMember(dest => dest.Tasks, opt => opt.Ignore());
    }
}