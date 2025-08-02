using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class EmployeeBankDetail
    {
        public EmployeeBankDetail()
        {
            this.Employees = new List<Employee>();
        }

        public long EmployeeBankIID { get; set; }
        public string BankName { get; set; }
        public string BankDetails { get; set; }
        public string AccountHolderName { get; set; }
        public string AccountNo { get; set; }
        public string IBAN { get; set; }
        public string SwiftCode { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
