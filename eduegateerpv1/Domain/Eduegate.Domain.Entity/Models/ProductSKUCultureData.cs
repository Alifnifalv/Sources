using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ProductSKUCultureDatas", Schema = "catalog")]
    public partial class ProductSKUCultureData
    {
        [Key]
        public byte CultureID { get; set; }
        public long ProductSKUMapID { get; set; }
        public string ProductSKUName { get; set; }
        public string ProductSKUDescription { get; set; }
        public string ProductSKUDetails { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        ////public byte[] TimeStamps { get; set; }
        public virtual Culture Culture { get; set; }
        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}
