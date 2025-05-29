using EDUMITRA.Datamodel.Interfaces;
using EDUMITRA.Datamodel.Masters;
using EDUMITRA.Datamodel.Shared;
using EDUMITRA.Datamodel.Student;
using EDUMITRA.Provider.Shared;
using EDUMITRA.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EDUMITRA.Provider
{
    public class StudentProvider:BaseProvider
    {
        public readonly StudentRepository _repository=null;
        public StudentProvider() {
            _repository = new StudentRepository();
        }
        public async Task<SimpleResponse> AddNewStudentRegistration(StudentRegistrationRequest request, IEDUMITRAServiceUser serviceUser)
        {
            SimpleResponse response = new SimpleResponse();
            response.Result = await _repository.AddNewStudentRegistration(request, serviceUser);
            return response;
        }
        public async Task<ListResponse> ListStudent(StudentListRequest request, IEDUMITRAServiceUser serviceUser)
        {
            ListResponse response = new ListResponse();
            response = await _repository.ListStudent(request, serviceUser);
            return response;
        }
        public async Task<SimpleResponse> AddNewExam(ExamRequest request, IEDUMITRAServiceUser serviceUser)
        {
            SimpleResponse response = new SimpleResponse();
            response.Result = await _repository.AddNewExam(request, serviceUser);
            return response;
        }
        public async Task<ListResponse> ListExam(ExamListRequest request, IEDUMITRAServiceUser serviceUser)
        {
            ListResponse response = new ListResponse();
            response = await _repository.ListExam(request, serviceUser);
            return response;
        }
    }
}
