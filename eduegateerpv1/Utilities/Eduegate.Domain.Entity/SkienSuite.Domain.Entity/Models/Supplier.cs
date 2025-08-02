using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            this.SupplierAccountMaps = new List<SupplierAccountMap>();
            this.TransactionHeads = new List<TransactionHead>();
            this.TransactionShipments = new List<TransactionShipment>();
            this.TransactionShipments1 = new List<TransactionShipment>();
            this.CustomerSupplierMaps = new List<CustomerSupplierMap>();
        }

        public long SupplierIID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<long> LoginID { get; set; }
        public string SupplierCode { get; set; }
        public Nullable<long> TitleID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string VendorCR { get; set; }
        public Nullable<System.DateTime> CRExpiry { get; set; }
        public string VendorNickName { get; set; }
        public string CompanyLocation { get; set; }
        public Nullable<byte> StatusID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<long> EmployeeID { get; set; }
        public Nullable<bool> IsMarketPlace { get; set; }
        public Nullable<long> BranchID { get; set; }
        public Nullable<long> BlockedBranchID { get; set; }
        public Nullable<decimal> Profit { get; set; }
        public Nullable<int> AliasID { get; set; }
        public Nullable<int> ReturnMethodID { get; set; }
        public Nullable<int> ReceivingMethodID { get; set; }
        public virtual ICollection<SupplierAccountMap> SupplierAccountMaps { get; set; }
        public virtual Login Login { get; set; }
        public virtual ReceivingMethod ReceivingMethod { get; set; }
        public virtual ReturnMethod ReturnMethod { get; set; }
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
        public virtual ICollection<TransactionShipment> TransactionShipments { get; set; }
        public virtual ICollection<TransactionShipment> TransactionShipments1 { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<CustomerSupplierMap> CustomerSupplierMaps { get; set; }
        public virtual SupplierStatus SupplierStatus { get; set; }
    }
}
