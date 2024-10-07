namespace EDUMITRA.Provider
{
    using EDUMITRA.Datamodel.Interfaces;
    using EDUMITRA.Datamodel.Shared;
    using EDUMITRA.DataModel.Masters.Notification.SMSGateway;
    using FIA.Repository;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SMSGatewayProvider
    {
        private readonly SMSGatewayRepository _repository = null;
        

        public SMSGatewayProvider()
        {
            _repository = new SMSGatewayRepository();

          
        }

        public async Task<ListResponse> SMSGateway_Search(SMSGatewayRequest request, IEDUMITRAServiceUser serviceUser)
        {
            return await _repository.SMSGateway_Search(request, serviceUser);
        }

        public async Task<SMSGatewayResponse> SMSGateway_GetByID(byte SMSGatewayID, IEDUMITRAServiceUser serviceUser)
        {
            SMSGatewayRequest request = new SMSGatewayRequest();
            request.SMSGatewayID = SMSGatewayID;
            request.SetDefaults();

            ListResponse response = await SMSGateway_Search(request, serviceUser);

            SMSGatewayResponse SMSGateway = new SMSGatewayResponse();
            if (response == null || response.Result == null)
            {
                return SMSGateway;
            }

            List<SMSGatewayResponse> list = (List<SMSGatewayResponse>)response.Result;
            if (list.Count == 0)
            {
                return SMSGateway;
            }
            SMSGateway = list[0];

            return SMSGateway;
        }


        public async Task<SMSGatewayResponse> SMSGateway_GetByUID(Guid SMSGatewayUID, IEDUMITRAServiceUser serviceUser)
        {
            SMSGatewayRequest request = new SMSGatewayRequest();
            request.SMSGatewayUID = SMSGatewayUID;
            request.SetDefaults();

            ListResponse response = await SMSGateway_Search(request, serviceUser);

            SMSGatewayResponse SMSGateway = new SMSGatewayResponse();
            if (response == null || response.Result == null)
            {
                return SMSGateway;
            }

            List<SMSGatewayResponse> list = (List<SMSGatewayResponse>)response.Result;
            if (list.Count == 0)
            {
                return SMSGateway;
            }
            SMSGateway = list[0];

            return SMSGateway;
        }


        public async Task<SimpleResponse> SMSGateway_AddUpdate(SMSGatewayResponse request, IEDUMITRAServiceUser FIAAPIUser)
        {
            SimpleResponse response = new SimpleResponse();
            long SMSGatewayID = await _repository.SMSGateway_AddUpdate(request, FIAAPIUser);

            if (SMSGatewayID > 0)
                response.Result = SMSGatewayID;
            else
                response.SetError(ErrorCodes.SERVER_ERROR);

            return response;
        }

        public async Task<int> SMSGateway_UpdateStatus(SMSGatewayStatusUpdateRequest request, IEDUMITRAServiceUser FIAAPIUser)
        {
            return await _repository.SMSGateway_UpdateStatus(request, FIAAPIUser);
        }
    }
}