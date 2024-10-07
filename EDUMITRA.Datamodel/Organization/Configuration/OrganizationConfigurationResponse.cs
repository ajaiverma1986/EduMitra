namespace EDUMITRA.DataModel.Masters.Organization.Configuration
{
    using EDUMITRA.Datamodel.Common;
    using EDUMITRA.Datamodel.Library;
    using System;
    using System.Data.SqlClient;

    public class OrganizationConfigurationResponse
    {
        public int? OrganizationID { get; set; }


        public Guid? OrganizationConfigurationUID { get; set; }

        public OrganizationResponse Organization { get; set; }

        public string APIKey { get; set; }

        public string ConfigurationKey { get; set; }

        public ConfigurationMethods ConfigurationMethodID { get; set; }

        public Platform PlatformID { get; set; }

        public long? LogoAssetID { get; set; }

        public long? FaviconAssetID { get; set; }

        public long? UITemplateID_IOS { get; set; }

        public long? UITemplateID_Android { get; set; }

        public long? UITemplateID_Web { get; set; }

        public Status Status { get; set; }

        public void FromReader(SqlDataReader reader)
        {
            OrganizationID = DataReaderHelper.Instance.GetDataReaderValue_Int(reader, "OrganizationID");
            ConfigurationKey = DataReaderHelper.Instance.GetDataReaderNullableValue_String(reader, "ConfigurationKey");
            APIKey = DataReaderHelper.Instance.GetDataReaderNullableValue_String(reader, "APIKey");
            ConfigurationMethodID = (ConfigurationMethods)DataReaderHelper.Instance.GetDataReaderValue_Int(reader, "ConfigurationMethodID");
            PlatformID = (Platform)DataReaderHelper.Instance.GetDataReaderValue_Int(reader, "ConfigurationMethodID");
            LogoAssetID = DataReaderHelper.Instance.GetNullDataReaderValue_Long(reader, "LogoAssetID");
            FaviconAssetID = DataReaderHelper.Instance.GetNullDataReaderValue_Long(reader, "FaviconAssetID");

            UITemplateID_IOS = DataReaderHelper.Instance.GetNullDataReaderValue_Long(reader, "UITemplateID_IOS");
            UITemplateID_Android = DataReaderHelper.Instance.GetNullDataReaderValue_Long(reader, "UITemplateID_Android");
            UITemplateID_Web = DataReaderHelper.Instance.GetNullDataReaderValue_Long(reader, "UITemplateID_Web");

            Status = (Status)DataReaderHelper.Instance.GetDataReaderValue_Int(reader, "Status");
        }
    }
}