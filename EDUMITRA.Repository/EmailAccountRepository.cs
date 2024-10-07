namespace EDUMITRA.Repository
{
    using EDUMITRA.Database;
    using EDUMITRA.Datamodel.Interfaces;
    using EDUMITRA.Datamodel.Shared;
    using EDUMITRA.DataModel.Masters.Notification.EmailGateway;
    using EDUMITRA.Repository.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class EmailGatewayRepository : BaseRepository
    {
        private readonly IEDUMITRADatabase _database = null;

        public EmailGatewayRepository()
        {
            _database = new EDUMITRADatabase();
        }

        public async Task<ListResponse> EmailGateway_Search(EmailGatewayRequest request, IEDUMITRAServiceUser FIAAPIUser)
        {
            ListResponse response = new ListResponse();
            List<EmailGatewayResponse> lst = new List<EmailGatewayResponse>();
            var dbCommand = _database.GetStoredProcCommand("[NOTI].[EmailGateway_Search]");
            _database.AutoGenerateInputParams(dbCommand, request, FIAAPIUser, true);

            using (var dataReader = await _database.ExecuteReaderAsync(dbCommand))
            {
                while (dataReader.Read())
                {
                    EmailGatewayResponse row = new EmailGatewayResponse();
                    row.FromReader(dataReader);
                    lst.Add(row);
                }
            }

            response.SetPagingOutput(dbCommand);
            response.CurrentPage = request.PageNo;
            response.Result = lst;
            return response;
        }

        public async Task<long> EmailGateway_AddUpdate(EmailGatewayResponse request, IEDUMITRAServiceUser FIAAPIUser)
        {
            var dbCommand = _database.GetStoredProcCommand("[NOTI].[EmailGateway_AddEdit]");
            _database.AutoGenerateInputParams(dbCommand, request, FIAAPIUser,false,true);
            await _database.ExecuteNonQueryAsync(dbCommand);

            return GetIDOutputLong(dbCommand);
        }

        public async Task<int> EmailGateway_UpdateStatus(EmailGatewayStatusUpdateRequest request, IEDUMITRAServiceUser FIAAPIUser)
        {
            var dbCommand = _database.GetStoredProcCommand("[NOTI].[EmailGateway_UpdateStatus]");
            _database.AutoGenerateInputParams(dbCommand, request, FIAAPIUser);
            return await _database.ExecuteNonQueryAsync(dbCommand);
        }
    }
}