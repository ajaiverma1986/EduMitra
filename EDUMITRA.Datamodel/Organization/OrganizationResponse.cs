namespace EDUMITRA.DataModel.Masters.Organization
{
    using EDUMITRA.Datamodel.Common;
    using EDUMITRA.Datamodel.Library;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.SqlClient;

    public class OrganizationResponse
    {
        public int? OrganizationID { get; set; }

        public Guid? OrganizationUID { get; set; }
        public string OrganizationCode { get; set; }

        public string OrganizationName { get; set; }
        public string LegalName { get; set; }
        public string CIN { get; set; }
        public string TIN { get; set; }
        public string CST { get; set; }
        public string GST { get; set; }
        public long? CorporateAddressID { get; set; }
        public long? RegisteredAddressID { get; set; }
        public string ContactInformation { get; set; }

        public Status Status { get; set; }

        public void FromReader(SqlDataReader reader)
        {
            OrganizationID = DataReaderHelper.Instance.GetDataReaderValue_Int(reader, "OrganizationID");

            OrganizationUID= DataReaderHelper.Instance.GetDataReaderValue_Guid(reader, "OrganizationUID");

            OrganizationCode = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "OrganizationCode");

            OrganizationName = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "OrganizationName");

            LegalName = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "LegalName");

            CIN = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "CIN");

            TIN = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "TIN");

            CST = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "CST");

            GST = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "GST");

            CorporateAddressID = DataReaderHelper.Instance.GetDataReaderValue_Long(reader, "CorporateAddressID");

            RegisteredAddressID = DataReaderHelper.Instance.GetDataReaderValue_Long(reader, "RegisteredAddressID");

            ContactInformation = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "ContactInformation");

            Status = (Status)DataReaderHelper.Instance.GetDataReaderValue_Short(reader, "Status");

        }
    }
}
