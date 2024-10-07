using Audit.WebApi;
using EDUMITRA.API.Common;
using EDUMITRA.API.Interfaces;
using EDUMITRA.API.Services;
using EDUMITRA.Datamodel.Shared;
using EDUMITRA.DataModel.Masters.Notification.NotificationConfig;
using EDUMITRA.Provider;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EDUMITRA.API.V1.Controllers
{
    [EnableCors("AllowAll")]
    [ResponseCache(Duration = -1, Location = ResponseCacheLocation.None, NoStore = true)]
    [ServiceFilter(typeof(EDUMITRAExceptionFilterService))]
    public class NotificationConfigController : BaseApiController
    {
        private readonly IAuthenticationService _authenticationService = null;
        private readonly NotificationConfigProvider NotificationConfigProvider = null;

        public NotificationConfigController()
        {
            _authenticationService = new AuthenticationService();
            NotificationConfigProvider = new NotificationConfigProvider();
        }

        [HttpPost]
        [AuditApi(EventTypeName = "POST NotificationConfigController/NotificationConfig_Search", IncludeHeaders = true, IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
        public async Task<IActionResult> NotificationConfig_Search([FromBody] NotificationConfigRequest request)
        {
            ListResponse response = new ListResponse();
            ErrorResponse error = await _authenticationService.Validate(this.CallerUser, true);
            if (error.HasError)
            {
                response.SetError(error);
                return Json(response);
            }

            if (request == null)
            {
                response.SetError(ErrorCodes.INVALID_PARAMETERS);
                return Json(response);
            }

            response = await NotificationConfigProvider.NotificationConfig_Search(request, CallerUser);
            return Json(response);
        }

        [HttpGet]
        [AuditApi(EventTypeName = "POST NotificationConfigController/NotificationConfig_GetByID", IncludeHeaders = true, IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
        public async Task<IActionResult> NotificationConfig_GetByID(byte NotificationConfigID)
        {
            SimpleResponse response = new SimpleResponse();
            ErrorResponse error = new ErrorResponse(); //await _authenticationService.Validate(this.CallerUser, true);
            if (error.HasError)
            {
                response.SetError(error);
                return Json(response);
            }

            if (NotificationConfigID < 1)
            {
                response.SetError(ErrorCodes.INVALID_PARAMETERS);
                return Json(response);
            }

            response.Result = await NotificationConfigProvider.NotificationConfig_GetByID(NotificationConfigID, CallerUser);
            return Json(response);
        }

        [HttpGet]
        [AuditApi(EventTypeName = "POST NotificationConfigController/NotificationConfig_GetByUID", IncludeHeaders = true, IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
        public async Task<IActionResult> NotificationConfig_GetByUID(Guid NotificationConfigUID)
        {
            SimpleResponse response = new SimpleResponse();
            ErrorResponse error = new ErrorResponse(); //await _authenticationService.Validate(this.CallerUser, true);
            if (error.HasError)
            {
                response.SetError(error);
                return Json(response);
            }

            if (NotificationConfigUID == null || NotificationConfigUID == Guid.Empty)
            {
                response.SetError(ErrorCodes.INVALID_PARAMETERS);
                return Json(response);
            }

            response.Result = await NotificationConfigProvider.NotificationConfig_GetByUID(NotificationConfigUID, CallerUser);
            return Json(response);
        }

        [HttpPost]
        [AuditApi(EventTypeName = "POST NotificationConfigController/NotificationConfig_AddUpdate", IncludeHeaders = true, IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
        public async Task<IActionResult> NotificationConfig_AddUpdate([FromBody] NotificationConfigResponse request)
        {
            SimpleResponse response = new SimpleResponse();
            ErrorResponse error = await _authenticationService.Validate(this.CallerUser, true);
            if (error.HasError)
            {
                response.SetError(error);
                return Json(response);
            }

            if (request == null)
            {
                response.SetError(ErrorCodes.INVALID_PARAMETERS);
                return Json(response);
            }

            error = IsModelValidNew();
            if (error.HasError)
            {
                response.SetError(error);
                return Json(response);
            }

            response = await NotificationConfigProvider.NotificationConfig_AddUpdate(request, CallerUser);

            return Json(response);
        }

        [HttpPost]
        [AuditApi(EventTypeName = "POST NotificationConfigController/NotificationConfig_UpdateStatus", IncludeHeaders = true, IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
        public async Task<IActionResult> NotificationConfig_UpdateStatus([FromBody] NotificationConfigStatusUpdateRequest request)
        {
            SimpleResponse response = new SimpleResponse();
            ErrorResponse error = await _authenticationService.Validate(this.CallerUser, true);
            if (error.HasError)
            {
                response.SetError(error);
                return Json(response);
            }

            if (request == null)
            {
                response.SetError(ErrorCodes.INVALID_PARAMETERS);
                return Json(response);
            }

            error = IsModelValidNew();
            if (error.HasError)
            {
                response.SetError(error);
                return Json(response);
            }

            int rowAffected = await NotificationConfigProvider.NotificationConfig_UpdateStatus(request, CallerUser);
            if (rowAffected == 0)
                response.SetError(ErrorCodes.SP_154);

            response.Result = rowAffected;

            return Json(response);
        }
    }
}