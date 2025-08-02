using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Unit
    {
        public Unit()
        {
            this.Products = new List<Product>();
            this.InvetoryTransactions = new List<InvetoryTransaction>();
        }

        public long UnitID { get; set; }
        public Nullable<long> UnitGroupID { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public Nullable<double> Fraction { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual UnitGroup UnitGroup { get; set; }
        public virtual ICollection<InvetoryTransaction> InvetoryTransactions { get; set; }
    }
}
