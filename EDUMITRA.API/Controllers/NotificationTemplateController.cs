using Audit.WebApi;
using EDUMITRA.API.Common;
using EDUMITRA.API.Interfaces;
using EDUMITRA.API.Services;
using EDUMITRA.Datamodel.Shared;
using EDUMITRA.DataModel.Masters.Notification.NotificationTemplate;
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
    public class NotificationTemplateController : BaseApiController
    {
        private readonly IAuthenticationService _authenticationService = null;
        private readonly NotificationTemplateProvider NotificationTemplateProvider = null;

        public NotificationTemplateController()
        {
            _authenticationService = new AuthenticationService();
            NotificationTemplateProvider = new NotificationTemplateProvider();
        }

        [HttpPost]
        [AuditApi(EventTypeName = "POST NotificationTemplateController/NotificationTemplate_Search", IncludeHeaders = true, IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
        public async Task<IActionResult> NotificationTemplate_Search([FromBody] NotificationTemplateRequest request)
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

            response = await NotificationTemplateProvider.NotificationTemplate_Search(request, CallerUser);
            return Json(response);
        }

        [HttpGet]
        [AuditApi(EventTypeName = "POST NotificationTemplateController/NotificationTemplate_GetByID", IncludeHeaders = true, IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
        public async Task<IActionResult> NotificationTemplate_GetByID(byte NotificationTemplateID)
        {
            SimpleResponse response = new SimpleResponse();
            ErrorResponse error = new ErrorResponse(); //await _authenticationService.Validate(this.CallerUser, true);
            if (error.HasError)
            {
                response.SetError(error);
                return Json(response);
            }

            if (NotificationTemplateID < 1)
            {
                response.SetError(ErrorCodes.INVALID_PARAMETERS);
                return Json(response);
            }

            response.Result = await NotificationTemplateProvider.NotificationTemplate_GetByID(NotificationTemplateID, CallerUser);
            return Json(response);
        }

        [HttpGet]
        [AuditApi(EventTypeName = "POST NotificationTemplateController/NotificationTemplate_GetByUID", IncludeHeaders = true, IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
        public async Task<IActionResult> NotificationTemplate_GetByUID(Guid NotificationTemplateUID)
        {
            SimpleResponse response = new SimpleResponse();
            ErrorResponse error = new ErrorResponse(); //await _authenticationService.Validate(this.CallerUser, true);
            if (error.HasError)
            {
                response.SetError(error);
                return Json(response);
            }

            if (NotificationTemplateUID == null || NotificationTemplateUID == Guid.Empty)
            {
                response.SetError(ErrorCodes.INVALID_PARAMETERS);
                return Json(response);
            }

            response.Result = await NotificationTemplateProvider.NotificationTemplate_GetByUID(NotificationTemplateUID, CallerUser);
            return Json(response);
        }

        [HttpPost]
        [AuditApi(EventTypeName = "POST NotificationTemplateController/NotificationTemplate_AddUpdate", IncludeHeaders = true, IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
        public async Task<IActionResult> NotificationTemplate_AddUpdate([FromBody] NotificationTemplateResponse request)
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

            response = await NotificationTemplateProvider.NotificationTemplate_AddUpdate(request, CallerUser);

            return Json(response);
        }

        [HttpPost]
        [AuditApi(EventTypeName = "POST NotificationTemplateController/NotificationTemplate_UpdateStatus", IncludeHeaders = true, IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
        public async Task<IActionResult> NotificationTemplate_UpdateStatus([FromBody] NotificationTemplateStatusUpdateRequest request)
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

            int rowAffected = await NotificationTemplateProvider.NotificationTemplate_UpdateStatus(request, CallerUser);
            if (rowAffected == 0)
                response.SetError(ErrorCodes.SP_154);

            response.Result = rowAffected;

            return Json(response);
        }
    }
}