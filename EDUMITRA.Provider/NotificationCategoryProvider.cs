namespace EDUMITRA.Provider
{
    using EDUMITRA.Datamodel.Interfaces;
    using EDUMITRA.Datamodel.Shared;
    using EDUMITRA.DataModel.Masters.Notification.NotificationCategory;
    using EDUMITRA.Repository;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class NotificationCategoryProvider
    {
        private readonly NotificationCategoryRepository _repository = null;

        public NotificationCategoryProvider()
        {
            _repository = new NotificationCategoryRepository();
        }

        public async Task<ListResponse> NotificationCategory_Search(NotificationCategoryRequest request, IEDUMITRAServiceUser serviceUser)
        {
            return await _repository.NotificationCategory_Search(request, serviceUser);
        }

        public async Task<NotificationCategoryResponse> NotificationCategory_GetByID(byte NotificationCategoryID, IEDUMITRAServiceUser serviceUser)
        {
            NotificationCategoryRequest request = new NotificationCategoryRequest();
            request.NotificationCategoryID = NotificationCategoryID;
            request.SetDefaults();

            ListResponse response = await NotificationCategory_Search(request, serviceUser);

            NotificationCategoryResponse NotificationCategory = new NotificationCategoryResponse();
            if (response == null || response.Result == null)
            {
                return NotificationCategory;
            }

            List<NotificationCategoryResponse> list = (List<NotificationCategoryResponse>)response.Result;
            if (list.Count == 0)
            {
                return NotificationCategory;
            }
            NotificationCategory = list[0];

            return NotificationCategory;
        }

        public async Task<NotificationCategoryResponse> NotificationCategory_GetByUID(Guid NotificationCategoryUID, IEDUMITRAServiceUser serviceUser)
        {
            NotificationCategoryRequest request = new NotificationCategoryRequest();
            request.NotificationCategoryUID = NotificationCategoryUID;
            request.SetDefaults();

            ListResponse response = await NotificationCategory_Search(request, serviceUser);

            NotificationCategoryResponse NotificationCategory = new NotificationCategoryResponse();
            if (response == null || response.Result == null)
            {
                return NotificationCategory;
            }

            List<NotificationCategoryResponse> list = (List<NotificationCategoryResponse>)response.Result;
            if (list.Count == 0)
            {
                return NotificationCategory;
            }
            NotificationCategory = list[0];

            return NotificationCategory;
        }

        public async Task<SimpleResponse> NotificationCategory_AddUpdate(NotificationCategoryResponse request, IEDUMITRAServiceUser FIAAPIUser)
        {
            SimpleResponse response = new SimpleResponse();
            long NotificationCategoryID = await _repository.NotificationCategory_AddUpdate(request, FIAAPIUser);

            if (NotificationCategoryID > 0)
                response.Result = NotificationCategoryID;
            else
                response.SetError(ErrorCodes.SERVER_ERROR);

            return response;
        }

        public async Task<int> NotificationCategory_UpdateStatus(NotificationCategoryStatusUpdateRequest request, IEDUMITRAServiceUser FIAAPIUser)
        {
            return await _repository.NotificationCategory_UpdateStatus(request, FIAAPIUser);
        }
    }
}