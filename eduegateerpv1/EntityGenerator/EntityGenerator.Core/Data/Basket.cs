using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Baskets", Schema = "jobs")]
    public partial class Basket
    {
        public Basket()
        {
            JobEntryHeads = new HashSet<JobEntryHead>();
        }

        [Key]
        public int BasketID { get; set; }
        [StringLength(20)]
        public string BasketCode { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Barcode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? CompanyID { get; set; }

        [InverseProperty("Basket")]
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
    }
}
