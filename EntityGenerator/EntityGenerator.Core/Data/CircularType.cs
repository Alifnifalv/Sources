using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CircularTypes", Schema = "schools")]
    public partial class CircularType
    {
        public CircularType()
        {
            Circulars = new HashSet<Circular>();
        }

        [Key]
        public byte CirculateTypeID { get; set; }
        [StringLength(50)]
        public string TypeName { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [InverseProperty("CircularType")]
        public virtual ICollection<Circular> Circulars { get; set; }
    }
}
