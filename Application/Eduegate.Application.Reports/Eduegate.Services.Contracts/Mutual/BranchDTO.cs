using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public class BranchDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public BranchDTO()
        {
            BranchCultureDatas = new List<BranchCultureDataDTO>();
        }

        [DataMember]
        public long BranchIID { get; set; }
        [DataMember]
        public string BranchName { get; set; }
        [DataMember]
        public string Logo { get; set; }
        [DataMember]
        public long? BranchGroupID { get; set; }
        [DataMember]
        public long? WarehouseID { get; set; }
        [DataMember]
        public byte StatusID { get; set; }
        [DataMember]
        public bool? IsMarketPlace { get; set; }
        [DataMember]
        public List<PriceListDetailDTO> PriceLists { get; set; }
        [DataMember]
        public List<DocumentTypeDetailDTO> DocumentTypeMaps { get; set; }
        [DataMember]
        public int? CompanyID { get; set; }
        [DataMember]
        public string Latitude { get; set; }
        [DataMember]
        public string Longitude { get; set; }
        public List<BranchCultureDataDTO> BranchCultureDatas { get; set; }

        [DataMember]
        public bool? IsVirtual { get; set; }
        [DataMember]
        public string BranchCode { get; set; }
    }
}
