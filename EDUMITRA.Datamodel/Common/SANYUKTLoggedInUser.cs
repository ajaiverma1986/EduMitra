﻿using EDUMITRA.Datamodel.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EDUMITRA.Datamodel.Common
{
    public class SANYUKTLoggedInUser: IEDUMITRALoggedInUser
    {
        public String Email { get; set; }
        public String DisplayName { get; set; }
        public List<String> RolePermissions { get; set; }
        public String UserToken { get; set; }
        public String ApiToken { get; set; }
        public String IPAddress { get; set; }
        public Int64 UserMasterID { get; set; }
        public String UserName { get; set; }
        //public OrganizationConfigurationResponse OrganizationConfiguration { get; set; }
    }
}
