namespace EDUMITRA.DataModel.Masters.Notification.NotificationConfig
{
    using EDUMITRA.Datamodel.Common;
    using EDUMITRA.Datamodel.Library;
    using EDUMITRA.Datamodel.Shared;
    using EDUMITRA.DataModel.Masters.Notification.EmailGateway;
    using EDUMITRA.DataModel.Masters.Notification.SMSGateway;
    using EDUMITRA.DataModel.Masters.Organization;
    using System;
    using System.Data.SqlClient;

    public class NotificationConfigResponse
    {
        public byte? NotificationConfigID { get; set; }

        public Guid? NotificationConfigUID { get; set; }

        public int OrganizationID { get; set; }

        [SQLParam(Usage = SQLParamPlaces.None)]
        public OrganizationResponse Organization { get; set; }

        public byte SMSGatewayID { get; set; }

        [SQLParam(Usage = SQLParamPlaces.None)]
        public SMSGatewayResponse SMSGateway { get; set; }

        [SQLParam(Usage = SQLParamPlaces.None)]
        public string SMSGatewayName
        {
            get
            {
                return SMSGateway.GetDisplayName();
            }
        }

        public byte EmailGatewayID { get; set; }

        [SQLParam(Usage = SQLParamPlaces.None)]
        public EmailGatewayResponse EmailGateway { get; set; }

        [SQLParam(Usage = SQLParamPlaces.None)]
        public string EmailGatewayName
        {
            get
            {
                return EmailGateway.GetDisplayName();
            }
        }

        public string GCMKey { get; set; }

        public string iOSKey { get; set; }

        public string WindowsKey { get; set; }

        public string OuterEmailTemplate { get; set; }

        public Status? Status { get; set; }
        [SQLParam(Usage = SQLParamPlaces.None)]
        public string StatusName
        {
            get
            {
                return Status.GetDisplayName();
            }
        }

        public void FromReader(SqlDataReader reader)
        {
            NotificationConfigID = DataReaderHelper.Instance.GetDataReaderValue_Byte(reader, "NotificationConfigID");

            NotificationConfigUID = DataReaderHelper.Instance.GetDataReaderValue_Guid(reader, "NotificationConfigUID");

            OrganizationID = DataReaderHelper.Instance.GetDataReaderValue_Int(reader, "OrganizationID");

            SMSGatewayID = DataReaderHelper.Instance.GetDataReaderValue_Byte(reader, "SMSGatewayID");

            EmailGatewayID = DataReaderHelper.Instance.GetDataReaderValue_Byte(reader, "EmailGatewayID");

            GCMKey = DataReaderHelper.Instance.GetDataReaderNullableValue_String(reader, "GCMKey");

            iOSKey = DataReaderHelper.Instance.GetDataReaderNullableValue_String(reader, "iOSKey");

            WindowsKey = DataReaderHelper.Instance.GetDataReaderNullableValue_String(reader, "WindowsKey");

            OuterEmailTemplate = DataReaderHelper.Instance.GetDataReaderNullableValue_String(reader, "OuterEmailTemplate");

            Status = (Status)DataReaderHelper.Instance.GetDataReaderValue_Byte(reader, "Status");
        }
    }
}
