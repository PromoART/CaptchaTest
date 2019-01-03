using AutoMapper;
using GigHub.DTO;
using GigHub.Models;

namespace GigHub.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.Initialize(config => config.CreateMap<ApplicationUser, UserDto>());
            Mapper.Initialize(config => config.CreateMap<Gig, GigDto>());
            Mapper.Initialize(config => config.CreateMap<Notification, NotificationDto>());

            Mapper.AssertConfigurationIsValid();
        }
    }
}