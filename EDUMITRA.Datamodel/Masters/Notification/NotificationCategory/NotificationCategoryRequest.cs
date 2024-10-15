using EDUMITRA.Datamodel.Common;
using EDUMITRA.Datamodel.Shared;
using System;

namespace EDUMITRA.DataModel.Masters.Notification.NotificationCategory
{
    public class NotificationCategoryRequest:ListRequest
    {
        public long NotificationCategoryID { get; set; }
        public string NotificationCategory { get; set; }

        public Status Status { get; set; }
    }
}
