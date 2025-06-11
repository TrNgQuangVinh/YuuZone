using AutoMapper;
using Repositories.DTO.RequestDTO;
using Repositories.DTO.ResponseDTO;
using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Extension
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserPostRegView>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.GenderTitle, opt => opt.MapFrom(src => src.Gender.GenderTitle))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.StatusName))
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<User, UserView>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.GenderTitle, opt => opt.MapFrom(src => src.Gender.GenderTitle))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.StatusName))
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<RegisterUserForm, User>();

            CreateMap<UpdateUserForm, User>();
        }
    }
}
