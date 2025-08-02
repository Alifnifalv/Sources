using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeJobDescriptionDetail", Schema = "payroll")]
    public partial class EmployeeJobDescriptionDetail
    {
        [Key]
        public long JobDescriptionMapID { get; set; }
        public long? JobDescriptionID { get; set; }
        public string Description { get; set; }

        [ForeignKey("JobDescriptionID")]
        [InverseProperty("EmployeeJobDescriptionDetails")]
        public virtual EmployeeJobDescription JobDescription { get; set; }
    }
}
