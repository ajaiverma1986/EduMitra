using EDUMITRA.Commonlib.Security;
using EDUMITRA.Configuration;
using EDUMITRA.Datamodel.Masters;
using EDUMITRA.Datamodel.Shared;
using EDUMITRA.Provider.Shared;
using EDUMITRA.Repository;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EDUMITRA.Provider
{
    public class UtilityProvider : BaseProvider
    {
        private readonly UtilityRepository repository = null;
        public UtilityProvider()
        {
            repository = new UtilityRepository();
        }

       

        public async Task<OTPResponse> SendOTP(OTPRequest otpRequest)
        {
            string smscontent = "";
            OTPResponse response = new OTPResponse();

            if (otpRequest != null && !string.IsNullOrEmpty(otpRequest.mobileno))
            {
                string _otp = CommonHelper.RandomDigits(6);
                    smscontent = "" + _otp + " is the reference no. for FIA Verification. Do Not share the reference no. with anyone other than the agent assisting.";

                    response = await repository.SendOTP(otpRequest.mobileno, _otp);
               
            }
            return response;
        }

        public async Task<SimpleResponse> ValidateOTP(OTPValidateRequest fIAOTPValidateRequest)
        {
            SimpleResponse response = new SimpleResponse();

            if (fIAOTPValidateRequest != null && !string.IsNullOrEmpty(fIAOTPValidateRequest.mobileno) && !string.IsNullOrEmpty(fIAOTPValidateRequest.otp))
            {
                response = await repository.ValidateOTP(fIAOTPValidateRequest.mobileno, fIAOTPValidateRequest.otp);

            }
            return response;
        }
      
        public string GetHMACSHA256(string text, string key)
        {
            UTF8Encoding encoder = new UTF8Encoding();

            byte[] hashValue;
            byte[] keybyt = encoder.GetBytes(key);
            byte[] message = encoder.GetBytes(text);

            HMACSHA256 hashString = new HMACSHA256(keybyt);
            string hex = "";

            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }

    }
}
