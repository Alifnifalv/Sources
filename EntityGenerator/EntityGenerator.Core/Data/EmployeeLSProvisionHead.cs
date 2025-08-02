using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeLSProvisionHeads", Schema = "payroll")]
    public partial class EmployeeLSProvisionHead
    {
        public EmployeeLSProvisionHead()
        {
            EmployeeLSProvisionDetails = new HashSet<EmployeeLSProvisionDetail>();
        }

        [Key]
        public long EmployeeLSProvisionHeadIID { get; set; }
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
        public bool? IsaccountPosted { get; set; }
        public bool? IsOpening { get; set; }

        [ForeignKey("BranchID")]
        [InverseProperty("EmployeeLSProvisionHeads")]
        public virtual Branch Branch { get; set; }
        [ForeignKey("DocumentTypeID")]
        [InverseProperty("EmployeeLSProvisionHeads")]
        public virtual DocumentType DocumentType { get; set; }
        [ForeignKey("SalaryComponentID")]
        [InverseProperty("EmployeeLSProvisionHeads")]
        public virtual SalaryComponent SalaryComponent { get; set; }
        [InverseProperty("EmployeeLSProvisionHead")]
        public virtual ICollection<EmployeeLSProvisionDetail> EmployeeLSProvisionDetails { get; set; }
    }
}
