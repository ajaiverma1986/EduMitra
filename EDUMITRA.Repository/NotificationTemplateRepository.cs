namespace EDUMITRA.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EDUMITRA.Database;
    using EDUMITRA.Datamodel.Interfaces;
    using EDUMITRA.Datamodel.Shared;
    using EDUMITRA.DataModel.Masters.Notification.NotificationTemplate;
    using EDUMITRA.Repository.Shared;

    public class NotificationTemplateRepository : BaseRepository
    {
        private readonly IEDUMITRADatabase _database = null;

        public NotificationTemplateRepository()
        {
            _database = new EDUMITRADatabase();
        }

        public async Task<SimpleResponse> NotificationTemplateSearch(string request, IEDUMITRAServiceUser FIAAPIUser)
        {
            SimpleResponse response = new SimpleResponse();
            List<TypeViewModel> objUser = new List<TypeViewModel>();
            TypeViewModel row;
            var dbCommand = _database.GetStoredProcCommand("[NOTI].[NotificationTemplate_AutoSearch]");
            _database.AddInParameter(dbCommand, "@LinkedNotificationTemplateID", request);

            using (var dataReader = await _database.ExecuteReaderAsync(dbCommand))
            {
                while (dataReader.Read())
                {
                    row = new TypeViewModel
                    {
                        Value = GetInt32Value(dataReader, "NotificationTemplateDetailID").Value.ToString(),
                        Name = GetStringValue(dataReader, "NotificationTemplateName")
                    };
                    objUser.Add(row);
                }
            }
            response.Result = objUser;
            return response;
        }

        public async Task<ListResponse> NotificationTemplate_Search(NotificationTemplateRequest request, IEDUMITRAServiceUser FIAAPIUser)
        {
            ListResponse response = new ListResponse();
            List<NotificationTemplateResponse> lst = new List<NotificationTemplateResponse>();
            var dbCommand = _database.GetStoredProcCommand("[NOTI].[NotificationTemplate_Search]");
            _database.AutoGenerateInputParams(dbCommand, request, FIAAPIUser, true);

            using (var dataReader = await _database.ExecuteReaderAsync(dbCommand))
            {
                while (dataReader.Read())
                {
                    NotificationTemplateResponse row = new NotificationTemplateResponse();
                    row.FromReader(dataReader);
                    lst.Add(row);
                }
            }

            response.SetPagingOutput(dbCommand);
            response.CurrentPage = request.PageNo;
            response.Result = lst;
            return response;
        }

        public async Task<long> NotificationTemplate_AddUpdate(NotificationTemplateResponse request, IEDUMITRAServiceUser FIAAPIUser)
        {
            var dbCommand = _database.GetStoredProcCommand("[NOTI].[NotificationTemplate_AddEdit]");
            _database.AutoGenerateInputParams(dbCommand, request, FIAAPIUser,false,true);
            await _database.ExecuteNonQueryAsync(dbCommand);

            return GetIDOutputLong(dbCommand);
        }

        public async Task<int> NotificationTemplate_UpdateStatus(NotificationTemplateStatusUpdateRequest request, IEDUMITRAServiceUser FIAAPIUser)
        {
            var dbCommand = _database.GetStoredProcCommand("[NOTI].[NotificationTemplate_UpdateStatus]");
            _database.AutoGenerateInputParams(dbCommand, request, FIAAPIUser);
            return await _database.ExecuteNonQueryAsync(dbCommand);
        }
    }
}