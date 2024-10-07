
using EDUMITRA.Datamodel.Common;
using System;

namespace EDUMITRA.DataModel.Masters.Notification.NotificationTemplate
{
   public class NotificationTemplateStatusUpdateRequest
    {
        public Guid? NotificationTemplateUID { get; set; }

        public Status? Status { get; set; }
    }
}
