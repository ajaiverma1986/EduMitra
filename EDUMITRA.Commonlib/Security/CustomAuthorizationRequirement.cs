using EDUMITRA.Datamodel.Common;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace EDUMITRA.Commonlib.Security
{
    public class CustomAuthorizationRequirement : IAuthorizationRequirement
    {
        public Permissions Permission { get; set; }
    }
}
