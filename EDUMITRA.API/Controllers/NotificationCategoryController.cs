using Audit.WebApi;
using EDUMITRA.API.Common;
using EDUMITRA.API.Interfaces;
using EDUMITRA.API.Services;
using EDUMITRA.Datamodel.Shared;
using EDUMITRA.DataModel.Masters.Notification.NotificationCategory;
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
    public class NotificationCategoryController : BaseApiController
    {
        private readonly IAuthenticationService _authenticationService = null;
        private readonly NotificationCategoryProvider NotificationCategoryProvider = null;

        public NotificationCategoryController()
        {
            _authenticationService = new AuthenticationService();
            NotificationCategoryProvider = new NotificationCategoryProvider();
        }

        [HttpPost]
        [AuditApi(EventTypeName = "POST NotificationCategoryController/NotificationCategory_Search", IncludeHeaders = true, IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
        public async Task<IActionResult> NotificationCategory_Search([FromBody] NotificationCategoryRequest request)
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

            response = await NotificationCategoryProvider.NotificationCategory_Search(request, CallerUser);
            return Json(response);
        }

        [HttpGet]
        [AuditApi(EventTypeName = "POST NotificationCategoryController/NotificationCategory_GetByID", IncludeHeaders = true, IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
        public async Task<IActionResult> NotificationCategory_GetByID(byte NotificationCategoryID)
        {
            SimpleResponse response = new SimpleResponse();
            ErrorResponse error = new ErrorResponse(); //await _authenticationService.Validate(this.CallerUser, true);
            if (error.HasError)
            {
                response.SetError(error);
                return Json(response);
            }

            if (NotificationCategoryID < 1)
            {
                response.SetError(ErrorCodes.INVALID_PARAMETERS);
                return Json(response);
            }

            response.Result = await NotificationCategoryProvider.NotificationCategory_GetByID(NotificationCategoryID, CallerUser);
            return Json(response);
        }

        [HttpGet]
        [AuditApi(EventTypeName = "POST NotificationCategoryController/NotificationCategory_GetByUID", IncludeHeaders = true, IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
        public async Task<IActionResult> NotificationCategory_GetByUID(Guid NotificationCategoryUID)
        {
            SimpleResponse response = new SimpleResponse();
            ErrorResponse error = new ErrorResponse(); //await _authenticationService.Validate(this.CallerUser, true);
            if (error.HasError)
            {
                response.SetError(error);
                return Json(response);
            }

            if (NotificationCategoryUID == null || NotificationCategoryUID == Guid.Empty)
            {
                response.SetError(ErrorCodes.INVALID_PARAMETERS);
                return Json(response);
            }

            response.Result = await NotificationCategoryProvider.NotificationCategory_GetByUID(NotificationCategoryUID, CallerUser);
            return Json(response);
        }

        [HttpPost]
        [AuditApi(EventTypeName = "POST NotificationCategoryController/NotificationCategory_AddUpdate", IncludeHeaders = true, IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
        public async Task<IActionResult> NotificationCategory_AddUpdate([FromBody] NotificationCategoryResponse request)
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

            response = await NotificationCategoryProvider.NotificationCategory_AddUpdate(request, CallerUser);

            return Json(response);
        }

        [HttpPost]
        [AuditApi(EventTypeName = "POST NotificationCategoryController/NotificationCategory_UpdateStatus", IncludeHeaders = true, IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
        public async Task<IActionResult> NotificationCategory_UpdateStatus([FromBody] NotificationCategoryStatusUpdateRequest request)
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

            int rowAffected = await NotificationCategoryProvider.NotificationCategory_UpdateStatus(request, CallerUser);
            if (rowAffected == 0)
                response.SetError(ErrorCodes.SP_154);

            response.Result = rowAffected;

            return Json(response);
        }
    }
}