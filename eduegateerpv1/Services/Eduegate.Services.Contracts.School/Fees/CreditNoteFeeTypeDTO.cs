using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public class CreditNoteFeeTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public CreditNoteFeeTypeDTO()
        {
            FeeMaster = new KeyValueDTO();
            FeePeriod = new KeyValueDTO();
            Months = new KeyValueDTO();
            Years = new KeyValueDTO();
        }
        [DataMember]
        public long CreditNoteFeeTypeMapIID { get; set; }
        [DataMember]
        public long? SchoolCreditNoteID { get; set; }
        [DataMember]
        public decimal? Amount { get; set; }
        [DataMember]
        public int? FeeMasterID { get; set; }
        [DataMember]
        public bool Status { get; set; }
        [DataMember]
        public KeyValueDTO FeeMaster { get; set; }

        [DataMember]
        public int? FeePeriodID { get; set; }

        [DataMember]
        public KeyValueDTO FeePeriod { get; set; }
        
        [DataMember]
        public int? YearID { get; set; }
        [DataMember]
        public int? MonthID { get; set; }

        [DataMember]
        public KeyValueDTO Years { get; set; }

        [DataMember]
        public KeyValueDTO Months { get; set; }

        [DataMember]
        public long? FeeDueFeeTypeMapsID { get; set; }

        [DataMember]
        public long? FeeDueMonthlySplitID { get; set; }

        [DataMember]
        public KeyValueDTO InvoiceNo { get; set; }
    }
}
