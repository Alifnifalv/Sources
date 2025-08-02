using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CircularPriorities", Schema = "schools")]
    public partial class CircularPriority
    {
        public CircularPriority()
        {
            Circulars = new HashSet<Circular>();
        }

        [Key]
        public byte CircularPriorityID { get; set; }
        [StringLength(50)]
        public string PriorityName { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [InverseProperty("CircularPriority")]
        public virtual ICollection<Circular> Circulars { get; set; }
    }
}
