using Domain.Dto;
using Domain.Interfaces;
using Domain.Models;
using Domain.Validations;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Service.Abstract;
using Service.Helpers;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concrete
{
    public class AuthService : IAuthService
    {

        private readonly IRepository<User> _userRepository;
        public AuthService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _userRepository.GetWhereAsync(u => u.Email == email);
        }

        public async Task<BaseResponseDto<UserDto>> Login(AuthDto auth)
        {
            try
            {
                var user = await GetByEmailAsync(auth.Email);
                BaseResponseDto<UserDto> userResponse = new BaseResponseDto<UserDto>();
                if (user == null)
                {
                    userResponse.Errors.Add("Email", "Enail is wrong");
                    return userResponse;
                }

                var locker = CreateLockerInstance();
                string decrpytPassword = locker.Decrypt(user.Password);
                if (decrpytPassword != auth.Password)
                {
                    userResponse.Errors.Add("Password", "Password is wrong");
                    return userResponse;

                }

                JwtTokenHandler tokenHandler = new JwtTokenHandler();
                var token = tokenHandler.CreateAccessToken(user);
                user.AccessToken = token.AccessToken;
                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenEndDate = token.Expiration.AddDays(1);
                UserDto userDto = new UserDto(user.Id,user.FirstName,user.LastName,user.Address,user.AccessToken,user.Role);
                userResponse.Data = userDto;
                await _userRepository.UpdateAsync(user);
                return userResponse;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<BaseResponseDto<User>> Register(User user)
        {
            try
            {
                BaseResponseDto<User> createUserResponse = new BaseResponseDto<User>();
                var errorsResult = ValidateUserInRegister(user);
                if (errorsResult != null && errorsResult.HasError) return errorsResult;
                var hasSuchEmail = await GetByEmailAsync(user.Email);
                if (hasSuchEmail != null)
                {
                    createUserResponse.Errors.Add("Email", "Such email is taken");
                    return createUserResponse;
                }
                var locker = CreateLockerInstance();
                string encryptKey = locker.Encrypt(user.Password);
                user.Password = encryptKey;
                user.Role = Role.User;
                createUserResponse.Data = user;
                await _userRepository.CreateAsync(user);
                return createUserResponse;
            }
            catch (Exception)
            {
                throw;
            }

        }
        private BaseResponseDto<User> ValidateUserInRegister(User user)
        {
            BaseResponseDto<User> createUserResponse = new BaseResponseDto<User>();
            UserValidator userValidator = new UserValidator();
            var result = userValidator.Validate(user);
            if (!result.IsValid)
            {
                foreach (var validationFailure in result.Errors)
                {
                    var checkPrevValue = createUserResponse.Errors.Where(x => x.Key == validationFailure.PropertyName).FirstOrDefault();
                    if (checkPrevValue.Value != null)
                    {
                        createUserResponse.Errors.Add(validationFailure.PropertyName + "1", validationFailure.ErrorMessage);
                    }
                    else
                    {
                        createUserResponse.Errors.Add(validationFailure.PropertyName, validationFailure.ErrorMessage);
                    }

                }
                createUserResponse.Data = user;
                return createUserResponse;
            }
            return null;
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            User user = await _userRepository.GetWhereAsync(x => x.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.Now)
            {
                JwtTokenHandler tokenHandler = new JwtTokenHandler();
                Token token = tokenHandler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenEndDate = token.Expiration.AddMinutes(3);
                await _userRepository.UpdateAsync(user);

                return token;
            }
            return null;
        }
        private Security CreateLockerInstance()
        {
            var SCollection = new ServiceCollection();
            SCollection.AddDataProtection();
            var LockerKey = SCollection.BuildServiceProvider();
            var locker = ActivatorUtilities.CreateInstance<Security>(LockerKey);
            return locker;
        }


    }
}
