using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.ShoppingCart
{

   [DataContract]
   public  class AddToCartStatusDTO
    {

       [DataMember]
       public long SKUID { get; set; }

        [DataMember]
        public bool Status { get; set; }

        [DataMember]
        public string CartMessage { get; set; }

        [DataMember]
        public long BranchID { get; set; }

        [DataMember]
        public decimal ProductDiscountPrice { get; set; }

    }
}
