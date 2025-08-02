using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CashChanges", Schema = "inventory")]
    public partial class CashChanx
    {
        public CashChanx()
        {
            CashChangeCultureDatas = new HashSet<CashChangeCultureData>();
        }

        [Key]
        public int CashChangeID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }

        [InverseProperty("CashChange")]
        public virtual ICollection<CashChangeCultureData> CashChangeCultureDatas { get; set; }
    }
}
