namespace EDUMITRA.DataModel.Masters.Notification.NotificationLog
{
    using EDUMITRA.Datamodel.Common;
    using System;

    public class NotificationLogStatusUpdateRequest
    {
        public Guid NotificationLogUID { get; set; }

        public string LastMessageSendResponse { get; set; }

        public NotificationLogStatus Status { get; set; }
    }
}