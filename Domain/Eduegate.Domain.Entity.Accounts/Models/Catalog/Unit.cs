using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Assets;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Accounts.Models.Catalog
{
    [Table("Units", Schema = "catalog")]
    public partial class Unit
    {
        public Unit()
        {
            Assets = new HashSet<Asset>();
            //InvetoryTransactions = new HashSet<InvetoryTransaction>();
            ProductPurchaseUnits = new HashSet<Product>();
            ProductSellingUnits = new HashSet<Product>();
            ProductUnits = new HashSet<Product>();
            //TransactionDetails = new HashSet<TransactionDetail>();
        }

        [Key]
        public long UnitID { get; set; }

        [StringLength(20)]
        [Unicode(false)]
        public string UnitCode { get; set; }

        [StringLength(50)]
        public string UnitName { get; set; }

        public double? Fraction { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public string ShortName { get; set; }

        public long? UnitGroupID { get; set; }

        //public virtual UnitGroup UnitGroup { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }

        //public virtual ICollection<InvetoryTransaction> InvetoryTransactions { get; set; }

        public virtual ICollection<Product> ProductPurchaseUnits { get; set; }

        public virtual ICollection<Product> ProductSellingUnits { get; set; }

        public virtual ICollection<Product> ProductUnits { get; set; }

        //public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }
    }
}