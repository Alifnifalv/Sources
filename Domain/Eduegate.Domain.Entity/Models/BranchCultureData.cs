using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("BranchCultureDatas", Schema = "mutual")]
    public partial class BranchCultureData
    {
        [Key]
        public byte CultureID { get; set; }

        [Key]
        public long BranchID { get; set; }

        [StringLength(255)]
        public string BranchName { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual Culture Culture { get; set; }
    }
}
