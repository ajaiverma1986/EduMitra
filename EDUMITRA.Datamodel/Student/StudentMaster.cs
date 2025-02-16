using EDUMITRA.Datamodel.Common;
using EDUMITRA.Datamodel.Library;
using EDUMITRA.Datamodel.Shared;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using System.Text;

namespace EDUMITRA.Datamodel.Student
{
    public class StudentRegistrationRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo {  get; set; }
        public DateTime DOB { get; set; }
        public int ClassId { get; set; }
        public string CreatedBy { get; set; }
    }
    public class StudentListRequest:ListRequest
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public long? RegistrationID { get; set; }
        public string RegistrationNo { get; set; }
        public string MobileNo { get; set; }
        public long? UserMasterId { get; set; }
    }
    public class StudentListResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long RegistrationID { get; set; }
        public string RegistrationNo { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public DateTime? DOB { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public Status Status { get; set; }
        public string Statusname { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public void FromReader(SqlDataReader reader)
        {
            RegistrationID = DataReaderHelper.Instance.GetDataReaderValue_Long(reader, "RegistrationID");
            ClassId = DataReaderHelper.Instance.GetDataReaderValue_Int(reader, "ClassId");
            FirstName = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "FirstName");
            Status = (Status)DataReaderHelper.Instance.GetDataReaderValue_Short(reader, "Status");
            ClassName = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "ClassName");
            Statusname = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "Statusname");
            CreatedBy = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "CreatedBy");
            UpdatedBy = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "UpdatedBy");
            UpdatedOn = DataReaderHelper.Instance.GetDataReaderValue_DateTime(reader, "UpdatedOn");
            CreatedOn = DataReaderHelper.Instance.GetDataReaderValue_DateTime(reader, "CreatedOn");
            DOB = DataReaderHelper.Instance.GetDataReaderValue_DateTime(reader, "DOB");
            EmailId = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "EmailId");
            MobileNo = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "MobileNo");
            RegistrationNo = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "RegistrationNo");
        }
    }
}
