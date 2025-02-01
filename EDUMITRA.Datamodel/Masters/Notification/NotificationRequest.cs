namespace EDUMITRA.DataModel.Masters.Notification
{
    using System.Collections.Generic;
    using System.Net.Mail;


    public class NotificationRequest
    {
        public long? UserMasterID { get; set; }

        public string TO { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }

        public string MobileNumber { get; set; }
    }
}