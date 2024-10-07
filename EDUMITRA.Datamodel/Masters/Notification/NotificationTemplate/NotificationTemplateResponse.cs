namespace EDUMITRA.DataModel.Masters.Notification.NotificationTemplate
{
    using EDUMITRA.Datamodel.Common;
    using EDUMITRA.Datamodel.Library;
    using EDUMITRA.Datamodel.Shared;
    using EDUMITRA.DataModel.Masters.Notification.NotificationCategory;
    using EDUMITRA.DataModel.Masters.Notification.NotificationConfig;
    using EDUMITRA.DataModel.Masters.Organization;
    using System;
    using System.Data.SqlClient;

    public class NotificationTemplateResponse
    {
        public int? NotificationTemplateID { get; set; }

        public Guid? NotificationTemplateUID { get; set; }

        public byte NotificationCategoryID { get; set; }

        public byte NotificationConfigID { get; set; }

        public int OrganizationID { get; set; }

        public int? NotificationMediums { get; set; }

        public string NotificationTemplate { get; set; }

        public string NotificationTemplateDescription { get; set; }

        [SQLParam(Usage = SQLParamPlaces.None)]
        public bool SendSMS
        {
            get
            {
                if (string.IsNullOrWhiteSpace(SmsTemplate))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

       
        public string SmsTemplate { get; set; }

        [SQLParam(Usage = SQLParamPlaces.None)]
        public bool SendEMail
        {
            get
            {
                if (string.IsNullOrWhiteSpace(EmailSubject) || string.IsNullOrWhiteSpace(EmailBody))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public string EmailSubject { get; set; }

        public string EmailBody { get; set; }

        public string AndroidText { get; set; }

        public string iOSText { get; set; }

        public string WindowsText { get; set; }

        [SQLParam(Usage = SQLParamPlaces.None)]
        public Status? Status { get; set; }


        [SQLParam(Usage = SQLParamPlaces.None)]
        public string StatusName
        {
            get
            {
                return Status.GetDisplayName();
            }
        }

        [SQLParam(Usage = SQLParamPlaces.None)]
        public OrganizationResponse Organization { get; set; }
        [SQLParam(Usage = SQLParamPlaces.None)]
        public NotificationCategoryResponse NotificationCategory { get; set; }
        [SQLParam(Usage = SQLParamPlaces.None)]
        public NotificationConfigResponse NotificationConfig { get; set; }

        public void FromReader(SqlDataReader reader)
        {
            NotificationTemplateID = DataReaderHelper.Instance.GetDataReaderValue_Int(reader, "NotificationTemplateID");

            NotificationTemplateUID = DataReaderHelper.Instance.GetDataReaderValue_Guid(reader, "NotificationTemplateUID");

            NotificationCategoryID = DataReaderHelper.Instance.GetDataReaderValue_Byte(reader, "NotificationCategoryID");

            NotificationConfigID = DataReaderHelper.Instance.GetDataReaderValue_Byte(reader, "NotificationConfigID");

            OrganizationID = DataReaderHelper.Instance.GetDataReaderValue_Int(reader, "OrganizationID");

            NotificationMediums = DataReaderHelper.Instance.GetDataReaderNullableValue_Int(reader, "NotificationMediums");

            NotificationTemplate = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "NotificationTemplate");

            NotificationTemplateDescription = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "NotificationTemplateDescription");

            SmsTemplate = DataReaderHelper.Instance.GetDataReaderNullableValue_String(reader, "SmsTemplate");

            EmailSubject = DataReaderHelper.Instance.GetDataReaderNullableValue_String(reader, "EmailSubject");

            EmailBody = DataReaderHelper.Instance.GetDataReaderNullableValue_String(reader, "EmailBody");

            AndroidText = DataReaderHelper.Instance.GetDataReaderNullableValue_String(reader, "AndroidText");

            iOSText = DataReaderHelper.Instance.GetDataReaderNullableValue_String(reader, "iOSText");

            WindowsText = DataReaderHelper.Instance.GetDataReaderNullableValue_String(reader, "WindowsText");

            Status = (Status)DataReaderHelper.Instance.GetDataReaderValue_Byte(reader, "Status");
        }
    }
}