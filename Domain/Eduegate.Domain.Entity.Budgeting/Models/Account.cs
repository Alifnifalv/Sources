using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Budgeting.Models
{
    [Table("Accounts", Schema = "account")]
    public partial class Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            InverseParentAccount = new HashSet<Account>();
            //EmployeeSalaryStructures = new HashSet<EmployeeSalaryStructure>();
            BudgetEntryAccountMaps = new HashSet<BudgetEntryAccountMap>();
        }

        [Key]

        public long AccountID { get; set; }

        [StringLength(30)]
        public string Alias { get; set; }

        [StringLength(50)]
        public string AccountName { get; set; }

        public long? ParentAccountID { get; set; }

        public int? GroupID { get; set; }

        public byte? AccountBehavoirID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        [StringLength(100)]
        public string ChildAliasPrefix { get; set; }

        public long? ChildLastID { get; set; }

        [StringLength(50)]
        public string ExternalReferenceID { get; set; }

        [StringLength(30)]
        public string AccountCode { get; set; }

        [StringLength(500)]
        public string AccountAddress { get; set; }

        [StringLength(50)]
        public string TaxRegistrationNum { get; set; }

        public bool? IsEnableSubLedger { get; set; }

        public virtual ICollection<BudgetEntryAccountMap> BudgetEntryAccountMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Account> InverseParentAccount { get; set; }

        public virtual Account ParentAccount { get; set; }

        public virtual Group Group { get; set; }

    }
}