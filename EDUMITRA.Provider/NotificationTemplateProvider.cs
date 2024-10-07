namespace EDUMITRA.Provider
{
    using EDUMITRA.Commonlib.Utility;
    using EDUMITRA.Datamodel.Interfaces;
    using EDUMITRA.Datamodel.Shared;
    using EDUMITRA.DataModel.Masters.Notification.NotificationTemplate;
    using EDUMITRA.Provider;
    using EDUMITRA.Repository;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class NotificationTemplateProvider
    {
        private readonly NotificationTemplateRepository _repository = null;
        private readonly NotificationCategoryProvider _notificationCategoryProvider = null;
        private readonly NotificationConfigProvider _notificationConfigProvider = null;
        

        public NotificationTemplateProvider()
        {
            _repository = new NotificationTemplateRepository();
            _notificationCategoryProvider = new NotificationCategoryProvider();
            _notificationConfigProvider = new NotificationConfigProvider();
        }

        public async Task<ListResponse> NotificationTemplate_Search(NotificationTemplateRequest request, IEDUMITRAServiceUser serviceUser)
        {
           
            ListResponse list = await _repository.NotificationTemplate_Search(request, serviceUser);
            if (list != null && list.TotalRecords > 0)
            {
                List<NotificationTemplateResponse> lstNotificationTemplate = list.DeserializeListResponse<List<NotificationTemplateResponse>>();

                foreach (var item in lstNotificationTemplate)
                {
                   // item.Organization = await _organizationProvider.Organization_GetByID(item.OrganizationID, serviceUser);
                    item.NotificationCategory = await _notificationCategoryProvider.NotificationCategory_GetByID(item.NotificationCategoryID, serviceUser);
                    item.NotificationConfig = await _notificationConfigProvider.NotificationConfig_GetByID(item.NotificationConfigID, serviceUser);
                }
                list.Result = lstNotificationTemplate;
            }

            return list;
        }

        public async Task<NotificationTemplateResponse> NotificationTemplate_GetByID(int NotificationTemplateID, IEDUMITRAServiceUser serviceUser)
        {
            NotificationTemplateRequest request = new NotificationTemplateRequest();
            request.NotificationTemplateID = NotificationTemplateID;
            request.SetDefaults();

            ListResponse response = await NotificationTemplate_Search(request, serviceUser);

            NotificationTemplateResponse NotificationTemplate = new NotificationTemplateResponse();
            if (response == null || response.Result == null)
            {
                return NotificationTemplate;
            }

            List<NotificationTemplateResponse> list = (List<NotificationTemplateResponse>)response.Result;
            if (list.Count == 0)
            {
                return NotificationTemplate;
            }
            NotificationTemplate = list[0];

            return NotificationTemplate;
        }


        public async Task<NotificationTemplateResponse> NotificationTemplate_GetByUID(Guid NotificationTemplateUID, IEDUMITRAServiceUser serviceUser)
        {
            NotificationTemplateRequest request = new NotificationTemplateRequest();
            request.NotificationTemplateUID = NotificationTemplateUID;
            request.SetDefaults();

            ListResponse response = await NotificationTemplate_Search(request, serviceUser);

            NotificationTemplateResponse NotificationTemplate = new NotificationTemplateResponse();
            if (response == null || response.Result == null)
            {
                return NotificationTemplate;
            }

            List<NotificationTemplateResponse> list = (List<NotificationTemplateResponse>)response.Result;
            if (list.Count == 0)
            {
                return NotificationTemplate;
            }
            NotificationTemplate = list[0];

            return NotificationTemplate;
        }


        public async Task<SimpleResponse> NotificationTemplate_AddUpdate(NotificationTemplateResponse request, IEDUMITRAServiceUser FIAAPIUser)
        {
            SimpleResponse response = new SimpleResponse();
            long NotificationTemplateID = await _repository.NotificationTemplate_AddUpdate(request, FIAAPIUser);

            if (NotificationTemplateID > 0)
                response.Result = NotificationTemplateID;
            else
                response.SetError(ErrorCodes.SERVER_ERROR);

            return response;
        }

        public async Task<int> NotificationTemplate_UpdateStatus(NotificationTemplateStatusUpdateRequest request, IEDUMITRAServiceUser FIAAPIUser)
        {
            return await _repository.NotificationTemplate_UpdateStatus(request, FIAAPIUser);
        }
    }
}