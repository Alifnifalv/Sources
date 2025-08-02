using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Eduegate.Domain.Entity.HR.Models;
using Eduegate.Domain.Entity.HR.Payroll;
namespace Eduegate.Domain.Entity.HR
{
    [Table("EmployeeESBProvisionHeads", Schema = "payroll")]
    public partial class EmployeeESBProvisionHead
    {
        public EmployeeESBProvisionHead()
        {
            EmployeeESBProvisionDetails = new HashSet<EmployeeESBProvisionDetail>();
        }

        [Key]
        public long EmployeeESBProvisionHeadIID { get; set; }


        public int? DocumentTypeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EntryDate { get; set; }
        [StringLength(25)]
        public string EntryNumber { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? SalaryComponentID { get; set; }
        public long? BranchID { get; set; }
        public bool? IsaccountPosted { get; set; } = false;
        public bool? IsOpening { get; set; }

        [ForeignKey("BranchID")]
        [InverseProperty("EmployeeESBProvisionHeads")]
        public virtual Branch Branch { get; set; }
        [ForeignKey("DocumentTypeID")]
        [InverseProperty("EmployeeESBProvisionHeads")]
        public virtual DocumentType DocumentType { get; set; }
        [ForeignKey("SalaryComponentID")]
        [InverseProperty("EmployeeESBProvisionHeads")]
        public virtual SalaryComponent SalaryComponent { get; set; }
        [InverseProperty("EmployeeESBProvisionHead")]
        public virtual ICollection<EmployeeESBProvisionDetail> EmployeeESBProvisionDetails { get; set; }
    }
}
