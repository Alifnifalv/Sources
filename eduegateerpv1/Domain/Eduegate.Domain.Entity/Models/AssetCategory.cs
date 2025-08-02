using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("AssetCategories", Schema = "asset")]
    public partial class AssetCategory
    {
        public AssetCategory()
        {
            this.Assets = new List<Asset>();
        }

        [Key]
        public long AssetCategoryID { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<Asset> Assets { get; set; }
    }
}
