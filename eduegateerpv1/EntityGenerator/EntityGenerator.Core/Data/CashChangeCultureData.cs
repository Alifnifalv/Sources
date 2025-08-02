using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CashChangeCultureDatas", Schema = "inventory")]
    public partial class CashChangeCultureData
    {
        [Key]
        public int CashChangeID { get; set; }
        [Key]
        public byte CultureID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }

        [ForeignKey("CashChangeID")]
        [InverseProperty("CashChangeCultureDatas")]
        public virtual CashChanx CashChange { get; set; }
        [ForeignKey("CultureID")]
        [InverseProperty("CashChangeCultureDatas")]
        public virtual Culture Culture { get; set; }
    }
}
