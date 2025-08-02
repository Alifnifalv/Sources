using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeDepartmentAccountMaps", Schema = "payroll")]
    public partial class EmployeeDepartmentAccountMap
    {
        [Key]
        public long EmployeeDepartmentAccountMaplIID { get; set; }
        public int? SalaryComponentID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public long? StaffLedgerAccountID { get; set; }
        public long? ProvisionLedgerAccountID { get; set; }
        public long? ExpenseLedgerAccountID { get; set; }
        public long? TaxLedgerAccountID { get; set; }
        public long? DepartMentID { get; set; }

        [ForeignKey("DepartMentID")]
        [InverseProperty("EmployeeDepartmentAccountMaps")]
        public virtual Department1 DepartMent { get; set; }
        [ForeignKey("ExpenseLedgerAccountID")]
        [InverseProperty("EmployeeDepartmentAccountMapExpenseLedgerAccounts")]
        public virtual Account ExpenseLedgerAccount { get; set; }
        [ForeignKey("ProvisionLedgerAccountID")]
        [InverseProperty("EmployeeDepartmentAccountMapProvisionLedgerAccounts")]
        public virtual Account ProvisionLedgerAccount { get; set; }
        [ForeignKey("SalaryComponentID")]
        [InverseProperty("EmployeeDepartmentAccountMaps")]
        public virtual SalaryComponent SalaryComponent { get; set; }
        [ForeignKey("StaffLedgerAccountID")]
        [InverseProperty("EmployeeDepartmentAccountMapStaffLedgerAccounts")]
        public virtual Account StaffLedgerAccount { get; set; }
        [ForeignKey("TaxLedgerAccountID")]
        [InverseProperty("EmployeeDepartmentAccountMapTaxLedgerAccounts")]
        public virtual Account TaxLedgerAccount { get; set; }
    }
}
