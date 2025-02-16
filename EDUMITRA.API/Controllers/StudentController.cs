using Audit.WebApi;
using EDUMITRA.API.Common;
using EDUMITRA.API.Security;
using EDUMITRA.Datamodel.Masters;
using EDUMITRA.Datamodel.Shared;
using EDUMITRA.Datamodel.Student;
using EDUMITRA.Provider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyModel.Resolution;
using System.Threading.Tasks;

namespace EDUMITRA.API.Controllers
{
    public class StudentController : BaseApiController
    {
        private readonly StudentProvider _provider=null;
        private AuthenticationHelper _callValidator = null;
        public StudentController() { 
            _provider=new StudentProvider();
            _callValidator = new AuthenticationHelper();
        }
        [HttpPost]
        [AuditApi(EventTypeName = "POST StudentController/AddNewStudentRegistration", IncludeHeaders = true, IncludeResponseBody = true, IncludeRequestBody = false, IncludeModelState = false)]
        public async Task<IActionResult> StudentRegistration([FromBody] StudentRegistrationRequest request)
        {
            SimpleResponse response = new SimpleResponse();
            ErrorResponse error = await _callValidator.AuthenticateAndAuthorize(this.CallerUser, false);
            if (error.HasError)
            {
                response.SetError(error);
                return Json(response);
            }
            response = await _provider.AddNewStudentRegistration(request,this.CallerUser);
            return Json(response);
        }
        [HttpPost]
        [AuditApi(EventTypeName = "POST StudentController/ListStudent", IncludeHeaders = true, IncludeResponseBody = true, IncludeRequestBody = false, IncludeModelState = false)]
        public async Task<IActionResult> ListStudent([FromBody] StudentListRequest request)
        {
            ListResponse response = new ListResponse();
            ErrorResponse error = await _callValidator.AuthenticateAndAuthorize(this.CallerUser, false);
            if (error.HasError)
            {
                response.SetError(error);
                return Json(response);
            }
            response = await _provider.ListStudent(request, this.CallerUser);
            return Json(response);
        }
    }
}
