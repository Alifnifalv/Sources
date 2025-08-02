using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public class FeeCollectionPendingInvoiceDTO
    {
        [DataMember]
        public bool? IsExternal { get; set; }

        [DataMember]
        public string ReportName { get; set; }
        [DataMember]
        public bool? IsRowCheckBoxDisable { get; set; }

        [DataMember]
        public bool? IsRowSelected { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public string InvoiceNo { get; set; }

        [DataMember]
        public DateTime? InvoiceDate { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }
        [DataMember]
        public decimal? CrDrAmount { get; set; }
        [DataMember]
        public decimal? CollAmount { get; set; }
        [DataMember]
        public decimal? Balance { get; set; }
        [DataMember]
        public long? StudentFeeDueID { get; set; }
        [DataMember]
        public int? FeePeriodID { get; set; }
        [DataMember]
        public int? FeeMasterID { get; set; }
    }
}
