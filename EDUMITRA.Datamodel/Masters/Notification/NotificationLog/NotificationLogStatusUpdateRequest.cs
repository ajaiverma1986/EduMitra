namespace EDUMITRA.DataModel.Masters.Notification.NotificationLog
{
    using EDUMITRA.Datamodel.Common;
    using System;

    public class NotificationLogStatusUpdateRequest
    {
        public long? NotificationLogID { get; set; }

        public string LastMessageSendResponse { get; set; }

        public NotificationLogStatus Status { get; set; }
    }
}