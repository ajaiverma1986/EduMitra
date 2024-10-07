namespace EDUMITRA.Provider
{
    using EDUMITRA.Datamodel.Interfaces;
    using EDUMITRA.Datamodel.Shared;
    using EDUMITRA.DataModel.Masters.Notification.EmailGateway;
    using EDUMITRA.Repository;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class EmailGatewayProvider
    {
        private readonly EmailGatewayRepository _repository = null;
       
        public EmailGatewayProvider()
        {
            _repository = new EmailGatewayRepository();

          
        }

        public async Task<ListResponse> EmailGateway_Search(EmailGatewayRequest request, IEDUMITRAServiceUser serviceUser)
        {
            return await _repository.EmailGateway_Search(request, serviceUser);
            
        }

        public async Task<EmailGatewayResponse> EmailGateway_GetByID(byte EmailGatewayID, IEDUMITRAServiceUser serviceUser)
        {
            EmailGatewayRequest request = new EmailGatewayRequest();
            request.EmailGatewayID = EmailGatewayID;
            request.SetDefaults();

            ListResponse response = await EmailGateway_Search(request, serviceUser);

            EmailGatewayResponse EmailGateway = new EmailGatewayResponse();
            if (response == null || response.Result == null)
            {
                return EmailGateway;
            }

            List<EmailGatewayResponse> list = (List<EmailGatewayResponse>)response.Result;
            if (list.Count == 0)
            {
                return EmailGateway;
            }
            EmailGateway = list[0];

            return EmailGateway;
        }


        public async Task<EmailGatewayResponse> EmailGateway_GetByUID(Guid EmailGatewayUID, IEDUMITRAServiceUser serviceUser)
        {
            EmailGatewayRequest request = new EmailGatewayRequest();
            request.EmailGatewayUID = EmailGatewayUID;
            request.SetDefaults();

            ListResponse response = await EmailGateway_Search(request, serviceUser);

            EmailGatewayResponse EmailGateway = new EmailGatewayResponse();
            if (response == null || response.Result == null)
            {
                return EmailGateway;
            }

            List<EmailGatewayResponse> list = (List<EmailGatewayResponse>)response.Result;
            if (list.Count == 0)
            {
                return EmailGateway;
            }
            EmailGateway = list[0];

            return EmailGateway;
        }


        public async Task<SimpleResponse> EmailGateway_AddUpdate(EmailGatewayResponse request, IEDUMITRAServiceUser FIAAPIUser)
        {
            SimpleResponse response = new SimpleResponse();
            long EmailGatewayID = await _repository.EmailGateway_AddUpdate(request, FIAAPIUser);

            if (EmailGatewayID > 0)
                response.Result = EmailGatewayID;
            else
                response.SetError(ErrorCodes.SERVER_ERROR);

            return response;
        }

        public async Task<int> EmailGateway_UpdateStatus(EmailGatewayStatusUpdateRequest request, IEDUMITRAServiceUser FIAAPIUser)
        {
            return await _repository.EmailGateway_UpdateStatus(request, FIAAPIUser);
        }
    }
}