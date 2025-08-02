using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Accounts.Models.Catalog
{
    [Table("Brands", Schema = "catalog")]
    [Index("BrandCode", Name = "UQ_BrandCode", IsUnique = true)]
    public partial class Brand
    {
        public Brand()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public long BrandIID { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string BrandCode { get; set; }

        [StringLength(50)]
        public string BrandName { get; set; }

        [StringLength(1000)]
        public string Descirption { get; set; }

        [StringLength(300)]
        public string LogoFile { get; set; }

        public long? IsIncludeHomePage { get; set; }

        public byte? StatusID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public long? SEOMetadataID { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}