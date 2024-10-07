namespace EDUMITRA.DataModel.Masters.Organization.Configuration
{
    using EDUMITRA.Datamodel.Common;
    using System;

    public class OrganizationConfigurationRequest
    {
        public Guid? OrganizationConfigurationUID { get; set; }

        public string TLD { get; set; }

        public string MasterAPIToken { get; set; }

        public ApplicationTypes? ApplicationTypeID { get; set; }

        public Platform? PlatformID { get; set; }
    }
}