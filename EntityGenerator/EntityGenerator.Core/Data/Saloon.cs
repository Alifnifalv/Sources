using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Saloons", Schema = "saloon")]
    public partial class Saloon
    {
        [Key]
        public long SaloonIID { get; set; }
        [StringLength(500)]
        public string SaloonName { get; set; }
        public long? BranchID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("BranchID")]
        [InverseProperty("Saloons")]
        public virtual Branch Branch { get; set; }
    }
}
