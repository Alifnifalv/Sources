using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
   public class AdditionalExpenseMapDTO
    {
        [DataMember]
        public int AdditionalExpenseID { get; set; }
        [DataMember]
        public string AdditionalExpenseCode { get; set; }
        [DataMember]
        public string AdditionalExpenseName { get; set; }
        [DataMember]
        public long? AccountID { get; set; }
        [DataMember]
        public string ProvisionalAccountID { get; set; }
        [DataMember]
        public int? CreatedBy { get; set; }
        [DataMember]
        public DateTime? CreatedDate { get; set; }
        [DataMember]
        public int? UpdatedBy { get; set; }
        [DataMember]
        public DateTime? UpdatedDate { get; set; }
        [DataMember]
        public byte? CompanyID { get; set; }
    }
}
