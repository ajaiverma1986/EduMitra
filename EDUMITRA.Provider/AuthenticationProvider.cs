﻿using EDUMITRA.Commonlib.Cache;
using EDUMITRA.Commonlib.Security;
using EDUMITRA.Datamodel.Common;
using EDUMITRA.Datamodel.DTO.Request;
using EDUMITRA.Datamodel.DTO.Response;
using EDUMITRA.Datamodel.Entities.Application;
using EDUMITRA.Datamodel.Entities.Authorization;
using EDUMITRA.Datamodel.Interfaces;
using EDUMITRA.Datamodel.Shared;
using EDUMITRA.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace EDUMITRA.Provider
{
    public class AuthenticationProvider
    {
        private readonly AuthenticationRepository _repository = null;

        //private readonly OracleAuthenticationRepository _repository = null;

        public AuthenticationProvider()
        {
            // _repository = new OracleAuthenticationRepository();

            _repository = new AuthenticationRepository();
        }

        public async Task<ApplicationUserMappingResponse> GetApplicationAndUserDetails(IEDUMITRAServiceUser serviceUser)
        {
            return await _repository.GetApplicationAndUserDetails(serviceUser);
        }

        public async Task<List<UserApplicationAccessPermissions>> GetUserAccessPermissions(IEDUMITRAServiceUser serviceUser)
        {
            List<UserApplicationAccessPermissions> userPermissions = null;
            if (!string.IsNullOrWhiteSpace(serviceUser.UserToken))
            {
                userPermissions = await MemoryCachingService.Get<List<UserApplicationAccessPermissions>>(string.Format(CacheKeys.USER_ROLES_API, serviceUser.ApplicationID, serviceUser.UserToken));
                if (userPermissions == null || userPermissions.Count == 0)
                {
                    userPermissions = await _repository.GetUserAccessPermissions(serviceUser);
                    await MemoryCachingService.Put(string.Format(CacheKeys.USER_ROLES_API, serviceUser.ApplicationID, serviceUser.UserToken), userPermissions);
                }
            }
            return userPermissions;
        }

        public async Task<int> Logout(IEDUMITRAServiceUser request)
        {
            return await _repository.Logout(request);
        }

        public async Task<bool> IsValidToken(String apiToken, String userToken, Boolean validateBothToken = true, Boolean slidingExpiration = true)
        {
            return await _repository.IsValidToken(apiToken, userToken, validateBothToken, slidingExpiration);
        }

        public async Task<UserLoginResponse> Login(UserLoginRequest userLoginRequest, IEDUMITRAServiceUser serviceUser)
        {
            UserLoginResponse userLoginResponse = new UserLoginResponse();
            UserDetails userDetail = await GetUserLoginDetails(userLoginRequest, serviceUser);

            if (userDetail == null)
            {
                userLoginResponse.SetError(ErrorCodes.INVALID_UserName);
                return userLoginResponse;
            }

            if (userDetail.Status != UserMasterStatus.Activated)
            {
                userLoginResponse.SetError(ErrorCodes.AUTHENTICATION_FAILURE, "This id is not Active");
                return userLoginResponse;
            }
            Boolean validPassword = BCrypt.Net.BCrypt.Verify(userLoginRequest.Password, userDetail.Password);
            if (!validPassword)
            {
                userLoginResponse.SetError(ErrorCodes.INCORRECT_PASSWORD);
                return userLoginResponse;
            }

            if (!await _repository.ValidateApplicationTypePermissions(userDetail.UserMasterID, serviceUser))
            {
                userLoginResponse.SetError(ErrorCodes.USER_NOT_ALLOWED_APPLCATION_ACCESS);
                return userLoginResponse;
            }

            String token = GenerateToken(serviceUser);

            Boolean isTokenCreateSuccess = await _repository.CreateUserToken(userDetail.UserMasterID, token, serviceUser);

            if (!isTokenCreateSuccess)
            {
                userLoginResponse.SetError(ErrorCodes.USER_NOT_ALLOWED_APPLCATION_ACCESS);
                return userLoginResponse;
            }

            userLoginResponse.UserToken = token;
            userLoginResponse.DisplayName = userDetail.DisplayName;
            return userLoginResponse;
        }

        private async Task<UserDetails> GetUserLoginDetails(UserLoginRequest userLoginRequest, IEDUMITRAServiceUser serviceUser)
        {
            return await _repository.GetUserLoginDetails(userLoginRequest, serviceUser);
        }

        public async Task<SimpleResponse> ChangePassword(UserChangePasswordRequest request, IEDUMITRAServiceUser serviceUser)
        {
            SimpleResponse response = new SimpleResponse();
            UserLoginRequest _userLogin = new UserLoginRequest
            {
                Username = ""
            };
            UserDetails userDetails = await GetUserLoginDetails(_userLogin, serviceUser);


            if (userDetails == null || userDetails.UserMasterID <= 0)
            {
                response.SetError(ErrorCodes.SERVER_ERROR);
            }
            else
            {
                if (!PasswordChecker.IsStrongPassword(request.Password))
                {
                    response.SetError(ErrorCodes.SIMPLE_PASSWORD);
                }
                else
                {
                    if (BCrypt.Net.BCrypt.Verify(request.OldPassword, userDetails.Password))
                    {
                        request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
                        request.OldPassword = BCrypt.Net.BCrypt.HashPassword(request.OldPassword);
                        await _repository.ChangePassword(request, serviceUser);
                    }
                    else
                    {
                        response.SetError(ErrorCodes.INCORRECT_PASSWORD);
                    }
                }
            }
            return response;
        }

        public async Task<bool> IsValidTokenCheck(String apiToken, String userToken, Boolean validateBothToken, Boolean slidingExpiration, IEDUMITRAServiceUser serviceUser)
        {
            return await _repository.IsValidToken(apiToken, userToken, validateBothToken, slidingExpiration);
        }

        private string GenerateToken(IEDUMITRAServiceUser request)
        {
            String guid = Guid.NewGuid().ToString();
            return BCrypt.Net.BCrypt.HashString(guid);
        }

        public async Task<SimpleResponse> ForgotPassword(string userName, IEDUMITRAServiceUser serviceUser)
        {
            SimpleResponse response = new SimpleResponse();
            //SendEmail();
            //TODO: Do all the work and send email notification for password reset.
            return response;
        }

        public async Task<SimpleResponse> ResetPassword(ResetPasswordRequest request, IEDUMITRAServiceUser serviceUser)
        {
            SimpleResponse response = new SimpleResponse();
            //TODO: Do all the work and send email notification for password reset.
            return response;
        }
        public async Task<SimpleResponse> ResetPasswordOTP(ResetPasswordRequestOTP request, IEDUMITRAServiceUser serviceUser)
        {
            SimpleResponse response = new SimpleResponse();


            if (!PasswordChecker.IsStrongPassword(request.Password))
            {
                response.SetError(ErrorCodes.SIMPLE_PASSWORD);
            }
            else
            {
                string a = await _repository.PasswordLog_Search(request.UserMasterID, BCrypt.Net.BCrypt.HashPassword(request.Token));

                if (a != request.Token.ToString().Trim())
                {
                    response.SetError("Invalid Otp Password");
                    return response;
                }
                request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password.Trim());
                request.Token = request.Token.Trim();
                await _repository.ResetPasswordOTP(request, serviceUser);
            }
            return response;
        }

       

        private static string CreateRandomPassword()
        {
            try
            {
                int length = 8;
                // Create a string of characters, numbers, special characters that allowed in the password  
                string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
                Random random = new Random();

                // Select one random character at a time from the string  
                // and create an array of chars  
                char[] chars = new char[length];
                for (int i = 0; i < length; i++)
                {
                    chars[i] = validChars[random.Next(0, validChars.Length)];
                }
                return new string(chars);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
     

    }
}
