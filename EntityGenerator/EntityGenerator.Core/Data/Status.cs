using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Statuses", Schema = "mutual")]
    public partial class Status
    {
        public Status()
        {
            StatusesCultureDatas = new HashSet<StatusesCultureData>();
            TransactionHeadShoppingCartMaps = new HashSet<TransactionHeadShoppingCartMap>();
        }

        [Key]
        public byte StatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }

        [InverseProperty("CultureNavigation")]
        public virtual ICollection<StatusesCultureData> StatusesCultureDatas { get; set; }
        [InverseProperty("Status")]
        public virtual ICollection<TransactionHeadShoppingCartMap> TransactionHeadShoppingCartMaps { get; set; }
    }
}
