using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("MenuLinkTypes", Schema = "setting")]
    public partial class MenuLinkType
    {
        public MenuLinkType()
        {
            MenuLinks = new HashSet<MenuLink>();
        }

        [Key]
        public byte MenuLinkTypeID { get; set; }
        [StringLength(100)]
        public string MenuLinkTypeName { get; set; }

        [InverseProperty("MenuLinkType")]
        public virtual ICollection<MenuLink> MenuLinks { get; set; }
    }
}
