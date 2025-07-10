using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.DTO;
using Repositories.Extension.AutoMapper;
using Repository.CustomFunctions.TokenHandler;
using Services.Service;
using Services.Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<SMTPSettings>(configuration.GetSection("SMTPSettings"));
            service.AddScoped<IAuthenService, AuthenService>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IPostService, PostService>();
            service.AddScoped<ICommentService, CommentService>();
            service.AddScoped<ICommunityService, CommunityService>();
            service.AddScoped<IRoleService, RoleService>();
            service.AddScoped<IEmailService, EmailService>();
            service.AddScoped<JWTTokenProvider>();
            service.AddAutoMapper(typeof(AutoMapperProfile));
            return service;
        }
    }
}
