using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Students
{   
    public class FeeDueCancellationDetailDTO : BaseMasterDTO
    {
        public FeeDueCancellationDetailDTO()
        {
            FeeMaster = new KeyValueDTO();
            FeePeriod = new KeyValueDTO();           
        }
        [DataMember]
        public long FeeDueCancellationID { get; set; }

        [DataMember]
        public int? FeeMasterID { get; set; }

        [DataMember]
        public int? FeePeriodID { get; set; }

        [DataMember]
        public KeyValueDTO FeeMaster { get; set; }

        [DataMember]
        public KeyValueDTO FeeInvoice { get; set; }      

        [DataMember]
        public KeyValueDTO FeePeriod { get; set; }

        [DataMember]
        public decimal? DueAmount { get; set; }

        [DataMember]
        public long? FeeDueFeeTypeMapsID { get; set; }
        [DataMember]
        public long? StudentFeeDueID { get; set; }

        [DataMember]
        public long? CreditNoteID { get; set; }

        [DataMember]
        public long? CreditNoteFeeTypeMapID { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public bool? IsCancel { get; set; }

        [DataMember]
        public long StudentFeeDueIID { get; set; }

        [DataMember]
        public int? FeePeriodId { get; set; }

        [DataMember]
        public string InvoiceNo { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        //[DataMember]
        //public long? FeeDueFeeTypeMapsIID { get; set; }
    }
}
