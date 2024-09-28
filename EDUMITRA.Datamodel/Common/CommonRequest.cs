using System;
using System.Collections.Generic;
using System.Text;

namespace EDUMITRA.Datamodel.Common
{
    public class ApiRequestLog
    {

        public string apiname { get; set; }
        public string plainrequest { get; set; }
        public string plainresponse { get; set; }
        public string encryptedrequest { get; set; }
        public string encryptedresponse { get; set; }

    }
}
