using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Vendor
{
    public class PurchaseQuotationListDTO
    {
        public PurchaseQuotationListDTO() {
            DetailsDTO = new List<PurchaseQuotationListDetailDTO>();
        }

        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public string QuotationNo { get; set; }
        [DataMember]
        public string FromSchool { get; set; }
        [DataMember]
        public string Validity { get; set; }
        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public bool IsEditable { get; set; }

        [DataMember]
        public decimal? TotalQuantity { get; set; }
        [DataMember]
        public decimal? TotalPrice { get; set; }
        [DataMember]
        public decimal? TotalAmount { get; set; }       

        [DataMember]
        public List<PurchaseQuotationListDetailDTO> DetailsDTO { get; set; }
    }
}
