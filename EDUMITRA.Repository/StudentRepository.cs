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
    }
}
