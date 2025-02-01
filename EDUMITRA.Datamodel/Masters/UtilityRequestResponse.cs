using System;
using System.Collections.Generic;
using System.Text;

namespace EDUMITRA.Datamodel.Masters
{
    public class OTPRequest
    {
        public string mobileno { get; set; }
    }
   
    public class OTPResponse
    {
        public OTPResponse()
        {
            status = "Error";
            response_message = "Please Try Again";
            OTPID = "";
        }
        public string status { get; set; }
        public string response_message { get; set; }
        public string OTPID { get; set; }
    }
    public class OTPValidateRequest
    {
        public string mobileno { get; set; }
        public string otp { get; set; }
    }
}
