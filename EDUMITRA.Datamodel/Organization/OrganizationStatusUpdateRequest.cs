using EDUMITRA.Datamodel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EDUMITRA.DataModel.Masters.Organization
{
    public class OrganizationStatusUpdateRequest
    {
        public Guid OrganizationUID { get; set; }

        public Status Status { get; set; }
    }
}
