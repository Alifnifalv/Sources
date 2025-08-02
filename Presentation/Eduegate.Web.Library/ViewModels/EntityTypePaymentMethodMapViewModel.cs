using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels
{
    public partial class EntityTypePaymentMethodMapViewModel : BaseMasterViewModel
    {
        public long EntityTypePaymentMethodMapID { get; set; }
        public Nullable<short> EntityTypeID { get; set; }
        public Nullable<short> PaymentMethodID { get; set; }
        public Nullable<long> ReferenceID { get; set; }
        public Nullable<long> EntityPropertyID { get; set; }
        public Nullable<int> EntityPropertyTypeID { get; set; }
        public string AccountName { get; set; }
        public string AccountID { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string IBANCode { get; set; }
        public string SWIFTCode { get; set; }
        public string IFSCCode { get; set; }
        public string NameOnCheque { get; set; }
        //public Nullable<int> CreatedBy { get; set; }
        //public Nullable<int> UpdatedBy { get; set; }
        //public Nullable<System.DateTime> CreatedDate { get; set; }
        //public Nullable<System.DateTime> UpdatedDate { get; set; }
        ////public byte[] TimeStamps { get; set; }
    }
}
