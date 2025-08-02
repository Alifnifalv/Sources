using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Casts", Schema = "schools")]
    public partial class Cast
    {
        public Cast()
        {
            Employees = new HashSet<Employee>();
            StudentApplications = new HashSet<StudentApplication>();
            Students = new HashSet<Student>();
        }

        [Key]
        public byte CastID { get; set; }
        [StringLength(50)]
        public string CastDescription { get; set; }
        public byte? RelegionID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }

        [ForeignKey("RelegionID")]
        [InverseProperty("Casts")]
        public virtual Relegion Relegion { get; set; }
        [InverseProperty("Cast")]
        public virtual ICollection<Employee> Employees { get; set; }
        [InverseProperty("Cast")]
        public virtual ICollection<StudentApplication> StudentApplications { get; set; }
        [InverseProperty("Cast")]
        public virtual ICollection<Student> Students { get; set; }
    }
}
