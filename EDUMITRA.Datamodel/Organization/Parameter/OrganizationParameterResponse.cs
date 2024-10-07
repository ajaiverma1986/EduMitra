using EDUMITRA.Datamodel.Shared;

namespace EDUMITRA.DataModel.Masters.Organization.Parameter
{

    public class OrganizationParameterResponse : BaseResponse
    {
        /// <summary>
        /// gets or sets ParameterName
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// gets or sets ParameterValue
        /// </summary>
        public string ParameterValue { get; set; }

        /// <summary>
        /// gets or sets OrgId
        /// </summary>
        public int? OrganizationID { get; set; }

        /// <summary>
        /// Get parameter id
        /// </summary>
        public int? PrameterId { get; set; }
    }
}