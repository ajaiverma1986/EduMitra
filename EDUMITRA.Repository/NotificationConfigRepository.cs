namespace EDUMITRA.Repository
{
    using EDUMITRA.Database;
    using EDUMITRA.Datamodel.Interfaces;
    using EDUMITRA.Datamodel.Shared;
    using EDUMITRA.DataModel.Masters.Notification.NotificationConfig;
    using EDUMITRA.Repository.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class NotificationConfigRepository : BaseRepository
    {
        private readonly IEDUMITRADatabase _database = null;

        public NotificationConfigRepository()
        {
            _database = new EDUMITRADatabase();
        }

        public async Task<SimpleResponse> NotificationConfigSearch(string request, IEDUMITRAServiceUser FIAAPIUser)
        {
            SimpleResponse response = new SimpleResponse();
            List<TypeViewModel> objUser = new List<TypeViewModel>();
            TypeViewModel row;
            var dbCommand = _database.GetStoredProcCommand("[NOTI].[NotificationConfig_AutoSearch]");
            _database.AddInParameter(dbCommand, "@LinkedNotificationConfigID", request);

            using (var dataReader = await _database.ExecuteReaderAsync(dbCommand))
            {
                while (dataReader.Read())
                {
                    row = new TypeViewModel
                    {
                        Value = GetInt32Value(dataReader, "NotificationConfigDetailID").Value.ToString(),
                        Name = GetStringValue(dataReader, "NotificationConfigName")
                    };
                    objUser.Add(row);
                }
            }
            response.Result = objUser;
            return response;
        }

        public async Task<ListResponse> NotificationConfig_Search(NotificationConfigRequest request, IEDUMITRAServiceUser FIAAPIUser)
        {
            ListResponse response = new ListResponse();
            List<NotificationConfigResponse> lst = new List<NotificationConfigResponse>();
            var dbCommand = _database.GetStoredProcCommand("[NOTI].[NotificationConfig_Search]");
            _database.AutoGenerateInputParams(dbCommand, request, FIAAPIUser, true);

            using (var dataReader = await _database.ExecuteReaderAsync(dbCommand))
            {
                while (dataReader.Read())
                {
                    NotificationConfigResponse row = new NotificationConfigResponse();
                    row.FromReader(dataReader);
                    lst.Add(row);
                }
            }

            response.SetPagingOutput(dbCommand);
            response.CurrentPage = request.PageNo;
            response.Result = lst;
            return response;
        }

        public async Task<long> NotificationConfig_AddUpdate(NotificationConfigResponse request, IEDUMITRAServiceUser FIAAPIUser)
        {
            var dbCommand = _database.GetStoredProcCommand("[NOTI].[NotificationConfig_AddEdit]");
            _database.AutoGenerateInputParams(dbCommand, request, FIAAPIUser,false,true);
            await _database.ExecuteNonQueryAsync(dbCommand);

            return GetIDOutputLong(dbCommand);
        }

        public async Task<int> NotificationConfig_UpdateStatus(NotificationConfigStatusUpdateRequest request, IEDUMITRAServiceUser FIAAPIUser)
        {
            var dbCommand = _database.GetStoredProcCommand("[NOTI].[NotificationConfig_UpdateStatus]");
            _database.AutoGenerateInputParams(dbCommand, request, FIAAPIUser);
            return await _database.ExecuteNonQueryAsync(dbCommand);
        }
    }
}