
using EDUMITRA.Datamodel.Common;
using EDUMITRA.Datamodel.Library;
using EDUMITRA.Datamodel.Shared;
using System;
using System.Data.SqlClient;

namespace EDUMITRA.DataModel.Masters.Notification.EmailGateway
{
    public class EmailGatewayResponse
    {
        public byte? EmailGatewayID { get; set; }

        public Guid? EmailGatewayUID { get; set; }

        public string SMTPServer { get; set; }

        public int SMTPPort { get; set; }

        public bool SMTPEnableSSL { get; set; }

        public string SMTPUsername { get; set; }

        [SQLParam(Usage = SQLParamPlaces.None)]
        public string EmailGatewayName { get; set; }

        public string SMTPPassword { get; set; }

        public string SMTPSenderEmail { get; set; }

        public string SMTPSenderName { get; set; }

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


        public void FromReader(SqlDataReader reader)
        {
            EmailGatewayID = DataReaderHelper.Instance.GetDataReaderValue_Byte(reader, "EmailGatewayID");

            EmailGatewayUID = DataReaderHelper.Instance.GetDataReaderValue_Guid(reader, "EmailGatewayUID");

            SMTPServer = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "SMTPServer");

            SMTPPort = DataReaderHelper.Instance.GetDataReaderValue_Int(reader, "SMTPPort");

            SMTPEnableSSL = DataReaderHelper.Instance.GetDataReaderValue_Bool(reader, "SMTPEnableSSL");

            SMTPUsername = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "SMTPUsername");

           // EmailGatewayName = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "EmailGatewayName");

            SMTPPassword = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "SMTPPassword");

            SMTPSenderEmail = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "SMTPSenderEmail");

            SMTPSenderName = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "SMTPSenderName");

            Status = (Status)DataReaderHelper.Instance.GetDataReaderValue_Byte(reader, "Status");
        }
    }
}