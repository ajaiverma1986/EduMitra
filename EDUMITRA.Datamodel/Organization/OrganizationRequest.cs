namespace EDUMITRA.DataModel.Masters.Organization
{
    using EDUMITRA.Datamodel.Common;
    using EDUMITRA.Datamodel.Shared;
    using System;

    public class OrganizationRequest : ListRequest
    {
        public int? OrganizationID { get; set; }

        public Guid? OrganizationUID { get; set; }

        public string OrganizationCode { get; set; }

        public string OrganizationName { get; set; }

        public Status? Status { get; set; }
    }
}