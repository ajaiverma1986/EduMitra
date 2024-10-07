
using EDUMITRA.Datamodel.Common;
using EDUMITRA.Datamodel.Library;
using EDUMITRA.Datamodel.Shared;
using System;
using System.Data.SqlClient;

namespace EDUMITRA.DataModel.Masters.Notification.SMSGateway
{
    public class SMSGatewayResponse
    {
        public byte? SMSGatewayID { get; set; }

        public Guid? SMSGatewayUID { get; set; }

        public string SMSGatewayName { get; set; }

        public string GatewayURL { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string SenderID { get; set; }

        public string ReponseSuccess { get; set; }

        public string ReponseError { get; set; }

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
            SMSGatewayID = DataReaderHelper.Instance.GetDataReaderValue_Byte(reader, "SMSGatewayID");

            SMSGatewayUID = DataReaderHelper.Instance.GetDataReaderValue_Guid(reader, "SMSGatewayUID");

            SMSGatewayName = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "SMSGatewayName");

            GatewayURL = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "GatewayURL");

            UserName = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "UserName");

            Password = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "Password");

            SenderID = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "SenderID");

            ReponseSuccess = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "ReponseSuccess");

            ReponseError = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "ReponseError");

            Status = (Status)DataReaderHelper.Instance.GetDataReaderValue_Byte(reader, "Status");
        }

    }
}
