using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ImageType
    {
        public ImageType()
        {
            this.CategoryImageMaps = new List<CategoryImageMap>();
        }

        public byte ImageTypeID { get; set; }
        public string TypeName { get; set; }
        public virtual ICollection<CategoryImageMap> CategoryImageMaps { get; set; }
    }
}
