
using EDUMITRA.Datamodel.Common;
using EDUMITRA.Datamodel.Shared;
using System;

namespace EDUMITRA.DataModel.Masters.Notification.NotificationLog
{
    public class NotificationLogRequest : ListRequest
    {
        public long? NotificationLogID { get; set; }

        public Guid? NotificationLogUID { get; set; }

        public int? NotificationTemplateID { get; set; }

        public int? ApplicationID { get; set; }

        public NotificationMediumType? NotificationMediumTypeID { get; set; }

        public long? UserMasterID { get; set; }

        public NotificationLogStatus? Status { get; set; }
    }
}
