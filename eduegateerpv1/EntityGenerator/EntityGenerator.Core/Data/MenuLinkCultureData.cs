using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("MenuLinkCultureDatas", Schema = "setting")]
    public partial class MenuLinkCultureData
    {
        [Key]
        public byte CultureID { get; set; }
        [Key]
        public long MenuLinkID { get; set; }
        [StringLength(100)]
        public string MenuName { get; set; }
        [StringLength(50)]
        public string MenuTitle { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public string ActionLink { get; set; }
        public string ActionLink1 { get; set; }

        [ForeignKey("CultureID")]
        [InverseProperty("MenuLinkCultureDatas")]
        public virtual Culture Culture { get; set; }
        [ForeignKey("MenuLinkID")]
        [InverseProperty("MenuLinkCultureDatas")]
        public virtual MenuLink MenuLink { get; set; }
    }
}
