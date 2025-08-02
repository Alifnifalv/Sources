using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("CustomerProductReferences", Schema = "catalog")]
    public partial class CustomerProductReference
    {
        [Key]
        public long CustomerProductReferenceIID { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public Nullable<long> ProductSKUMapID { get; set; }
        public string BarCode { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}
