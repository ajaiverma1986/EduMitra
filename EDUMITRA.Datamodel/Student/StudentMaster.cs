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

    public class ExamRequest
    {
        public int ClassID { get; set; }
        public string ExamTitle { get; set; }
        public string ExamDescription { get; set; }
    }
    public class StudentRegistrationRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public DateTime? DOB { get; set; }
        public int ClassId { get; set; }
    }
    public class StudentListRequest : ListRequest
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public long? RegistrationID { get; set; }
        public string RegistrationNo { get; set; }
        public string MobileNo { get; set; }
    }
    public class ExamListRequest : ListRequest
    {
        public long? ClassID { get; set; }
        public string ExamTitle { get; set; }
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

    public class ExamListResponse
    {
        public long ExamID { get; set; }
        public string ExamCode { get; set; }
        public string ExamTitle { get; set; }
        public string ExamDescription { get; set; }
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public int Status { get; set; }
        public string Statusname { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public void FromReader(SqlDataReader reader)
        {
            ExamID = DataReaderHelper.Instance.GetDataReaderValue_Long(reader, "ExamID");
            ClassID = DataReaderHelper.Instance.GetDataReaderValue_Int(reader, "ClassID");
            ExamCode = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "ExamCode");
            Status = DataReaderHelper.Instance.GetDataReaderValue_Int(reader, "Status");
            ClassName = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "ClassName");
            Statusname = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "Statusname");
            CreatedBy = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "CreatedBy");
            UpdatedBy = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "UpdatedBy");
            UpdatedOn = DataReaderHelper.Instance.GetDataReaderValue_DateTime(reader, "UpdatedOn");
            CreatedOn = DataReaderHelper.Instance.GetDataReaderValue_DateTime(reader, "CreatedOn");
            ExamTitle = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "ExamTitle");
            ExamDescription = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "ExamDescription");
        }
    }
    public class QuestionCategoryRequest
    {
        public string QCategoryName { get; set; }
    }
    public class QuestionTypeRequest
    {
        public string QuestionTypeName { get; set; }
    }
    public class QuestionCateRequest : ListRequest
    {
        public long? CategoryID { get; set; }
        public string Categoryname { get; set; }
    }
    public class ListQuestionTypeRequest : ListRequest
    {
        public long? QuestionTypeID { get; set; }
        public string QuestionTypeName { get; set; }
    }
    public class QCategoryListResponse
    {
        public long QCategoryID { get; set; }
        public string QCategoryName { get; set; }
        public string Statusname { get; set; }

        public void FromReader(SqlDataReader reader)
        {
            QCategoryID = DataReaderHelper.Instance.GetDataReaderValue_Long(reader, "QCategoryID");
            QCategoryName = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "QCategoryName");
            Statusname = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "Statusname");
        }
    }
    public class QTypeListResponse
    {
        public long QuestionTypeID { get; set; }
        public string QuestionTypeName { get; set; }
        public string Statusname { get; set; }

        public void FromReader(SqlDataReader reader)
        {
            QuestionTypeID = DataReaderHelper.Instance.GetDataReaderValue_Long(reader, "QuestionTypeID");
            QuestionTypeName = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "QuestionTypeName");
            Statusname = DataReaderHelper.Instance.GetDataReaderValue_String(reader, "Statusname");
        }
    }
    public class NewQuestionRequest
    {
        public int QuestionCategoryId { get; set; }
        public string QuestionText { get; set; }
        public int QuestionTypeID { get; set; }
        public double QScore { get; set; }
        public double QNegativeS { get; set; }
        public string QInstruction { get; set; }
    }
}
