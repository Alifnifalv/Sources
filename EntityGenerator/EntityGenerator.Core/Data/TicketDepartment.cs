using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TicketDepartments", Schema = "cs")]
    public partial class TicketDepartment
    {
        [Key]
        public long TicketDepartmentID { get; set; }
        public long? DepartmentID { get; set; }
        [StringLength(100)]
        public string SupportEmailID { get; set; }
        public int? SortOrder { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("DepartmentID")]
        [InverseProperty("TicketDepartments")]
        public virtual Department1 Department { get; set; }
    }
}
