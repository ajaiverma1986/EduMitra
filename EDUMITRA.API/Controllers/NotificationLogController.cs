namespace FIA.API.V1.Controllers
{
    using Audit.WebApi;
    using EDUMITRA.API.Common;
    using EDUMITRA.API.Interfaces;
    using EDUMITRA.API.Services;
    using EDUMITRA.Datamodel.Shared;
    using EDUMITRA.DataModel.Masters.Notification.NotificationLog;
    using EDUMITRA.Provider;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    [EnableCors("AllowAll")]
    [ResponseCache(Duration = -1, Location = ResponseCacheLocation.None, NoStore = true)]
    [ServiceFilter(typeof(EDUMITRAExceptionFilterService))]
    public class NotificationLogController : BaseApiController
    {
        private readonly IAuthenticationService _authenticationService = null;
        private readonly NotificationLogProvider NotificationLogProvider = null;

        public NotificationLogController()
        {
            _authenticationService = new AuthenticationService();
            NotificationLogProvider = new NotificationLogProvider();
        }

        [HttpPost]
        [AuditApi(EventTypeName = "POST NotificationLogController/NotificationLog_Search", IncludeHeaders = true, IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
        public async Task<IActionResult> NotificationLog_Search([FromBody] NotificationLogRequest request)
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

            response = await NotificationLogProvider.NotificationLog_Search(request, CallerUser);
            return Json(response);
        }

        [HttpGet]
        [AuditApi(EventTypeName = "POST NotificationLogController/NotificationLog_GetByID", IncludeHeaders = true, IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
        public async Task<IActionResult> NotificationLog_GetByID(byte NotificationLogID)
        {
            SimpleResponse response = new SimpleResponse();
            ErrorResponse error = new ErrorResponse(); //await _authenticationService.Validate(this.CallerUser, true);
            if (error.HasError)
            {
                response.SetError(error);
                return Json(response);
            }

            if (NotificationLogID < 1)
            {
                response.SetError(ErrorCodes.INVALID_PARAMETERS);
                return Json(response);
            }

            response.Result = await NotificationLogProvider.NotificationLog_GetByID(NotificationLogID, CallerUser);
            return Json(response);
        }

        [HttpGet]
        [AuditApi(EventTypeName = "POST NotificationLogController/NotificationLog_GetByUID", IncludeHeaders = true, IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
        public async Task<IActionResult> NotificationLog_GetByUID(Guid NotificationLogUID)
        {
            SimpleResponse response = new SimpleResponse();
            ErrorResponse error = new ErrorResponse(); //await _authenticationService.Validate(this.CallerUser, true);
            if (error.HasError)
            {
                response.SetError(error);
                return Json(response);
            }

            if (NotificationLogUID == null || NotificationLogUID == Guid.Empty)
            {
                response.SetError(ErrorCodes.INVALID_PARAMETERS);
                return Json(response);
            }

            response.Result = await NotificationLogProvider.NotificationLog_GetByUID(NotificationLogUID, CallerUser);
            return Json(response);
        }

        [HttpPost]
        [AuditApi(EventTypeName = "POST NotificationLogController/NotificationLog_AddUpdate", IncludeHeaders = true, IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
        public async Task<IActionResult> NotificationLog_AddUpdate([FromBody] NotificationLogResponse request)
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

            response = await NotificationLogProvider.NotificationLog_AddUpdate(request, CallerUser);

            return Json(response);
        }

        [HttpPost]
        [AuditApi(EventTypeName = "POST NotificationLogController/NotificationLog_UpdateStatus", IncludeHeaders = true, IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
        public async Task<IActionResult> NotificationLog_UpdateStatus([FromBody] NotificationLogStatusUpdateRequest request)
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

            int rowAffected = await NotificationLogProvider.NotificationLog_UpdateStatus(request, CallerUser);
            if (rowAffected == 0)
                response.SetError(ErrorCodes.SP_154);

            response.Result = rowAffected;

            return Json(response);
        }
    }
}