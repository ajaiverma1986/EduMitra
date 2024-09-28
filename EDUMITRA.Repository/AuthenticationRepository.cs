using EDUMITRA.Database;
using EDUMITRA.Datamodel.Common;
using EDUMITRA.Datamodel.DTO.Request;
using EDUMITRA.Datamodel.Entities.Application;
using EDUMITRA.Datamodel.Entities.Authorization;
using EDUMITRA.Datamodel.Interfaces;
using EDUMITRA.Datamodel.Shared;
using EDUMITRA.Repository.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EDUMITRA.Repository
{
    public class AuthenticationRepository : BaseRepository
    {
        private readonly IEDUMITRADatabase _database = null;


        public AuthenticationRepository()
        {
            _database = new EDUMITRADatabase();
        }

        public async Task<ApplicationUserMappingResponse> GetApplicationAndUserDetails(IEDUMITRAServiceUser serviceUser)
        {
            ApplicationUserMappingResponse applicationDetail = new ApplicationUserMappingResponse();
            var dbCommand = _database.GetStoredProcCommand("AAC.usp_GetApplicationAndUserDetails");
            _database.AddInParameter(dbCommand, "@in_apiToken", serviceUser.ApiToken);
            _database.AddInParameter(dbCommand, "@in_userToken", serviceUser.UserToken);
            using (var dataReader = await _database.ExecuteReaderAsync(dbCommand))
            {
                if (dataReader.Read())
                {
                    applicationDetail.ApplicationName = GetStringValue(dataReader, "ApplicationName");
                    int? applicataionId = GetInt32Value(dataReader, "ApplicationId");
                    if (applicataionId.HasValue)
                        applicationDetail.ApplicationId = applicataionId.Value;

                    applicationDetail.UserMasterID = GetInt32Value(dataReader, "UserMasterID");

                    applicationDetail.OrganizationID = GetInt32Value(dataReader, "OrganizationID");
                    applicationDetail.UserID = GetInt32Value(dataReader, "UserID");
                    applicationDetail.UserTypeID = GetInt32Value(dataReader, "UserTypeId");

                    var applicationType = GetInt32Value(dataReader, "AppType");
                    if (applicationType.HasValue)
                    {
                        applicationDetail.AppType = (ApplicationTypes)applicationType;
                    }
                }
            }
            return applicationDetail;
        }

        public async Task<UserDetails> GetUserLoginDetails(UserLoginRequest userLoginRequest, IEDUMITRAServiceUser serviceUser)
        {
            UserDetails userDetail = null;
            var dbCommand = _database.GetStoredProcCommand("AAC.usp_GetUserDetails");
            _database.AddInParameter(dbCommand, "@in_userName", userLoginRequest.Username);
            _database.AddInParameter(dbCommand, "@in_UserMasterID", serviceUser.UserMasterID);

            using (var dataReader = await _database.ExecuteReaderAsync(dbCommand))
            {
                dataReader.Read();
                if (dataReader.HasRows)
                {
                    userDetail = new UserDetails();
                    userDetail.UserMasterID = GetInt64Value(dataReader, "UserMasterID").Value;
                    userDetail.Password = GetStringValue(dataReader, "Password");
                    userDetail.DisplayName = GetStringValue(dataReader, "DisplayName");
                    userDetail.Status = (UserMasterStatus)GetInt32Value(dataReader, "Status");
                }
            }

            return userDetail;
        }


        public async Task<SimpleResponse> ChangePassword(UserChangePasswordRequest request, IEDUMITRAServiceUser serviceUser)
        {
            SimpleResponse response = new SimpleResponse();
            var dbCommand = _database.GetStoredProcCommand("[USR].[UserMaster_UpdatePassword]");
            _database.AddInParameter(dbCommand, "@oldPassword", request.OldPassword);
            _database.AddInParameter(dbCommand, "@NewPassword", request.Password);
            _database.AddInParameter(dbCommand, "@LoggedinUserMasterID", serviceUser.UserMasterID);

            response.Result = await _database.ExecuteNonQueryAsync(dbCommand);
            return response;
        }
        public async Task<SimpleResponse> ResetPasswordOTP(ResetPasswordRequestOTP request, IEDUMITRAServiceUser serviceUser)
        {
            SimpleResponse response = new SimpleResponse();
            var dbCommand = _database.GetStoredProcCommand("[USR].ResetPasswordOTP");
            _database.AddInParameter(dbCommand, "@UserMasterID", request.UserMasterID);
            _database.AddInParameter(dbCommand, "@Token", request.Token);
            _database.AddInParameter(dbCommand, "@Password", request.Password);

            response.Result = await _database.ExecuteNonQueryAsync(dbCommand);
            return response;
        }


        public async Task<Int16> ValidatePassword(ValidatePasswordRequest request, IEDUMITRAServiceUser serviceUser)
        {
            Int16 Result = 0;
            SimpleResponse response = new SimpleResponse();
            var dbCommand = _database.GetStoredProcCommand("[USR].[UserMaster_ValidatePasswordRequest]");
            _database.AddInParameter(dbCommand, "@OldPassword", request.OldPassword);
            _database.AddInParameter(dbCommand, "@NewPassword", request.NewPassword);
            _database.AddInParameter(dbCommand, "@LoggedinUserMasterID", serviceUser.UserMasterID);
            _database.AddOutParameter(dbCommand, "@out_ErrorID", OUTPARAMETER_SIZE);
            await _database.ExecuteNonQueryAsync(dbCommand);

            object result = _database.GetParameterValue(dbCommand, "@out_ErrorID");
            if (result != null)
            {
                Result = Convert.ToInt16(result);
            }
            return Result;
        }

        public async Task<bool> CreateUserToken(Int64 UserMasterID, string token, IEDUMITRAServiceUser serviceUser)
        {
            bool isSuccess = false;

            var dbCommand = _database.GetStoredProcCommand("AAC.usp_CreateUserToken");
            _database.AddInParameter(dbCommand, "@in_UserMasterID", UserMasterID);
            _database.AddInParameter(dbCommand, "@in_UserToken", token);
            _database.AddInParameter(dbCommand, "@in_ApiToken", serviceUser.ApiToken);

            if (!string.IsNullOrEmpty(serviceUser.ClientIPAddress))
                _database.AddInParameter(dbCommand, "@in_IPAddress", serviceUser.ClientIPAddress);
            else
                _database.AddInParameter(dbCommand, "@in_IPAddress", serviceUser.IPAddress);

            object result = await _database.ExecuteScalarAsync(dbCommand);
            if (result != null)
            {
                isSuccess = (bool)result;
            }
            return isSuccess;
        }

        public async Task<int> Logout(IEDUMITRAServiceUser request)
        {
            string result = string.Empty;
            var dbCommand = _database.GetStoredProcCommand("AAC.usp_ExpireUserToken");
            _database.AddInParameter(dbCommand, "@in_userToken", request.UserToken);
            return await _database.ExecuteNonQueryAsync(dbCommand);
        }

        public async Task<bool> IsValidToken(string apiToken, string userToken, bool validateBothToken = true, bool slidingExpiration = true)
        {
            bool IsValidToken = false;
            var dbCommand = _database.GetStoredProcCommand("[AAC].usp_VerifyUserToken");
            _database.AddInParameter(dbCommand, "@in_userToken", userToken);
            _database.AddInParameter(dbCommand, "@in_apiToken", apiToken);
            _database.AddInParameter(dbCommand, "@in_slidingExpiration", slidingExpiration);
            _database.AddInParameter(dbCommand, "@in_validateBothToken", validateBothToken);

            object result = await _database.ExecuteScalarAsync(dbCommand);

            if (result != null)
            {
                IsValidToken = (bool)result;
            }

            return IsValidToken;
        }

        public async Task<bool> ValidateApplicationTypePermissions(Int64 UserMasterID, IEDUMITRAServiceUser serviceUser)
        {
            bool isAllowed = false;
            var dbCommand = _database.GetStoredProcCommand("[AAC].[usp_ValidationApplicationPermissions]");

            _database.AddInParameter(dbCommand, "@in_UserMasterID", UserMasterID);
            _database.AddInParameter(dbCommand, "@in_ApplicationId", serviceUser.ApplicationID);

            object result = await _database.ExecuteScalarAsync(dbCommand);

            if (result != null)
            {
                isAllowed = ((int)result > 0);
            }

            return isAllowed;
        }

        public async Task<List<UserApplicationAccessPermissions>> GetUserAccessPermissions(IEDUMITRAServiceUser serviceUser)
        {
            List<UserApplicationAccessPermissions> listResponse = new List<UserApplicationAccessPermissions>();

            var dbCommand = _database.GetStoredProcCommand("[AAC].[usp_GetUserAccessPermissions]");
            _database.AddInParameter(dbCommand, "@in_UserMasterID", serviceUser.UserMasterID);
            _database.AddInParameter(dbCommand, "@in_ApplicationID", serviceUser.ApplicationID);

            using (var dataReader = await _database.ExecuteReaderAsync(dbCommand))
            {
                while (dataReader.Read())
                {
                    UserApplicationAccessPermissions userResponse = new UserApplicationAccessPermissions();
                    userResponse.ApplicationID = GetInt32Value(dataReader, "ApplicationID").Value;
                    userResponse.UserMasterID = GetInt32Value(dataReader, "UserMasterID").Value;
                    userResponse.PermissionID = (Permissions)GetInt32Value(dataReader, "PermissionID").Value;
                    userResponse.RoleID = GetInt32Value(dataReader, "RoleID").Value;
                    listResponse.Add(userResponse);
                }
            }
            return listResponse;
        }


     
        public async Task<string> PasswordLog_Search(long UserMasterID, string Token)
        {
            string passwor = "";

            var dbCommand = _database.GetStoredProcCommand("usr.SerachResetPassword_Log");
            _database.AddInParameter(dbCommand, "@UserMasterID", UserMasterID);
            _database.AddInParameter(dbCommand, "@Token", Token);

            using (var dataReader = await _database.ExecuteReaderAsync(dbCommand))
            {
                while (dataReader.Read())
                {
                    passwor = GetStringValue(dataReader, "Password");
                }
            }
            return passwor;
        }

      
       

    }
}
