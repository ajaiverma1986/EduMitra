using EDUMITRA.Datamodel.Shared;
using EDUMITRA.Datamodel.Common;
using System;

namespace EDUMITRA.DataModel.Masters.Notification.EmailGateway
{
    public class EmailGatewayRequest:ListRequest
    {
        public byte? EmailGatewayID { get; set; }

        public Guid? EmailGatewayUID { get; set; }
        
        public Status Status { get; set; }
    }
}
