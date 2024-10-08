﻿using EDUMITRA.API.Interfaces;
using EDUMITRA.Commonlib.Cache;
using EDUMITRA.Datamodel.Interfaces;
using EDUMITRA.Datamodel.Shared;
using EDUMITRA.Provider;
using System.Threading.Tasks;

namespace EDUMITRA.API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private AuthenticationProvider _authenticationProvider;

        public AuthenticationService()
        {
            _authenticationProvider = new AuthenticationProvider();
        }

        public async Task<ErrorResponse> Validate(IEDUMITRAServiceUser SANYUKTAPIUser, bool validateBothToken = true, bool slidingExpiration = true)
        {
            ErrorResponse error = await IsValidToken(SANYUKTAPIUser, validateBothToken, slidingExpiration);
            if (error.HasError)
            {
                return error;
            }
            return error;
        }

        public async Task<ErrorResponse> IsValidToken(IEDUMITRAServiceUser SANYUKTAPIUser, bool validateBothToken = true, bool slidingExpiration = true)
        {
            ErrorResponse error = new ErrorResponse();

            //If ValidateBothToken is false and still user token is received then it will be validated
            if (!validateBothToken && !string.IsNullOrEmpty(SANYUKTAPIUser.UserToken))
            {
                validateBothToken = true;
            }
            if (string.IsNullOrEmpty(SANYUKTAPIUser.ApiToken) || (validateBothToken && string.IsNullOrEmpty(SANYUKTAPIUser.UserToken)))
            {
                error.SetError(ErrorCodes.AUTHENTICATION_FAILURE);
                this.ClearUserCache(SANYUKTAPIUser.ApiToken, SANYUKTAPIUser.UserToken);
                return error;
            }
            var isValid = await _authenticationProvider.IsValidToken(SANYUKTAPIUser.ApiToken, SANYUKTAPIUser.UserToken, validateBothToken, slidingExpiration);

            if (isValid)
            {
                error.NoError();
            }
            else
            {
                error.SetError(ErrorCodes.AUTHENTICATION_FAILURE);
                this.ClearUserCache(SANYUKTAPIUser.ApiToken, SANYUKTAPIUser.UserToken);
            }

            return error;
        }

        private void ClearUserCache(string apiToken, string userToken)
        {
            if (!string.IsNullOrEmpty(apiToken))
                MemoryCachingService.Clear(apiToken);

            if (!string.IsNullOrEmpty(userToken))
                MemoryCachingService.Clear(userToken);
        }

    }
}
