using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("BrandImageMap", Schema = "catalog")]
    public partial class BrandImageMap
    {
        [Key]
        public long BrandImageMapIID { get; set; }
        public Nullable<long> BrandID { get; set; }
        public Nullable<byte> ImageTypeID { get; set; }
        public string ImageFile { get; set; }
        public string ImageTitle { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual Brand Brand { get; set; }
    }
}
