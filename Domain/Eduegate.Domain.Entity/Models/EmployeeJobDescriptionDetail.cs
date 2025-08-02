using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("EmployeeJobDescriptionDetail", Schema = "payroll")]
    public partial class EmployeeJobDescriptionDetail
    {
        [Key]
        public long JobDescriptionMapID { get; set; }
        public long? JobDescriptionID { get; set; }
        public string Description { get; set; }
        public virtual EmployeeJobDescription JobDescription { get; set; }
    }
}
