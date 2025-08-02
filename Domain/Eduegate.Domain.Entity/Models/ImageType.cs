using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ImageTypes", Schema = "mutual")]
    public partial class ImageType
    {
        public ImageType()
        {
            this.CategoryImageMaps = new List<CategoryImageMap>();
        }

        [Key]
        public byte ImageTypeID { get; set; }
        public string TypeName { get; set; }
        public virtual ICollection<CategoryImageMap> CategoryImageMaps { get; set; }
    }
}
