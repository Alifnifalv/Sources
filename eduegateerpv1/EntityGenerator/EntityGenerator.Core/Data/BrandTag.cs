using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BrandTags", Schema = "catalog")]
    public partial class BrandTag
    {
        public BrandTag()
        {
            BrandTagMaps = new HashSet<BrandTagMap>();
        }

        [Key]
        public long BrandTagIID { get; set; }
        public long? BrandID { get; set; }
        [StringLength(50)]
        public string TagName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("BrandID")]
        [InverseProperty("BrandTags")]
        public virtual Brand Brand { get; set; }
        [InverseProperty("BrandTag")]
        public virtual ICollection<BrandTagMap> BrandTagMaps { get; set; }
    }
}
