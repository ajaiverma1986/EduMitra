namespace EDUMITRA.DataModel.Masters.Notification.NotificationLog
{
    using EDUMITRA.Datamodel.Common;
    using EDUMITRA.Datamodel.Library;
    using EDUMITRA.Datamodel.Shared;
    using EDUMITRA.DataModel.Masters.Organization;
    using System;
    using System.Data.SqlClient;

    public class NotificationLogResponse
    {
        public long? NotificationLogID { get; set; }

        public Guid? NotificationLogUID { get; set; }

        public int? NotificationTemplateID { get; set; }

        [SQLParam(Usage = SQLParamPlaces.None)]
        public int? ApplicationID { get; private set; }

        [SQLParam(Usage = SQLParamPlaces.None)]
        public int? OrganizationID { get; private set; }

        [SQLParam(Usage = SQLParamPlaces.None)]
        public OrganizationResponse Organization { get; set; }

        [SQLParam(Usage = SQLParamPlaces.None)]
        public int? WorkOrganizationID { get; private set; }

        //[SQLParam(Usage = SQLParamPlaces.None)]
        public NotificationMediumType NotificationMediumTypeID { get; set; }

        [SQLParam(Usage = SQLParamPlaces.None)]
        public string NotificationMediumType
        {
            get
            {
                return Status.GetDisplayName();
            }
        }

        public long? UserMasterID { get; set; }

        public string SentTo { get; set; }

        public string SentToName { get; set; }

        public string CC { get; set; }

        public string BCC { get; set; }

        public string Subject { get; set; }

        public string MessageBody { get; set; }

        [SQLParam(Usage = SQLParamPlaces.None)]
        public string LastMessageSendResponse { get; set; }

        [SQLParam(Usage = SQLParamPlaces.None)]
        public DateTimeOffset? LastRetryOn { get; set; }

        [SQLParam(Usage = SQLParamPlaces.None)]
        public short? SendCount { get; set; }

        [SQLParam(Usage = SQLParamPlaces.None)]
        public NotificationLogStatus? Status { get; set; }

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
            NotificationLogID = DataReaderHelper.Instance.GetDataReaderValue_Long(reader, "NotificationLogID");

            NotificationLogUID = DataReaderHelper.Instance.GetDataReaderValue_Guid(reader, "NotificationLogUID");

            NotificationTemplateID = DataReaderHelper.Instance.GetDataReaderNullableValue_Int(reader, "NotificationTemplateID");

            ApplicationID = DataReaderHelper.Instance.GetDataReaderNullableValue_Int(reader, "ApplicationID");

            OrganizationID = DataReaderHelper.Instance.GetDataReaderNullableValue_Int(reader, "OrganizationID");

            WorkOrganizationID = DataReaderHelper.Instance.GetDataReaderNullableValue_Int(reader, "WorkOrganizationID");

            NotificationMediumTypeID = (NotificationMediumType)DataReaderHelper.Instance.GetDataReaderValue_Byte(reader, "NotificationMediumTypeID");

            UserMasterID = DataReaderHelper.Instance.GetNullDataReaderValue_Long(reader, "UserMasterID");

            SentTo = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "SentTo");

            SentToName = DataReaderHelper.Instance.GetDataReaderNullableValue_String(reader, "SentToName");

            CC = DataReaderHelper.Instance.GetDataReaderNullableValue_String(reader, "CC");

            BCC = DataReaderHelper.Instance.GetDataReaderNullableValue_String(reader, "BCC");

            Subject = DataReaderHelper.Instance.GetDataReaderNullableValue_String(reader, "Subject");

            MessageBody = DataReaderHelper.Instance.GetDataReaderNullableValue_String(reader, "MessageBody");

            LastMessageSendResponse = DataReaderHelper.Instance.GetDataReaderNullableValue_String(reader, "LastMessageSendResponse");

            LastRetryOn = DataReaderHelper.Instance.GetNullDataReaderValue_DateTimeOffset(reader, "LastRetryOn");

            SendCount = DataReaderHelper.Instance.GetDataReaderValue_Short(reader, "SendCount");

            Status = (NotificationLogStatus)DataReaderHelper.Instance.GetDataReaderValue_Byte(reader, "Status");
        }
    }
}