using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.HR.Models
{
    [Table("DepartmentTags", Schema = "hr")]
    public partial class DepartmentTag
    {
        [Key]
        public long DepartmentTagIID { get; set; }
        public int? DepartmentID { get; set; }
        public string TagName { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public virtual DB_Department Department { get; set; }
    }
}
