using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Qualifications", Schema = "payroll")]
    public partial class Qualification
    {
        public Qualification()
        {
            EmployeeQualificationMaps = new HashSet<EmployeeQualificationMap>();
        }

        [Key]
        public byte QualificationID { get; set; }
        [StringLength(50)]
        public string QualificationName { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("Qualification")]
        public virtual ICollection<EmployeeQualificationMap> EmployeeQualificationMaps { get; set; }
    }
}
