using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
      [DataContract]
  public  class NotifyMeDTO
    {
          [DataMember]
        public long NotifyIID { get; set; }

          [DataMember]
        public Nullable<long> ProductSKUMapID { get; set; }

          [DataMember]
        public string EmailID { get; set; }
        [DataMember]
        public string CompanyID { get; set; }

        [DataMember]
        public Nullable<int> SiteID { get; set; }
          [DataMember]
        public Nullable<bool> IsEmailSend { get; set; }

          [DataMember]
          public short StatusID { get; set; }

          [DataMember]
          public string StatusMessage { get; set; }
    }
}
