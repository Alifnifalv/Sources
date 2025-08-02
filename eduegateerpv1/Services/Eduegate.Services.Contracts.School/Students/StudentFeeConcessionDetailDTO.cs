using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Students
{   
    public class StudentFeeConcessionDetailDTO : BaseMasterDTO
    {
        public StudentFeeConcessionDetailDTO()
        {
            FeeMaster = new KeyValueDTO();
            FeePeriod = new KeyValueDTO();           
        }
        [DataMember]
        public long StudentFeeConcessionID { get; set; }

        [DataMember]
        public int? FeeMasterID { get; set; }

        [DataMember]
        public int? FeePeriodID { get; set; }

        [DataMember]
        public bool? IsPercentage { get; set; }
      
        [DataMember]
        public int? StudentGroupID { get; set; }

        [DataMember]
        public decimal? PercentageAmount { get; set; }

        [DataMember]
        public string Formula { get; set; }

        [DataMember]
        public KeyValueDTO FeeMaster { get; set; }    

        [DataMember]
        public KeyValueDTO FeeInvoice { get; set; }      

        [DataMember]
        public KeyValueDTO FeePeriod { get; set; }
        [DataMember]
        public decimal? NetAmount { get; set; }
        [DataMember]
        public decimal? DueAmount { get; set; }
        [DataMember]
        public decimal? ConcessionAmount { get; set; }
        [DataMember]
        public long? FeeDueFeeTypeMapsID { get; set; }
        [DataMember]
        public long? StudentFeeDueID { get; set; }

        [DataMember]
        public long? CreditNoteID { get; set; }

        [DataMember]
        public long? CreditNoteFeeTypeMapID { get; set; }


    }
}
