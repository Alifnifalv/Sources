using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("JobDescriptionDetail", Schema = "payroll")]
    public partial class JobDescriptionDetail
    {
        [Key]
        public long JDMapID { get; set; }
        public long? JDMasterID { get; set; }
        public string Description { get; set; }
        public virtual JobDescription JDMaster { get; set; }
    }
}
