using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class AssetCategory
    {
        public AssetCategory()
        {
            this.Assets = new List<Asset>();
        }

        public long AssetCategoryID { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<Asset> Assets { get; set; }
    }
}
