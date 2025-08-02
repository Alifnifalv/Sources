using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AssetCategories", Schema = "asset")]
    public partial class AssetCategory
    {
        public AssetCategory()
        {
            Assets = new HashSet<Asset>();
        }

        [Key]
        public long AssetCategoryID { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string CategoryName { get; set; }

        [InverseProperty("AssetCategory")]
        public virtual ICollection<Asset> Assets { get; set; }
    }
}
