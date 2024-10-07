
using EDUMITRA.Datamodel.Common;
using EDUMITRA.Datamodel.Library;
using EDUMITRA.Datamodel.Shared;
using System;
using System.Data.SqlClient;

namespace EDUMITRA.DataModel.Masters.Notification.NotificationCategory
{
   public class NotificationCategoryResponse
    {
        public byte? NotificationCategoryID { get; set; }

        public Guid? NotificationCategoryUID { get; set; }

        public string NotificationCategory { get; set; }

        public string NotificationCategoryDescription { get; set; }

        [SQLParam(Usage = SQLParamPlaces.None)]
        public Status? Status { get; set; }

        [SQLParam(Usage = SQLParamPlaces.None)]
        public string StatusName
        {
            get
            {
                return Status.GetDisplayName();
            }
        }

        public void FromReader(SqlDataReader reader)
        {
            NotificationCategoryID = DataReaderHelper.Instance.GetDataReaderValue_Byte(reader, "NotificationCategoryID");

            NotificationCategoryUID = DataReaderHelper.Instance.GetDataReaderValue_Guid(reader, "NotificationCategoryUID");

            NotificationCategory = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "NotificationCategory");

            NotificationCategoryDescription = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "NotificationCategoryDescription");

            Status = (Status)DataReaderHelper.Instance.GetDataReaderValue_Byte(reader, "Status");

        }
    }
}
