namespace EDUMITRA.Repository
{

    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EDUMITRA.Database;
    using EDUMITRA.Datamodel.Interfaces;
    using EDUMITRA.Datamodel.Shared;
    using EDUMITRA.DataModel.Masters.Notification.NotificationCategory;
    using EDUMITRA.Repository.Shared;

    public class NotificationCategoryRepository : BaseRepository
    {
        private readonly IEDUMITRADatabase _database = null;

        public NotificationCategoryRepository()
        {
            _database = new EDUMITRADatabase();
        }

        public async Task<SimpleResponse> NotificationCategorySearch(string request, IEDUMITRAServiceUser FIAAPIUser)
        {
            SimpleResponse response = new SimpleResponse();
            List<TypeViewModel> objUser = new List<TypeViewModel>();
            TypeViewModel row;
            var dbCommand = _database.GetStoredProcCommand("[NOTI].[NotificationCategory_AutoSearch]");
            _database.AddInParameter(dbCommand, "@LinkedNotificationCategoryID", request);

            using (var dataReader = await _database.ExecuteReaderAsync(dbCommand))
            {
                while (dataReader.Read())
                {
                    row = new TypeViewModel
                    {
                        Value = GetInt32Value(dataReader, "NotificationCategoryDetailID").Value.ToString(),
                        Name = GetStringValue(dataReader, "NotificationCategoryName")
                    };
                    objUser.Add(row);
                }
            }
            response.Result = objUser;
            return response;
        }

        public async Task<ListResponse> NotificationCategory_Search(NotificationCategoryRequest request, IEDUMITRAServiceUser FIAAPIUser)
        {
            ListResponse response = new ListResponse();
            List<NotificationCategoryResponse> lst = new List<NotificationCategoryResponse>();
            var dbCommand = _database.GetStoredProcCommand("[NOTI].[NotificationCategory_Search]");
            _database.AutoGenerateInputParams(dbCommand, request, FIAAPIUser, true);

            using (var dataReader = await _database.ExecuteReaderAsync(dbCommand))
            {
                while (dataReader.Read())
                {
                    NotificationCategoryResponse row = new NotificationCategoryResponse();
                    row.FromReader(dataReader);
                    lst.Add(row);
                }
            }

            response.SetPagingOutput(dbCommand);
            response.CurrentPage = request.PageNo;
            response.Result = lst;
            return response;
        }

        public async Task<long> NotificationCategory_AddUpdate(NotificationCategoryResponse request, IEDUMITRAServiceUser FIAAPIUser)
        {
            var dbCommand = _database.GetStoredProcCommand("[NOTI].[NotificationCategory_AddEdit]");
            _database.AutoGenerateInputParams(dbCommand, request, FIAAPIUser,false,true);
            await _database.ExecuteNonQueryAsync(dbCommand);

            return GetIDOutputLong(dbCommand);
        }

        public async Task<int> NotificationCategory_UpdateStatus(NotificationCategoryStatusUpdateRequest request, IEDUMITRAServiceUser FIAAPIUser)
        {
            var dbCommand = _database.GetStoredProcCommand("[NOTI].[NotificationCategory_UpdateStatus]");
            _database.AutoGenerateInputParams(dbCommand, request, FIAAPIUser);
            return await _database.ExecuteNonQueryAsync(dbCommand);
        }
    }
}