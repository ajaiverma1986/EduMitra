namespace EDUMITRA.Provider
{
    using EDUMITRA.Commonlib.Utility;
    using EDUMITRA.Datamodel.Interfaces;
    using EDUMITRA.Datamodel.Shared;
    using EDUMITRA.DataModel.Masters.Notification.NotificationConfig;
    using EDUMITRA.Repository;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class NotificationConfigProvider
    {
        private readonly NotificationConfigRepository _repository = null;
        private readonly EmailGatewayProvider _emailGatewayProvider = null;
        private readonly SMSGatewayProvider _smsGatewayProvider = null;

        public NotificationConfigProvider()
        {
            _repository = new NotificationConfigRepository();
            _emailGatewayProvider = new EmailGatewayProvider();
            _smsGatewayProvider = new SMSGatewayProvider();
        }

        public async Task<ListResponse> NotificationConfig_Search(NotificationConfigRequest request, IEDUMITRAServiceUser serviceUser)
        {
            
            ListResponse list = await _repository.NotificationConfig_Search(request, serviceUser);
            if (list != null && list.TotalRecords > 0)
            {
                List<NotificationConfigResponse> lstNotificationConfig = list.DeserializeListResponse<List<NotificationConfigResponse>>();

                foreach (var item in lstNotificationConfig)
                {
                   
                    item.EmailGateway = await _emailGatewayProvider.EmailGateway_GetByID(item.EmailGatewayID, serviceUser);
                    item.SMSGateway = await _smsGatewayProvider.SMSGateway_GetByID(item.SMSGatewayID, serviceUser);
                }
                list.Result = lstNotificationConfig;
            }

            return list;

        }

        public async Task<NotificationConfigResponse> NotificationConfig_GetByID(byte NotificationConfigID, IEDUMITRAServiceUser serviceUser)
        {
            NotificationConfigRequest request = new NotificationConfigRequest();
            request.NotificationConfigID = NotificationConfigID;
            request.SetDefaults();

            ListResponse response = await NotificationConfig_Search(request, serviceUser);

            NotificationConfigResponse NotificationConfig = new NotificationConfigResponse();
            if (response == null || response.Result == null)
            {
                return NotificationConfig;
            }

            List<NotificationConfigResponse> list = (List<NotificationConfigResponse>)response.Result;
            if (list.Count == 0)
            {
                return NotificationConfig;
            }
            NotificationConfig = list[0];

            //NotificationConfig.Organization = await _organizationProvider.Organization_GetByID(NotificationConfig.OrganizationID, serviceUser);

            NotificationConfig.EmailGateway = await _emailGatewayProvider.EmailGateway_GetByID(NotificationConfig.EmailGatewayID, serviceUser);

            NotificationConfig.SMSGateway = await _smsGatewayProvider.SMSGateway_GetByID(NotificationConfig.SMSGatewayID, serviceUser);

            return NotificationConfig;
        }

        public async Task<NotificationConfigResponse> NotificationConfig_GetByOrgID(int OrganizationID, IEDUMITRAServiceUser serviceUser)
        {
            NotificationConfigRequest request = new NotificationConfigRequest();
            request.OrganizationID = OrganizationID;
            request.SetDefaults();

            ListResponse response = await NotificationConfig_Search(request, serviceUser);

            NotificationConfigResponse NotificationConfig = new NotificationConfigResponse();
            if (response == null || response.Result == null)
            {
                return NotificationConfig;
            }

            List<NotificationConfigResponse> list = (List<NotificationConfigResponse>)response.Result;
            if (list.Count == 0)
            {
                return NotificationConfig;
            }
            NotificationConfig = list[0];

            //NotificationConfig.Organization = await _organizationProvider.Organization_GetByID(NotificationConfig.OrganizationID, serviceUser);

            NotificationConfig.EmailGateway = await _emailGatewayProvider.EmailGateway_GetByID(NotificationConfig.EmailGatewayID, serviceUser);

            NotificationConfig.SMSGateway = await _smsGatewayProvider.SMSGateway_GetByID(NotificationConfig.SMSGatewayID, serviceUser);

            return NotificationConfig;
        }

        public async Task<NotificationConfigResponse> NotificationConfig_GetByUID(Guid NotificationConfigUID, IEDUMITRAServiceUser serviceUser)
        {
            NotificationConfigRequest request = new NotificationConfigRequest();
            request.NotificationConfigUID = NotificationConfigUID;
            request.SetDefaults();

            ListResponse response = await NotificationConfig_Search(request, serviceUser);

            NotificationConfigResponse NotificationConfig = new NotificationConfigResponse();
            if (response == null || response.Result == null)
            {
                return NotificationConfig;
            }

            List<NotificationConfigResponse> list = (List<NotificationConfigResponse>)response.Result;
            if (list.Count == 0)
            {
                return NotificationConfig;
            }
            NotificationConfig = list[0];

            //NotificationConfig.Organization = await _organizationProvider.Organization_GetByID(NotificationConfig.OrganizationID, serviceUser);

            NotificationConfig.EmailGateway = await _emailGatewayProvider.EmailGateway_GetByID(NotificationConfig.EmailGatewayID, serviceUser);

            NotificationConfig.SMSGateway = await _smsGatewayProvider.SMSGateway_GetByID(NotificationConfig.SMSGatewayID, serviceUser);

            return NotificationConfig;
        }

        public async Task<SimpleResponse> NotificationConfig_AddUpdate(NotificationConfigResponse request, IEDUMITRAServiceUser FIAAPIUser)
        {
            SimpleResponse response = new SimpleResponse();
            long NotificationConfigID = await _repository.NotificationConfig_AddUpdate(request, FIAAPIUser);

            if (NotificationConfigID > 0)
                response.Result = NotificationConfigID;
            else
                response.SetError(ErrorCodes.SERVER_ERROR);

            return response;
        }

        public async Task<int> NotificationConfig_UpdateStatus(NotificationConfigStatusUpdateRequest request, IEDUMITRAServiceUser FIAAPIUser)
        {
            return await _repository.NotificationConfig_UpdateStatus(request, FIAAPIUser);
        }
    }
}