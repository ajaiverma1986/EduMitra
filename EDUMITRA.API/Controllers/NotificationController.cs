using Audit.WebApi;
using EDUMITRA.API.Common;
using EDUMITRA.API.Interfaces;
using EDUMITRA.API.Services;
using EDUMITRA.Datamodel.Shared;
using EDUMITRA.DataModel.Masters.Notification;
using FIA.Provider;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FIA.API.V1.Controllers
{
    [EnableCors("AllowAll")]
    [ResponseCache(Duration = -1, Location = ResponseCacheLocation.None, NoStore = true)]
    [ServiceFilter(typeof(EDUMITRAExceptionFilterService))]
    public class NotificationController : BaseApiController
    {
        private readonly IAuthenticationService _authenticationService = null;
        private readonly NotificationProvider NotificationProvider = null;

        public NotificationController()
        {
            _authenticationService = new AuthenticationService();
            NotificationProvider = new NotificationProvider();
        }

        [HttpPost]
        [AuditApi(EventTypeName = "GET  NotificationController/ Send", IncludeHeaders = true, IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
        public async Task<IActionResult> Send([FromBody]NotificationRequest request)
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

            response = await NotificationProvider.Send(request, CallerUser);
            return Json(response);
        }

        [HttpGet]
        [AuditApi(EventTypeName = "GET  NotificationController/ Notification_SendEMail", IncludeHeaders = true, IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
        public async Task<IActionResult> Notification_SendEMail(string TO, string subjectName, string body, string CC = null, string BCC = null, List<Attachment> Files = null)
        {
            SimpleResponse response = new SimpleResponse();
            ErrorResponse error = await _authenticationService.Validate(this.CallerUser, true);
            if (error.HasError)
            {
                response.SetError(error);
                return Json(response);
            }

            if (string.IsNullOrWhiteSpace(TO) || string.IsNullOrWhiteSpace(subjectName) || string.IsNullOrWhiteSpace(body))
            {
                response.SetError(ErrorCodes.INVALID_PARAMETERS);
                return Json(response);
            }

            response = await NotificationProvider.SendEMail(TO, subjectName, body, CallerUser, CC, BCC, null, Files);
            return Json(response);
        }

        [HttpGet]
        [AuditApi(EventTypeName = "GET  NotificationController/ Notification_SendSMS", IncludeHeaders = true, IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
        public async Task<IActionResult> Notification_SendSMS(string TO, string Message)
        {
            SimpleResponse response = new SimpleResponse();
            ErrorResponse error = new ErrorResponse(); //await _authenticationService.Validate(this.CallerUser, true);
            if (error.HasError)
            {
                response.SetError(error);
                return Json(response);
            }

            if (string.IsNullOrWhiteSpace(TO) || string.IsNullOrWhiteSpace(Message))
            {
                response.SetError(ErrorCodes.INVALID_PARAMETERS);
                return Json(response);
            }

            response = await NotificationProvider.SendSMS(TO, Message, CallerUser);
            return Json(response);
        }
    }
}