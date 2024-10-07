
using EDUMITRA.Datamodel.Common;
using System;

namespace EDUMITRA.DataModel.Masters.Notification.NotificationCategory
{
    public class NotificationCategoryStatusUpdateRequest
    { 
        public Guid NotificationCategoryUID { get; set; }

        public Status Status { get; set; }
    }
}
