namespace FIA.Provider
{
    using EDUMITRA.Commonlib.Utility;
    using EDUMITRA.Datamodel.Common;
    using EDUMITRA.Datamodel.Entities.Authorization;
    using EDUMITRA.Datamodel.Interfaces;
    using EDUMITRA.Datamodel.Shared;
    using EDUMITRA.DataModel.Masters.Notification;
    using EDUMITRA.DataModel.Masters.Notification.NotificationConfig;
    using EDUMITRA.DataModel.Masters.Notification.NotificationLog;
    using EDUMITRA.DataModel.Masters.Notification.NotificationTemplate;
    using EDUMITRA.Provider;
    #region Import Namespace

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Text;
    using System.Threading.Tasks;
    #endregion

    /// <summary>
    /// Notification Provider to Send SMS, Email and Notifications
    /// </summary>
    public class NotificationProvider
    {
        #region Declaration
        private readonly NotificationConfigProvider _notificationConfigProvider = null;
        private readonly NotificationLogProvider _notificationLogProvider = null;
        private readonly NotificationTemplateProvider _notificationTemplateProvider = null;
        private readonly UserDetails _userMasterProvider = null;
        #endregion

        #region Constructor
        public NotificationProvider()
        {
            _notificationLogProvider = new NotificationLogProvider();
            _notificationConfigProvider = new NotificationConfigProvider();
            _notificationTemplateProvider = new NotificationTemplateProvider();
            _userMasterProvider = new UserDetails();
        }
        #endregion

        /// <summary>
        /// Send Notification Method,
        /// Which can be used to send SMS, Email & other notification like Push notifications
        /// These method uses the Notification Template and replace Body and Subject Parameters from request object
        /// </summary>
        /// <param name="request"></param>
        /// <param name="serviceUser"></param>
        /// <returns></returns>
        #region Send Notification Method
        public async Task<SimpleResponse> Send(NotificationRequest request, IEDUMITRAServiceUser serviceUser)
        {
            SimpleResponse response = new SimpleResponse();

            NotificationTemplateResponse notificationTemplate = await _notificationTemplateProvider.NotificationTemplate_GetByID((int)request.NotificationTemplate, serviceUser);
            //UserMasterDetailResponse userMaster = null;

            if (notificationTemplate.NotificationTemplateID == null || notificationTemplate.NotificationTemplateID <= 0)
            {
                response.SetError(ErrorCodes.INVALID_PARAMETERS, "Invalid Notification Template ID");
                return response;
            }
            if (request.UserMasterID != null && request.UserMasterID.HasValue && request.UserMasterID.Value > 0)
            {
                //userMaster = await _userMasterProvider.UserMaster_GetByID(request.UserMasterID.Value, serviceUser);
            }

            #region Prepare & Send SMS
            //if ((!string.IsNullOrWhiteSpace(request.MobileNumber) || (userMaster != null && !string.IsNullOrWhiteSpace(userMaster.Mobile))) && notificationTemplate.SendSMS)
            //{
            //    StringBuilder SMSText = new StringBuilder(notificationTemplate.SmsTemplate);

            //    foreach (string key in request.BodyParameters.Keys)
            //    {
            //        SMSText = SMSText.Replace("#" + key.ToUpper() + "#", request.BodyParameters[key]);
            //    }

            //    if (!string.IsNullOrWhiteSpace(request.MobileNumber))
            //    {
            //        response = await SendSMS(request.MobileNumber, SMSText.ToString(), serviceUser, request.UserMasterID);
            //    }

            //    if (userMaster != null && !string.IsNullOrWhiteSpace(userMaster.Mobile))
            //    {
            //        response = await SendSMS(userMaster.Mobile, SMSText.ToString(), serviceUser, request.UserMasterID);
            //    }
            //}
            #endregion

            #region Prepare & Send Email
            StringBuilder Subject = new StringBuilder(notificationTemplate.EmailSubject);
            StringBuilder MessageBody = new StringBuilder(notificationTemplate.EmailBody);

            foreach (string key in request.SubjectParameters.Keys)
            {
                Subject.Replace("#" + key.ToLower() + "#", request.SubjectParameters[key]);
            }

            foreach (string key in request.BodyParameters.Keys)
            {
                MessageBody.Replace("#" + key.ToLower() + "#", request.BodyParameters[key]);
            }

            //if (userMaster != null && !string.IsNullOrWhiteSpace(userMaster.Email))
            //{
            //    if (!string.IsNullOrWhiteSpace(request.TO))
            //        request.TO = $"{request.TO};{userMaster.Email};";
            //    else
            //        request.TO = $"{userMaster.Email};";
            //}

            response = await SendEMail(request.TO, Subject.ToString(), MessageBody.ToString(), serviceUser, request.CC, request.BCC, request.UserMasterID, request.Attachments);
            #endregion

            return response;
        }
        #endregion

