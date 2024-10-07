namespace FIA.Repository
{
    using EDUMITRA.Database;
    using EDUMITRA.Datamodel.Interfaces;
    using EDUMITRA.Datamodel.Shared;
    using EDUMITRA.DataModel.Masters.Notification.SMSGateway;
    using EDUMITRA.Repository.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SMSGatewayRepository : BaseRepository
    {
        private readonly IEDUMITRADatabase _database = null;

        public SMSGatewayRepository()
        {
            _database = new EDUMITRADatabase();
        }

        public async Task<SimpleResponse> SMSGatewaySearch(string request, IEDUMITRAServiceUser FIAAPIUser)
        {
            SimpleResponse response = new SimpleResponse();
            List<TypeViewModel> objUser = new List<TypeViewModel>();
            TypeViewModel row;
            var dbCommand = _database.GetStoredProcCommand("[NOTI].[SMSGateway_AutoSearch]");
            _database.AddInParameter(dbCommand, "@LinkedSMSGatewayID", request);

            using (var dataReader = await _database.ExecuteReaderAsync(dbCommand))
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

        public async Task<ListResponse> SMSGateway_Search(SMSGatewayRequest request, IEDUMITRAServiceUser FIAAPIUser)
        {
            ListResponse response = new ListResponse();
            List<SMSGatewayResponse> lst = new List<SMSGatewayResponse>();
            var dbCommand = _database.GetStoredProcCommand("[NOTI].[SMSGateway_Search]");
            _database.AutoGenerateInputParams(dbCommand, request, FIAAPIUser, true);

            using (var dataReader = await _database.ExecuteReaderAsync(dbCommand))
            {
                while (dataReader.Read())
                {
                    SMSGatewayResponse row = new SMSGatewayResponse();
                    row.FromReader(dataReader);
                    lst.Add(row);
                }
            }

            response.SetPagingOutput(dbCommand);
            response.CurrentPage = request.PageNo;
            response.Result = lst;
            return response;
        }

        public async Task<long> SMSGateway_AddUpdate(SMSGatewayResponse request, IEDUMITRAServiceUser FIAAPIUser)
        {
            var dbCommand = _database.GetStoredProcCommand("[NOTI].[SMSGateway_AddEdit]");
            _database.AutoGenerateInputParams(dbCommand, request, FIAAPIUser,false,true);
            await _database.ExecuteNonQueryAsync(dbCommand);

            return GetIDOutputLong(dbCommand);
        }

        public async Task<int> SMSGateway_UpdateStatus(SMSGatewayStatusUpdateRequest request, IEDUMITRAServiceUser FIAAPIUser)
        {
            var dbCommand = _database.GetStoredProcCommand("[NOTI].[SMSGateway_UpdateStatus]");
            _database.AutoGenerateInputParams(dbCommand, request, FIAAPIUser);
            return await _database.ExecuteNonQueryAsync(dbCommand);
        }
    }
}