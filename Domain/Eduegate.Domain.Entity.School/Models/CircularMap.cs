using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("CircularMaps", Schema = "schools")]
    public partial class CircularMap
    {
        [Key]
        public long CircularMapIID { get; set; }

        public long? CircularID { get; set; }

        public int? ClassID { get; set; }

        public bool? AllClass { get; set; }

        public int? SectionID { get; set; }

        public bool? AllSection { get; set; }
        public long? DepartmentID { get; set; }
        public bool? AllDepartment { get; set; }
        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Departments1 Departments1 { get; set; }

        public virtual Circular Circular { get; set; }

        public virtual Class Class { get; set; }

        public virtual Section Section { get; set; }
    }
}