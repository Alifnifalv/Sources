using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Landmarks", Schema = "mutual")]
    public partial class Landmark
    {
        [Key]
        public int LandmarkID { get; set; }
        [StringLength(500)]
        public string LandmarkDescription { get; set; }
        public int? LocationID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("LocationID")]
        [InverseProperty("Landmarks")]
        public virtual Location1 Location { get; set; }
    }
}
