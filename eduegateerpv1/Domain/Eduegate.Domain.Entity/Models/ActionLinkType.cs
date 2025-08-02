using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ActionLinkTypes", Schema = "mutual")]
    public partial class ActionLinkType
    {
        public ActionLinkType()
        {
            this.Banners = new List<Banner>();
            this.CategoryImageMaps = new List<CategoryImageMap>();
        }

        [Key]
        public int ActionLinkTypeID { get; set; }
        public string ActionLinkTypeName { get; set; }

        public virtual ICollection<Banner> Banners { get; set; }
        public virtual ICollection<CategoryImageMap> CategoryImageMaps { get; set; }
    }
}
