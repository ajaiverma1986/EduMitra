
using EDUMITRA.Datamodel.Common;
using EDUMITRA.Datamodel.Shared;
using System;

namespace EDUMITRA.DataModel.Masters.Notification.NotificationTemplate
{
    public class NotificationTemplateRequest:ListRequest
    {
        public long? NotificationTemplateID { get; set; }

        public byte NotificationCategoryID { get; set; }

        public byte NotificationConfigID { get; set; }

        public int OrganizationID { get; set; }

        public string NotificationTemplate { get; set; }

        public Status? Status { get; set; }
    }
}
