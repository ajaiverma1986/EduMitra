using EDUMITRA.Database;
using EDUMITRA.Datamodel.Interfaces;
using EDUMITRA.Datamodel.Masters;
using EDUMITRA.Datamodel.Shared;
using EDUMITRA.Datamodel.Student;
using EDUMITRA.Repository.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EDUMITRA.Repository
{
    public class StudentRepository:BaseRepository
    {
        public readonly IEDUMITRADatabase _database=null;
        public StudentRepository() {
            _database=new EDUMITRADatabase();
        }
        public async Task<long> AddNewStudentRegistration(StudentRegistrationRequest request, IEDUMITRAServiceUser serviceUser)
        {

            long outputstr = 0;
            SimpleResponse response = new SimpleResponse();
            var dbCommand = _database.GetStoredProcCommand("[STU].CreateStudent");
            _database.AddInParameter(dbCommand, "@FirstName", request.FirstName);
            _database.AddInParameter(dbCommand, "@LastName", request.LastName);
            _database.AddInParameter(dbCommand, "@EmailId", request.EmailId);
            _database.AddInParameter(dbCommand, "@MobileNo", request.MobileNo);
            _database.AddInParameter(dbCommand, "@DOB", request.DOB);
            _database.AddInParameter(dbCommand, "@ClassId", request.ClassId);
            _database.AddInParameter(dbCommand, "@CreatedBy", serviceUser.UserMasterID);
            _database.AddOutParameter(dbCommand, "@Out_ID", 100);

            await _database.ExecuteNonQueryAsync(dbCommand);

            outputstr = GetIDOutputLong(dbCommand);

            return outputstr;

        }
        public async Task<ListResponse> ListStudent(StudentListRequest request, IEDUMITRAServiceUser serviceUser)
        {
            ListResponse response = new ListResponse();
            List<StudentListResponse> lst = new List<StudentListResponse>();
            var dbCommand = _database.GetStoredProcCommand("[STU].usp_GetallStudent");
            _database.AutoGenerateInputParams(dbCommand, request, serviceUser, true);

            using (var dataReader = await _database.ExecuteReaderAsync(dbCommand))
            {
                while (dataReader.Read())
                {
                    StudentListResponse row = new StudentListResponse();
                    row.FromReader(dataReader);
                    lst.Add(row);
                }
            }

            response.SetPagingOutput(dbCommand);
            response.CurrentPage = request.PageNo;
            response.Result = lst;

            return response;
        }

        public async Task<long> AddNewExam(ExamRequest request, IEDUMITRAServiceUser serviceUser)
        {
            long outputstr = 0;
            SimpleResponse response = new SimpleResponse();
            var dbCommand = _database.GetStoredProcCommand("[EXM].CreateExamMaster");
            _database.AddInParameter(dbCommand, "@ExamTitle", request.ExamTitle);
            _database.AddInParameter(dbCommand, "@ExamDescription", request.ExamDescription);
            _database.AddInParameter(dbCommand, "@ClassID", request.ClassID);
            _database.AddInParameter(dbCommand, "@CreatedBy", serviceUser.UserMasterID);
            _database.AddOutParameter(dbCommand, "@Out_ID", 100);

            await _database.ExecuteNonQueryAsync(dbCommand);
            outputstr = GetIDOutputLong(dbCommand);
            return outputstr;
        }
        public async Task<ListResponse> ListExam(ExamListRequest request, IEDUMITRAServiceUser serviceUser)
        {
            ListResponse response = new ListResponse();
            List<ExamListResponse> lst = new List<ExamListResponse>();
            var dbCommand = _database.GetStoredProcCommand("[EXM].ListExamMaster");
            _database.AutoGenerateInputParams(dbCommand, request, serviceUser, true);

            using (var dataReader = await _database.ExecuteReaderAsync(dbCommand))
            {
                while (dataReader.Read())
                {
                    ExamListResponse row = new ExamListResponse();
                    row.FromReader(dataReader);
                    lst.Add(row);
                }
            }

            response.SetPagingOutput(dbCommand);
            response.CurrentPage = request.PageNo;
            response.Result = lst;

            return response;
        }
        public async Task<long> AddNewQCategory(QuestionCategoryRequest request, IEDUMITRAServiceUser serviceUser)
        {
            long outputstr = 0;
            SimpleResponse response = new SimpleResponse();
            var dbCommand = _database.GetStoredProcCommand("[EXM].CreateQCategory");
            _database.AddInParameter(dbCommand, "@QCategoryName", request.QCategoryName);
            _database.AddInParameter(dbCommand, "@CreatedBy", serviceUser.UserMasterID);
            _database.AddOutParameter(dbCommand, "@Out_ID", 100);

            await _database.ExecuteNonQueryAsync(dbCommand);
            outputstr = GetIDOutputLong(dbCommand);
            return outputstr;
        }
        public async Task<ListResponse> ListQCategory(QuestionCateRequest request, IEDUMITRAServiceUser serviceUser)
        {
            ListResponse response = new ListResponse();
            List<QCategoryListResponse> lst = new List<QCategoryListResponse>();
            var dbCommand = _database.GetStoredProcCommand("[EXM].ListQuestionCategoryMaster");
            _database.AutoGenerateInputParams(dbCommand, request, serviceUser, true);

            using (var dataReader = await _database.ExecuteReaderAsync(dbCommand))
            {
                while (dataReader.Read())
                {
                    QCategoryListResponse row = new QCategoryListResponse();
                    row.FromReader(dataReader);
                    lst.Add(row);
                }
            }

            response.SetPagingOutput(dbCommand);
            response.CurrentPage = request.PageNo;
            response.Result = lst;

            return response;
        }

        public async Task<long> AddNewQType(QuestionTypeRequest request, IEDUMITRAServiceUser serviceUser)
        {
            long outputstr = 0;
            SimpleResponse response = new SimpleResponse();
            var dbCommand = _database.GetStoredProcCommand("[EXM].CreateQType");
            _database.AddInParameter(dbCommand, "@QuestionTypeName", request.QuestionTypeName);
            _database.AddInParameter(dbCommand, "@CreatedBy", serviceUser.UserMasterID);
            _database.AddOutParameter(dbCommand, "@Out_ID", 100);

            await _database.ExecuteNonQueryAsync(dbCommand);
            outputstr = GetIDOutputLong(dbCommand);
            return outputstr;
        }
        public async Task<ListResponse> ListQType(ListQuestionTypeRequest request, IEDUMITRAServiceUser serviceUser)
        {
            ListResponse response = new ListResponse();
            List<QTypeListResponse> lst = new List<QTypeListResponse>();
            var dbCommand = _database.GetStoredProcCommand("[EXM].QuestionTypeMaster");
            _database.AutoGenerateInputParams(dbCommand, request, serviceUser, true);

            using (var dataReader = await _database.ExecuteReaderAsync(dbCommand))
            {
                while (dataReader.Read())
                {
                    QTypeListResponse row = new QTypeListResponse();
                    row.FromReader(dataReader);
                    lst.Add(row);
                }
            }

            response.SetPagingOutput(dbCommand);
            response.CurrentPage = request.PageNo;
            response.Result = lst;

            return response;
        }
        public async Task<long> AddNewQuestions(NewQuestionRequest request, IEDUMITRAServiceUser serviceUser)
        {
            long outputstr = 0;
            SimpleResponse response = new SimpleResponse();
            var dbCommand = _database.GetStoredProcCommand("[EXM].CreateQuestions");
            _database.AddInParameter(dbCommand, "@QuestionCategoryId", request.QuestionCategoryId);
            _database.AddInParameter(dbCommand, "@QInstruction", request.QInstruction);
            _database.AddInParameter(dbCommand, "@QuestionText", request.QuestionText);
            _database.AddInParameter(dbCommand, "@QNegativeS", request.QNegativeS);
            _database.AddInParameter(dbCommand, "@QScore", request.QScore);
            _database.AddInParameter(dbCommand, "@QuestionTypeID", request.QuestionTypeID);
            _database.AddInParameter(dbCommand, "@CreatedBy", serviceUser.UserMasterID);
            _database.AddOutParameter(dbCommand, "@Out_ID", 100);

            await _database.ExecuteNonQueryAsync(dbCommand);
            outputstr = GetIDOutputLong(dbCommand);
            return outputstr;
        }
    }
}
