using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace Eduegate.Services.Contracts.Accounting.MonthlyClosing
{
    [DataContract]
    public class FeeMismatchedMonthlyClosingDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public FeeMismatchedMonthlyClosingDTO()
        {

        }

        [DataMember]
        public int? FeeTypeID { get; set; }

        [DataMember]
        public string FeeTypeName { get; set; }
        [DataMember]
        public string SchoolName { get; set; }
        [DataMember]
        public string AdmissionNumber { get; set; }
        [DataMember]
        public string StudentName { get; set; }
        [DataMember]
        public string InvoiceNo { get; set; }
        [DataMember]
        public DateTime? InvoiceDate { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public string FeeName { get; set; }
        [DataMember]
        public string Remarks { get; set; }

    }
}

