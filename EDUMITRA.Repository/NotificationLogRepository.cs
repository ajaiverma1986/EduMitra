namespace EDUMITRA.Repository
{
    using EDUMITRA.Database;
    using EDUMITRA.Datamodel.Interfaces;
    using EDUMITRA.Datamodel.Shared;
    using EDUMITRA.DataModel.Masters.Notification.NotificationLog;
    using EDUMITRA.Repository.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class NotificationLogRepository : BaseRepository
    {
        private readonly IEDUMITRADatabase _database = null;

        public NotificationLogRepository()
        {
            _database = new EDUMITRADatabase();
        }

       
        public async Task<ListResponse> NotificationLog_Search(NotificationLogRequest request, IEDUMITRAServiceUser FIAAPIUser)
        {
            ListResponse response = new ListResponse();
            List<NotificationLogResponse> lst = new List<NotificationLogResponse>();
            var dbCommand = _database.GetStoredProcCommand("[NOTI].[NotificationLog_Search]");
            _database.AutoGenerateInputParams(dbCommand, request, FIAAPIUser, true);

            using (var dataReader = await _database.ExecuteReaderAsync(dbCommand))
            {
                while (dataReader.Read())
                {
                    NotificationLogResponse row = new NotificationLogResponse();
                    row.FromReader(dataReader);
                    lst.Add(row);
                }
            }

            response.SetPagingOutput(dbCommand);
            response.CurrentPage = request.PageNo;
            response.Result = lst;
            return response;
        }

        public async Task<long> NotificationLog_AddUpdate(NotificationLogResponse request, IEDUMITRAServiceUser FIAAPIUser)
        {
            var dbCommand = _database.GetStoredProcCommand("[NOTI].[NotificationLog_AddEdit]");
            _database.AutoGenerateInputParams(dbCommand, request, FIAAPIUser, false, true, true, true);
            await _database.ExecuteNonQueryAsync(dbCommand);

            return GetIDOutputLong(dbCommand);
        }

        public async Task<int> NotificationLog_UpdateStatus(NotificationLogStatusUpdateRequest request, IEDUMITRAServiceUser FIAAPIUser)
        {
            var dbCommand = _database.GetStoredProcCommand("[NOTI].[NotificationLog_UpdateStatus]");
            _database.AutoGenerateInputParams(dbCommand, request, FIAAPIUser);
            return await _database.ExecuteNonQueryAsync(dbCommand);
        }
    }
}