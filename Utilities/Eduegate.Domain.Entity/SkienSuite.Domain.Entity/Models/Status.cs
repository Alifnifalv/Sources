using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Status
    {
        public Status()
        {
            this.ShoppingCartVoucherMaps = new List<ShoppingCartVoucherMap>();
            this.TransactionHeadShoppingCartMaps = new List<TransactionHeadShoppingCartMap>();
            this.StatusesCultureDatas = new List<StatusesCultureData>();
        }

        public byte StatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<ShoppingCartVoucherMap> ShoppingCartVoucherMaps { get; set; }
        public virtual ICollection<TransactionHeadShoppingCartMap> TransactionHeadShoppingCartMaps { get; set; }
        public virtual ICollection<StatusesCultureData> StatusesCultureDatas { get; set; }
    }
}
