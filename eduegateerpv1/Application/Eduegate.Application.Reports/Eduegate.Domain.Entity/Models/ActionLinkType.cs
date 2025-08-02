using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ActionLinkType
    {
        public ActionLinkType()
        {
            this.Banners = new List<Banner>();
            this.CategoryImageMaps = new List<CategoryImageMap>();
        }
        
        public int ActionLinkTypeID { get; set; }
        public string ActionLinkTypeName { get; set; }

        public virtual ICollection<Banner> Banners { get; set; }
        public virtual ICollection<CategoryImageMap> CategoryImageMaps { get; set; }
    }
}
