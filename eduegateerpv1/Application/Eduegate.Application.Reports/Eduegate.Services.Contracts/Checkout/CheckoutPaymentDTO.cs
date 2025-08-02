using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Checkout
{
    [DataContract]
    public class CheckoutPaymentDTO
    {
        [DataMember]
        public string ShoppingCartID { get; set; }
        [DataMember]
        public string VoucherNo { get; set; }
        [DataMember]
        public decimal VoucherAmount { get; set; }
        [DataMember]
        public string SelectedPaymentOption { get; set; }
        [DataMember]
        public string SelectedShippingAddress { get; set; }
        [DataMember]
        public string SelectedBillingAddress { get; set; }
        [DataMember]
        public decimal WalletAmount { get; set; }

        [DataMember]
        public int CurrencyID { get; set; }

        [DataMember]
        public string PostObject { get; set; }
        [DataMember]
        public string DevicePlatorm { get; set; }
        [DataMember]
        public string DeviceVersion { get; set; }
        [DataMember]
        public string OrderNote { get; set; }

        [DataMember]
        public long? SelectedStudentID { get; set; }

        [DataMember]
        public int? SelectedStudentAcademicYearID { get; set; }

        [DataMember]
        public byte? SelectedStudentSchoolID { get; set; }
    }
}
