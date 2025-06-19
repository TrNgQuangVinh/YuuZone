using AutoMapper;
using Repositories.DTO.RequestDTO.Comment;
using Repositories.DTO.RequestDTO.Post;
using Repositories.DTO.RequestDTO.User;
using Repositories.DTO.ResponseDTO.Comment;
using Repositories.DTO.ResponseDTO.Post;
using Repositories.DTO.ResponseDTO.User;
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

            CreateMap<UpdateUserForm, User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CreateCommentForm, Comment>();

            CreateMap<Comment, CommentView>().
                ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Username));

            CreateMap<Post, PostView>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Username))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.StatusName))
                .ForMember(dest => dest.Community, opt => opt.MapFrom(src => src.Community.Name))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments));

            CreateMap<CreatePostForm, Post>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.CommunityId, opt => opt.MapFrom(src => src.CommunityId));

            CreateMap<UpdatePostForm, Post>()
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.Status))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
