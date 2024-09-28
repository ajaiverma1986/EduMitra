using EDUMITRA.Datamodel.Entities.Authorization;
using EDUMITRA.Datamodel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUMITRA.Datamodel.DTO.Response
{
    public class UserLoginResponse : BaseResponse
    {
        public String UserToken { get; set; }
        public String DisplayName { get; set; }
    }


    public class UserPersmissionsResponse
    {
        public String DisplayName { get; set; }
        public List<UserApplicationAccessPermissions> Permissions { get; set; }
    }
}
