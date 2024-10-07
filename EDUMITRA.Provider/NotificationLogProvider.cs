namespace EDUMITRA.Provider
{
    using EDUMITRA.Datamodel.Interfaces;
    using EDUMITRA.Datamodel.Shared;
    using EDUMITRA.DataModel.Masters.Notification.NotificationLog;
    using EDUMITRA.Repository;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class NotificationLogProvider
    {
        private readonly NotificationLogRepository _repository = null;
        

        public NotificationLogProvider()
        {
            _repository = new NotificationLogRepository();
           
        }

        public async Task<ListResponse> NotificationLog_Search(NotificationLogRequest request, IEDUMITRAServiceUser serviceUser)
        {
            return await _repository.NotificationLog_Search(request, serviceUser);
        }

        public async Task<NotificationLogResponse> NotificationLog_GetByID(long NotificationLogID, IEDUMITRAServiceUser serviceUser)
        {
            NotificationLogRequest request = new NotificationLogRequest();
            request.NotificationLogID = NotificationLogID;
            request.SetDefaults();

            ListResponse response = await NotificationLog_Search(request, serviceUser);

            NotificationLogResponse NotificationLog = new NotificationLogResponse();
            if (response == null || response.Result == null)
            {
                return NotificationLog;
            }

            List<NotificationLogResponse> list = (List<NotificationLogResponse>)response.Result;
            if (list.Count == 0)
            {
                return NotificationLog;
            }
            NotificationLog = list[0];

            return NotificationLog;
        }

        public async Task<NotificationLogResponse> NotificationLog_GetByUID(Guid NotificationLogUID, IEDUMITRAServiceUser serviceUser)
        {
            NotificationLogRequest request = new NotificationLogRequest();
            request.NotificationLogUID = NotificationLogUID;
            request.SetDefaults();

            ListResponse response = await NotificationLog_Search(request, serviceUser);

            NotificationLogResponse NotificationLog = new NotificationLogResponse();
            if (response == null || response.Result == null)
            {
                return NotificationLog;
            }

            List<NotificationLogResponse> list = (List<NotificationLogResponse>)response.Result;
            if (list.Count == 0)
            {
                return NotificationLog;
            }
            NotificationLog = list[0];

            return NotificationLog;
        }

        public async Task<SimpleResponse> NotificationLog_AddUpdate(NotificationLogResponse request, IEDUMITRAServiceUser FIAAPIUser)
        {
            SimpleResponse response = new SimpleResponse();
            long NotificationLogID = await _repository.NotificationLog_AddUpdate(request, FIAAPIUser);

            if (NotificationLogID > 0)
                response.Result = NotificationLogID;
            else
                response.SetError(ErrorCodes.SERVER_ERROR);

            return response;
        }

        public async Task<int> NotificationLog_UpdateStatus(NotificationLogStatusUpdateRequest request, IEDUMITRAServiceUser FIAAPIUser)
        {
            return await _repository.NotificationLog_UpdateStatus(request, FIAAPIUser);
        }
    }
}