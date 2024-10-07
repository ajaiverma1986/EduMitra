
using EDUMITRA.Datamodel.Common;
using System;

namespace EDUMITRA.DataModel.Masters.Notification.SMSGateway
{
    public class SMSGatewayStatusUpdateRequest
    {
        public Guid? SMSGatewayUID { get; set; }

        public Status? Status { get; set; }
    }
}
