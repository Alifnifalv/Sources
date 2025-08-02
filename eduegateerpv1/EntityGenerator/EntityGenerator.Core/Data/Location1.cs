using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Locations", Schema = "mutual")]
    public partial class Location1
    {
        public Location1()
        {
            InverseParentLocation = new HashSet<Location1>();
            Landmarks = new HashSet<Landmark>();
        }

        [Key]
        public int LocationID { get; set; }
        [StringLength(100)]
        public string LocationName { get; set; }
        public int? AreaID { get; set; }
        public int? ParentLocationID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("AreaID")]
        [InverseProperty("Location1")]
        public virtual Area Area { get; set; }
        [ForeignKey("ParentLocationID")]
        [InverseProperty("InverseParentLocation")]
        public virtual Location1 ParentLocation { get; set; }
        [InverseProperty("ParentLocation")]
        public virtual ICollection<Location1> InverseParentLocation { get; set; }
        [InverseProperty("Location")]
        public virtual ICollection<Landmark> Landmarks { get; set; }
    }
}
