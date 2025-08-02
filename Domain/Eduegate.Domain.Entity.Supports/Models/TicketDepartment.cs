using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Supports.Models.Mutual;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Supports.Models
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

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Department Department { get; set; }
    }
}