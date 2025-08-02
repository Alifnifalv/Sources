using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public class FeeDueFeeFineMapDTO : BaseMasterDTO
    {
        [DataMember]
        public int? FineMasterID { get; set; }

        [DataMember]
        public string FineName { get; set; }

        [DataMember]
        public long? FineMasterStudentMapID { get; set; }

        [DataMember]
        public long? FeeDueFeeTypeMapsID  { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

      

        [DataMember]
        public long? StudentFeeDueID { get; set; }

        [DataMember]
        public bool? FeeCollectionStatus { get; set; }

        [DataMember]
        public string InvoiceNo { get; set; }

        [DataMember]
        public DateTime? InvoiceDate { get; set; }

        [DataMember]
        public DateTime? FineMapDate { get; set; }
    }
}
