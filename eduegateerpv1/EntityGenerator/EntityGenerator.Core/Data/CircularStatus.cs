using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CircularStatuses", Schema = "schools")]
    public partial class CircularStatus
    {
        public CircularStatus()
        {
            Circulars = new HashSet<Circular>();
        }

        [Key]
        public byte CircularStatusID { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [InverseProperty("CircularStatus")]
        public virtual ICollection<Circular> Circulars { get; set; }
    }
}
