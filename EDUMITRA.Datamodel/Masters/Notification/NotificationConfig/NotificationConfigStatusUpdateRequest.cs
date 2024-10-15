
using EDUMITRA.Datamodel.Common;
using System;

namespace EDUMITRA.DataModel.Masters.Notification.NotificationConfig
{
    public class NotificationConfigStatusUpdateRequest
    {
        public int  NotificationConfigID { get; set; }

        public Status? Status { get; set; }
    }
}
