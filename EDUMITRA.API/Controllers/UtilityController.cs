using Audit.WebApi;
using EDUMITRA.API.Common;
using EDUMITRA.API.Security;
using EDUMITRA.Datamodel.Masters;
using EDUMITRA.Datamodel.Shared;
using EDUMITRA.Provider;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EDUMITRA.API.Controllers
{
    public class UtilityController : BaseApiController
    {
        private readonly UtilityProvider provider = null;
        private AuthenticationHelper _callValidator = null;
        public UtilityController()
        {
            provider = new UtilityProvider();
            _callValidator = new AuthenticationHelper();
        }

   
        [HttpPost]
        [AuditApi(EventTypeName = "POST UtilityController/SendOTP", IncludeHeaders = true, IncludeResponseBody = true, IncludeRequestBody = false, IncludeModelState = false)]
        public async Task<IActionResult> SendOTP([FromBody] OTPRequest request)
        {
            OTPResponse response = new OTPResponse();
            ErrorResponse error = await _callValidator.AuthenticateAndAuthorize(this.CallerUser, false);
            if (error.HasError)
            {
                response.response_message = "Authentication Fail";
                return Json(response);
            }
            response = await provider.SendOTP(request);
            return Json(response);
        }

        [HttpPost]
        [AuditApi(EventTypeName = "POST UtilityController/ValidateOTP", IncludeHeaders = true, IncludeResponseBody = true, IncludeRequestBody = false, IncludeModelState = false)]
        public async Task<IActionResult> ValidateOTP([FromBody] OTPValidateRequest request)
        {
            SimpleResponse response = new SimpleResponse();
            ErrorResponse error = await _callValidator.AuthenticateAndAuthorize(this.CallerUser, false);
            if (error.HasError)
            {
                response.SetError(error);
                return Json(response);
            }
            response = await provider.ValidateOTP(request);
            return Json(response);
        }
        
    }
}
