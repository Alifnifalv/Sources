using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeExperienceDetails", Schema = "payroll")]
    public partial class EmployeeExperienceDetail
    {
        [Key]
        public long EmployeeExperienceDetailIID { get; set; }
        public long? EmployeeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FromDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ToDate { get; set; }
        [StringLength(200)]
        public string NameOfOraganizationtName { get; set; }
        [StringLength(50)]
        public string CurriculamOrIndustry { get; set; }
        [StringLength(50)]
        public string Designation { get; set; }
        [StringLength(200)]
        public string SubjectTaught { get; set; }
        [StringLength(20)]
        public string ClassTaught { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("EmployeeID")]
        [InverseProperty("EmployeeExperienceDetails")]
        public virtual Employee Employee { get; set; }
    }
}