        /// <summary>
        /// Send EMail method, which Send Email based on the Notification Configuration
        /// This method also logs records of the notification in the Notification Log Table.
        /// </summary>
        /// <param name="TO"></param>
        /// <param name="subjectName"></param>
        /// <param name="body"></param>
        /// <param name="serviceUser"></param>
        /// <param name="CC"></param>
        /// <param name="BCC"></param>
        /// <param name="UserMasterID"></param>
        /// <param name="Files"></param>
        /// <param name="Internal"></param>
        /// <returns></returns>
        #region SendEMail
        public async Task<SimpleResponse> SendEMail(string TO, string subjectName, string body, IEDUMITRAServiceUser serviceUser, string CC = null, string BCC = null, long? UserMasterID = null, List<Attachment> Files = null, bool Internal = false)
        {
            SimpleResponse response = new SimpleResponse();

            long NotificationLogID;
            Guid _notificationUID = Guid.Empty;

            if (string.IsNullOrWhiteSpace(TO))
            {
                TO = "ajai.verma@fiaglobal.com;";
            }

            NotificationLogResponse notificationLog = new NotificationLogResponse()
            {
                SentTo = TO,
                CC = CC,
                BCC = BCC,
                Subject = subjectName,
                MessageBody = body,
                Status = NotificationLogStatus.Pending,
                UserMasterID = UserMasterID ?? serviceUser.UserMasterID,
                NotificationMediumTypeID = NotificationMediumType.Email
            };
            SimpleResponse notificationLogResponse = await _notificationLogProvider.NotificationLog_AddUpdate(notificationLog, serviceUser);

            if (notificationLogResponse == null)
            {
                response.SetError(ErrorCodes.SERVER_ERROR);
                return response;
            }
            else if (notificationLogResponse.HasError)
            {
                response.SetError(notificationLogResponse.Errors);
                return response;
            }
            else
            {
                NotificationLogID = notificationLogResponse.DeserializeSimpleResponse<long>();
                notificationLog = await _notificationLogProvider.NotificationLog_GetByID(NotificationLogID, serviceUser);
                if (notificationLog != null && notificationLog.NotificationLogID > 0)
                {
                    _notificationUID = notificationLog.NotificationLogUID.Value;
                }
            }

            NotificationConfigResponse notificationConfig = null;
            if (serviceUser.OrganizationID == null || serviceUser.OrganizationID <= 0)
            {
                serviceUser.OrganizationID = 1; //Passing FIA Organization ID if Organization ID is not available somehow.
            }

            notificationConfig = await _notificationConfigProvider.NotificationConfig_GetByOrgID(serviceUser.OrganizationID.Value, serviceUser);
            if (notificationConfig == null || notificationConfig.NotificationConfigID <= 0 || notificationConfig.EmailGatewayID <= 0 || notificationConfig.EmailGateway == null)
            {
                NotificationLogStatusUpdateRequest statusUpdateRequest = new NotificationLogStatusUpdateRequest()
                {
                    NotificationLogUID = _notificationUID,
                    LastMessageSendResponse = "Notification Configuration not found for the org",
                    Status = NotificationLogStatus.Error
                };
                await _notificationLogProvider.NotificationLog_UpdateStatus(statusUpdateRequest, serviceUser);
                response.SetError(ErrorCodes.UNEXPECTED_ERROR_FOUND);
                return response;
            }

            //SmtpClient smtp = new SmtpClient("smtp.elasticemail.com", 2525);
            //smtp.UseDefaultCredentials = false;

            //smtp.Credentials = new NetworkCredential("amit.vyas@fiaglobal.com", "ba1c2c16-9034-4a14-bb60-78be480b926c");
            //smtp.EnableSsl = false;
            //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;


            //MailAddress from = new MailAddress("amit.vyas@fiaglobal.com", "Amit Vyas");

            //--------------------------//
            //TO = string.Empty;

            //if (Internal)
            //{
            //    TO = TO.Split(";").SkipWhile(x => x.ToString().Contains("fiaglobal.com")).ToString();
            //    CC = CC.Split(";").SkipWhile(x => x.ToString().Contains("fiaglobal.com")).ToString();
            //    BCC = BCC.Split(";").SkipWhile(x => x.ToString().Contains("fiaglobal.com")).ToString();
            //}

            SmtpClient smtp = new SmtpClient(notificationConfig.EmailGateway.SMTPServer, notificationConfig.EmailGateway.SMTPPort)
            {
                //smtp.UseDefaultCredentials = false;
                Credentials = new NetworkCredential(notificationConfig.EmailGateway.SMTPUsername, notificationConfig.EmailGateway.SMTPPassword),
                EnableSsl = true
            };
            //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            MailAddress from = new MailAddress(notificationConfig.EmailGateway.SMTPSenderEmail, notificationConfig.EmailGateway.SMTPSenderName);

            MailMessage msg = new MailMessage();
            msg.From = from;

            if (!string.IsNullOrWhiteSpace(TO))
            {
                string[] _to;
                TO = TO.Replace(",", ";").Replace(" ", "");
                _to = TO.Split(";");

                foreach (var item in _to)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        MailAddress to = new MailAddress(item, item);
                        msg.To.Add(to);
                    }
                }
            }
            else
            {
                msg.To.Add(new MailAddress("amit.vyas@fiaglobal.com", "amit.vyas@fiaglobal.com"));
            }

