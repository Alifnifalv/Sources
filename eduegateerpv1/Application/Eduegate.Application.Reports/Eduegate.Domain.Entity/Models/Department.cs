using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Department
    {
        public Department()
        {
            this.Employees = new List<Employee>();
            this.AccountTransactionDetails = new HashSet<AccountTransactionDetail>();
            this.DepartmentCostCenterMaps = new HashSet<DepartmentCostCenterMap>();
        }

        public long DepartmentID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<byte> StatusID { get; set; }
        public string DepartmentName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual DepartmentStatus DepartmentStatuses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountTransactionDetail> AccountTransactionDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DepartmentCostCenterMap> DepartmentCostCenterMaps { get; set; }
    }
}
