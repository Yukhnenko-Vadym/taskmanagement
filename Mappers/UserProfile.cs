using AutoMapper;
using TaskManagementApi.DTOs.Request;
using TaskManagementApi.DTOs.Response;
using TaskManagementApi.Models;

namespace TaskManagementApi.Mappers;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.PasswordHash,
                opt => opt.Ignore())
            .ForMember(dest => dest.PasswordSalt,
                opt => opt.Ignore());

        CreateMap<UpdateUserDto, User>()
            .ForMember(dest => dest.Id, 
                opt => opt.Ignore())
            .ForMember(dest => dest.FirstName,
                opt => opt.Ignore())
            .ForMember(dest => dest.LastName,
                opt => opt.Ignore());

        CreateMap<User, UserResponseDto>();
    }
}