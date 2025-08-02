using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class BannerMasterDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long BannerIID { get; set; }

        [DataMember]
        public string BannerName { get; set; }

        [DataMember]
        public string BannerFile { get; set; }

        [DataMember]
        public Nullable<int> BannerTypeID { get; set; }

        [DataMember]
        public string ReferenceID { get; set; }

        [DataMember]
        public byte Frequency { get; set; }

        [DataMember]
        public string Link { get; set; }

        [DataMember]
        public string Target { get; set; }

        [DataMember]
        public int StatusID { get; set; }

        [DataMember]
        public bool isInternalLink { get; set; }

        [DataMember]
        public Nullable<int> ActionLinkTypeID { get; set; }

        [DataMember]
        public long SerialNo { get; set; }
        [DataMember]
        public int CompanyID { get; set; }
    }
}
