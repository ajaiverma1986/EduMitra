using EDUMITRA.Database;
using EDUMITRA.Datamodel.Interfaces;
using EDUMITRA.Datamodel.Shared;
using EDUMITRA.Repository.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EDUMITRA.Repository
{
    public class UserMasterRepository:BaseRepository
    {
        public readonly IEDUMITRADatabase database = null;
        public UserMasterRepository() { 
            database = new EDUMITRADatabase();
        }
        public async Task<SimpleResponse> SMSGatewaySearch(string request, IEDUMITRAServiceUser FIAAPIUser)
        {
            SimpleResponse response = new SimpleResponse();
            List<TypeViewModel> objUser = new List<TypeViewModel>();
            TypeViewModel row;
            var dbCommand = database.GetStoredProcCommand("[NOTI].[SMSGateway_AutoSearch]");
            database.AddInParameter(dbCommand, "@LinkedSMSGatewayID", request);

            using (var dataReader = await database.ExecuteReaderAsync(dbCommand))
            {
                while (dataReader.Read())
                {
                    row = new TypeViewModel
                    {
                        Value = GetInt32Value(dataReader, "SMSGatewayDetailID").Value.ToString(),
                        Name = GetStringValue(dataReader, "SMSGatewayName")
                    };
                    objUser.Add(row);
                }
            }
            response.Result = objUser;
            return response;
        }
    }
}
