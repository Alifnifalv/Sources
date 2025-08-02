using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public  class FeeStructureMontlySplitMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long FeeStructureMontlySplitMapIID { get; set; }
        [DataMember]
        public long? FeeStructureFeeMapID { get; set; }
        [DataMember]
        public int MonthID { get; set; }
        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public decimal? TaxPercentage { get; set; }
        [DataMember]
        public decimal? TaxAmount { get; set; }
        [DataMember]
        public int Year { get; set; }

    }
}

