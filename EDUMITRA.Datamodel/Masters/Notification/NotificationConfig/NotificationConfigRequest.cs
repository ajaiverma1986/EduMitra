using EDUMITRA.Datamodel.Common;
using EDUMITRA.Datamodel.Shared;
using System;

namespace EDUMITRA.DataModel.Masters.Notification.NotificationConfig
{
    public class NotificationConfigRequest : ListRequest
    {
        public long? NotificationConfigID { get; set; }

        public int? OrganizationID { get; set; }

        public byte? SMSGatewayID { get; set; }

        public byte? EmailGatewayID { get; set; }

        public Status? Status { get; set; }
    }
}
