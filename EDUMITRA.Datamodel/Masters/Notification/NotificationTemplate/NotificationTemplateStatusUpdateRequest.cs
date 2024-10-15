
using EDUMITRA.Datamodel.Common;
using System;

namespace EDUMITRA.DataModel.Masters.Notification.NotificationTemplate
{
   public class NotificationTemplateStatusUpdateRequest
    {
        public long NotificationTemplateID { get; set; }

        public Status? Status { get; set; }
    }
}
