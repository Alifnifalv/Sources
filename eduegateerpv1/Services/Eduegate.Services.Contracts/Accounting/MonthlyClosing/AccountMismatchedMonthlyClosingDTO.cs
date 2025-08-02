using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace Eduegate.Services.Contracts.Accounting.MonthlyClosing
{
    [DataContract]
    public class AccountMismatchedMonthlyClosingDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public AccountMismatchedMonthlyClosingDTO()
        {

        }

        [DataMember]
        public int? TranTypeID { get; set; }

        [DataMember]
        public string TranTypeName { get; set; }
        [DataMember]
        public int? DocumentTypeID { get; set; }

        [DataMember]
        public string DocumentTypeName { get; set; }
        [DataMember]
        public string Branch { get; set; }
        [DataMember]
        public string TranNo { get; set; }
        [DataMember]
        public DateTime? TranDate { get; set; }
        [DataMember]
        public string InvoiceNo { get; set; }
        [DataMember]
        public DateTime? InvoiceDate { get; set; }

        [DataMember]
        public string Narration { get; set; }

        [DataMember]
        public string VoucherNo { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public string FeeName { get; set; }
        [DataMember]
        public string Remarks { get; set; }

    }
}

