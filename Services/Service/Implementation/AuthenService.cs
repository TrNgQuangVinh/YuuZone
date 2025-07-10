using AutoMapper;
using Azure;
using Microsoft.IdentityModel.Tokens;
using Repositories.Base;
using Repositories.Constant;
using Repositories.DTO.RequestDTO.User;
using Repositories.DTO.ResponseDTO.User;
using Repositories.Repository;
using Repository.CustomFunctions.TokenHandler;
using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.Implementation
{
    public class AuthenService : IAuthenService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _email;
        private readonly JWTTokenProvider _jwt;

        public AuthenService(UnitOfWork unitOfWork, IMapper mapper, IEmailService email, JWTTokenProvider jwt)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _email = email;
            _jwt = jwt;
        }

        public async Task<(UserLoginView? login, UserPostRegView? register)> GoogleLogin(string email, string name, string googleId)
        {
            if (email.IsNullOrEmpty() && name.IsNullOrEmpty() && googleId.IsNullOrEmpty())
                return (null,null);
            //This is the existing check, check account with similar email or name AND googleId
            //If login failed = account doesnt exist, so register an account with google
            var existing = await _unitOfWork._authRepo.LoginByGoogle(email, name, googleId);
            if(existing == null)
            {
                var result = await GoogleRegister(email, name, googleId);
                return (null,result.user);
            }
            else
            {
                var token = _jwt.CreateToken(existing);
                var mapped = _mapper.Map<UserLoginView?>(existing); //include Role
                mapped.JwtToken = token;
                return (mapped, null);
            }
        }

        public async Task<(string status, UserPostRegView? user)> GoogleRegister(string email, string name, string googleId)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                RegisterUserForm regUser = new RegisterUserForm();
                regUser.Email = email;
                regUser.Username = name;
                regUser.GoogleId = googleId;

                var regData = _mapper.Map<User>(regUser);
                var response = await _unitOfWork._authRepo.RegisterByGoogle(regData);

                await _unitOfWork.CommitTransactionAsync();
                if (response.status.Equals(ConstantEnum.RepoStatus.SUCCESS))
                {
                    var body = _email.GenerateBodyRegisterSuccess(response.user.Username, response.user.Password);
                    _email.SendEmailAsync("YuuZone Account Registration", body, response.user.Email, response.user.Fullname);
                    return (response.status, _mapper.Map<UserPostRegView>(response.user));
                }
                else
                    return (response.status, null);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserLoginView?> Login(string? input, string password)
        {
            if (input.IsNullOrEmpty())
                return null;
            var user = await _unitOfWork._authRepo.Login(input, password);
            if(user != null)
            {
                var token = _jwt.CreateToken(user);
                var mapped = _mapper.Map<UserLoginView?>(user); //include Role
                mapped.JwtToken = token;
                return mapped;
            }
            return null;
        }

        public async Task<(string status,UserPostRegView? user)> Register(RegisterUserForm regUser)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                if(!regUser.PhoneNumber.IsNullOrEmpty())
                    regUser.PhoneNumber = regUser.PhoneNumber.Trim();

                var regData = _mapper.Map<User>(regUser);
                var response = await _unitOfWork._authRepo.Register(regData);

                await _unitOfWork.CommitTransactionAsync();

                if (response.status.Equals(ConstantEnum.RepoStatus.SUCCESS))
                {
                    var body = _email.GenerateBodyRegisterSuccess(response.user.Username, response.user.Password);
                    _email.SendEmailAsync("YuuZone Account Registration", body, response.user.Email, response.user.Fullname);
                    return (response.status, _mapper.Map<UserPostRegView>(response.user));
                }
                else
                    return (response.status, null);
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
            
        }
    }
}
