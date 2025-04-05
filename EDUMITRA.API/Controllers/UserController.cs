using EDUMITRA.API.Common;
using EDUMITRA.API.Security;
using EDUMITRA.Datamodel.Shared;
using EDUMITRA.Provider;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EDUMITRA.API.Controllers
{
    [EnableCors("AllowAll")]
    [ResponseCache(Duration = -1, Location = ResponseCacheLocation.None, NoStore = true)]
    [ServiceFilter(typeof(EDUMITRAExceptionFilterService))]

    public class UserController : BaseApiController
    {
        public readonly UserDetailsProvider _Provider;
        private AuthenticationHelper _callValidator = null;
        private readonly AuthenticationProvider _authenticationProvider;
        public UserController()
        {
            _authenticationProvider = new AuthenticationProvider();
            _Provider = new UserDetailsProvider();
            _callValidator = new AuthenticationHelper();
        }
        [HttpGet]
        public async Task<IActionResult> ListAllMenu()
        {
            SimpleResponse response = new SimpleResponse();
            ErrorResponse error = await _callValidator.AuthenticateAndAuthorize(this.CallerUser, true, true);
            if (error.HasError)
            {
                response.SetError(error);
                return Json(response);
            }
            response.Result = await _Provider.GetallMenu(CallerUser);
            return Json(response);
        }
        [HttpGet]
        public async Task<IActionResult> ListAllsubMenu(int Menuid)
        {
            SimpleResponse response = new SimpleResponse();
            ErrorResponse error = await _callValidator.AuthenticateAndAuthorize(this.CallerUser, true, true);
            if (error.HasError)
            {
                response.SetError(error);
                return Json(response);
            }
            response.Result = await _Provider.GetallSubMenu(Menuid, CallerUser);
            return Json(response);
        }

    }
}
