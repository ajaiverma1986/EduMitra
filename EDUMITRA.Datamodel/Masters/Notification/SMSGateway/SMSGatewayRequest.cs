using EDUMITRA.Datamodel.Common;
using EDUMITRA.Datamodel.Shared;
using System;

namespace EDUMITRA.DataModel.Masters.Notification.SMSGateway
{
    public class SMSGatewayRequest:ListRequest
    {
        public byte? SMSGatewayID { get; set; }

        public Guid? SMSGatewayUID { get; set; }

        public string SMSGatewayName { get; set; }

        public string GatewayURL { get; set; }
        
        public Status? Status { get; set; }
    }
}
