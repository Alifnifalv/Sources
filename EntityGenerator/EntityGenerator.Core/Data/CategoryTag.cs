using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CategoryTags", Schema = "catalog")]
    public partial class CategoryTag
    {
        public CategoryTag()
        {
            CategoryTagMaps = new HashSet<CategoryTagMap>();
        }

        [Key]
        public long CategoryTagIID { get; set; }
        public long? CategoryID { get; set; }
        [StringLength(50)]
        public string TagName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("CategoryID")]
        [InverseProperty("CategoryTags")]
        public virtual Category Category { get; set; }
        [InverseProperty("CategoryTag")]
        public virtual ICollection<CategoryTagMap> CategoryTagMaps { get; set; }
    }
}