            if (!string.IsNullOrWhiteSpace(CC))
            {
                string[] _cc;
                CC = CC.Replace(",", ";").Replace(" ", "");
                _cc = CC.Split(";");

                foreach (var item in _cc)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        MailAddress cc = new MailAddress(item, item);
                        msg.CC.Add(cc);
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(BCC))
            {
                string[] _bcc;
                CC = BCC.Replace(",", ";").Replace(" ", "");
                _bcc = BCC.Split(";");

                foreach (var item in _bcc)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        MailAddress bcc = new MailAddress(item, item);
                        msg.Bcc.Add(bcc);
                    }
                }
            }

            msg.Body = body;
            msg.IsBodyHtml = true;
            msg.Subject = subjectName; ;// + " - " + FileName;

            if (Files != null && Files.Count > 0)
            {
                foreach (var item in Files)
                {
                    msg.Attachments.Add(item);
                }
            }

            try
            {
                await smtp.SendMailAsync(msg);

                NotificationLogStatusUpdateRequest statusUpdateRequest = new NotificationLogStatusUpdateRequest()
                {
                    NotificationLogUID = _notificationUID,
                    LastMessageSendResponse = "EMail Sent",
                    Status = NotificationLogStatus.Sent
                };
                await _notificationLogProvider.NotificationLog_UpdateStatus(statusUpdateRequest, serviceUser);
                response.Result = "EMail Sent";
                return response;
            }
            catch (Exception ex)
            {
                NotificationLogStatusUpdateRequest statusUpdateRequest = new NotificationLogStatusUpdateRequest()
                {
                    NotificationLogUID = _notificationUID,
                    LastMessageSendResponse = ex.Message,
                    Status = NotificationLogStatus.Error
                };
                await _notificationLogProvider.NotificationLog_UpdateStatus(statusUpdateRequest, serviceUser);
                response.SetError(ErrorCodes.UNEXPECTED_ERROR_FOUND);
                return response;
            }

            //var client = new SmtpClient("smtp.gmail.com", 587)
            //{
            //    Credentials = new NetworkCredential("myusername@gmail.com", "mypwd"),
            //    EnableSsl = true
            //};
            //client.Send("myusername@gmail.com", "myusername@gmail.com", "test", "testbody")

            //var client = new SmtpClient("smtp.gmail.com", 587)
            //{
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential("fiaglobalIT@gmail.com", "P@ssword15"),
            //    EnableSsl = true
            //};
            //client.Send("fiaglobalIT@gmail.com", "fiaglobalIT@gmail.com", "testingsmtp", "testsmtpbody");
            //Console.WriteLine("Sent");
        }
        #endregion

        /// <summary>
        /// Send SMS method, which Send Email based on the Notification Configuration
        /// This method also logs records of the notification in the Notification Log Table.
        /// </summary>
        /// <param name="TO"></param>
        /// <param name="Message"></param>
        /// <param name="serviceUser"></param>
        /// <param name="UserMasterID"></param>
        /// <returns></returns>
        #region SendSMS
        public async Task<SimpleResponse> SendSMS(string TO, string Message, IEDUMITRAServiceUser serviceUser, long? UserMasterID = null)
        {
            SimpleResponse response = new SimpleResponse();

            long NotificationLogID;
            Guid _notificationUID = Guid.Empty;

            NotificationLogResponse notificationLog = new NotificationLogResponse()
            {
                SentTo = TO,
                MessageBody = Message,
                Status = NotificationLogStatus.Pending,
                UserMasterID = serviceUser.UserMasterID,
                NotificationMediumTypeID = NotificationMediumType.Email
            };
            SimpleResponse notificationLogResponse = await _notificationLogProvider.NotificationLog_AddUpdate(notificationLog, serviceUser);

            if (notificationLogResponse == null)
            {
                response.SetError(ErrorCodes.SERVER_ERROR);
                return response;
            }
            else if (notificationLogResponse.HasError)
            {
                response.SetError(notificationLogResponse.Errors);
                return response;
            }
            else
            {
                NotificationLogID = notificationLogResponse.DeserializeSimpleResponse<long>();
                notificationLog = await _notificationLogProvider.NotificationLog_GetByID(NotificationLogID, serviceUser);
                if (notificationLog != null && notificationLog.NotificationLogID > 0)
                {
                    _notificationUID = notificationLog.NotificationLogUID.Value;
                }
            }

            NotificationConfigResponse notificationConfig = null;
            if (serviceUser.OrganizationID != null && serviceUser.OrganizationID > 0)
            {
                notificationConfig = await _notificationConfigProvider.NotificationConfig_GetByOrgID(serviceUser.OrganizationID.Value, serviceUser);
                if (notificationConfig == null || notificationConfig.NotificationConfigID <= 0 || notificationConfig.SMSGatewayID <= 0 || notificationConfig.SMSGateway == null)
                {
                    NotificationLogStatusUpdateRequest statusUpdateRequest = new NotificationLogStatusUpdateRequest()
                    {
                        NotificationLogUID = _notificationUID,
                        LastMessageSendResponse = "Notification Configuration not found for the org",
                        Status = NotificationLogStatus.Error
                    };
                    await _notificationLogProvider.NotificationLog_UpdateStatus(statusUpdateRequest, serviceUser);
                    response.SetError(ErrorCodes.UNEXPECTED_ERROR_FOUND);
                    return response;
                }
            }
            else
            {
                NotificationLogStatusUpdateRequest statusUpdateRequest = new NotificationLogStatusUpdateRequest()
                {
                    NotificationLogUID = _notificationUID,
                    LastMessageSendResponse = "Invalid Organization ID",
                    Status = NotificationLogStatus.Error
                };
                await _notificationLogProvider.NotificationLog_UpdateStatus(statusUpdateRequest, serviceUser);
                response.SetError(ErrorCodes.UNEXPECTED_ERROR_FOUND);
                return response;
            }


            string baseURL = "";
            string successCode = "";
            string failureCode = "";

            baseURL = notificationConfig.SMSGateway.GatewayURL.ToLower();
            successCode = notificationConfig.SMSGateway.ReponseSuccess;
            failureCode = notificationConfig.SMSGateway.ReponseError;

            if (!string.IsNullOrEmpty(notificationConfig.SMSGateway.UserName))
                baseURL = baseURL.Replace("##username##", notificationConfig.SMSGateway.UserName);

            if (!string.IsNullOrEmpty(notificationConfig.SMSGateway.Password))
                baseURL = baseURL.Replace("##password##", notificationConfig.SMSGateway.Password);

            string _smsText = WebUtility.UrlEncode(Message); //Uri.EscapeUriString(SMSText); //Uri.EscapeDataString(SMSText);
            baseURL = baseURL.Replace("##mobile##", TO);
            baseURL = baseURL.Replace("##smsbody##", _smsText);
            if (!string.IsNullOrEmpty(notificationConfig.SMSGateway.SenderID))
                baseURL = baseURL.Replace("##senderid##", notificationConfig.SMSGateway.SenderID);

            SimpleResponse httpResp = await HttpClientHelper.HttpGet(baseURL);

            if (httpResp.HasError)
            {
                NotificationLogStatusUpdateRequest statusUpdateRequest = new NotificationLogStatusUpdateRequest()
                {
                    NotificationLogUID = _notificationUID,
                    LastMessageSendResponse = httpResp.Errors?.FirstOrDefault()?.ErrorMessage ?? "Something wrong happend",
                    Status = NotificationLogStatus.Error
                };
                await _notificationLogProvider.NotificationLog_UpdateStatus(statusUpdateRequest, serviceUser);
                response.SetError(httpResp.Errors);
                return response;
            }
            else
            {
                NotificationLogStatusUpdateRequest statusUpdateRequest = new NotificationLogStatusUpdateRequest()
                {
                    NotificationLogUID = _notificationUID,
                    LastMessageSendResponse = $"SMS Sent - {httpResp.Result}",
                    Status = NotificationLogStatus.Sent
                };
                await _notificationLogProvider.NotificationLog_UpdateStatus(statusUpdateRequest, serviceUser);
                response.Result = "SMS Sent";
            }

            return response;
        }
        #endregion
    }
}