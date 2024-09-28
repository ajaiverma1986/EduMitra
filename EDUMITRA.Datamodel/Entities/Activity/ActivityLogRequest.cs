using EDUMITRA.Datamodel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUMITRA.Datamodel.Entities.Activity
{
    public class ActivityLogRequest : ListRequest
    {
        public long? ActivityID { get; set; }

        public long? EntityID { get; set; }

        public long? EntityTypeID { get; set; }
    }
}
