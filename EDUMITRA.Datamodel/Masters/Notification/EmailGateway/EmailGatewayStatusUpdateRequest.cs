using EDUMITRA.Datamodel.Common;
using System;

namespace EDUMITRA.DataModel.Masters.Notification.EmailGateway
{
    public class EmailGatewayStatusUpdateRequest
    {
        public Guid EmailGatewayUID { get; set; }

        public Status Status { get; set; }
    }
}
