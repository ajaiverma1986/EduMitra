using EDUMITRA.Database;
using EDUMITRA.Datamodel.Masters;
using EDUMITRA.Datamodel.Shared;
using EDUMITRA.Repository.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EDUMITRA.Repository
{
    public class UtilityRepository : BaseRepository
    {
        private readonly IEDUMITRADatabase database = null;
        public UtilityRepository()
        {
            database = new EDUMITRADatabase();
        }
        public async Task<OTPResponse> SendOTP(string mobileNumber, string OTP)
        {
            OTPResponse row = new OTPResponse();
            var dbCommand = database.GetStoredProcCommand("usp_GenerateOTP");
            database.AddInParameter(dbCommand, "@MobileNo", mobileNumber);
            database.AddInParameter(dbCommand, "@OTP", OTP);
            using (var dataReader = await database.ExecuteReaderAsync(dbCommand))
            {
                if (dataReader.Read())
                {
                   
                    row.status = "Success";
                    row.response_message = "Otp Send Successfully";
                    row.OTPID = GetStringValue(dataReader, "OTPID");
                }
            }
           
            return row;
        }
        public async Task<SimpleResponse> ValidateOTP(string mobileNumber, string OTP)
        {
            SimpleResponse response = new SimpleResponse();
            int abc = 0;
            var dbCommand = database.GetStoredProcCommand("usp_ValidateOTP");
            database.AddInParameter(dbCommand, "@MobileNo", mobileNumber);
            database.AddInParameter(dbCommand, "@OTP", OTP);
            using (var dataReader = await database.ExecuteReaderAsync(dbCommand))
            {
                while (dataReader.Read())
                {

                    abc = GetInt32Value(dataReader, "Result").Value;
                }
            }
            response.Result = abc;
            return response;
        }
       

    }
}
