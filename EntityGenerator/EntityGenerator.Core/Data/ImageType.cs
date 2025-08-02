using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ImageTypes", Schema = "mutual")]
    public partial class ImageType
    {
        public ImageType()
        {
            CategoryImageMaps = new HashSet<CategoryImageMap>();
        }

        [Key]
        public byte ImageTypeID { get; set; }
        [StringLength(50)]
        public string TypeName { get; set; }

        [InverseProperty("ImageType")]
        public virtual ICollection<CategoryImageMap> CategoryImageMaps { get; set; }
    }
}
