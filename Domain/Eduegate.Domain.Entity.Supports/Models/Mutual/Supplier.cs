using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Supports.Models.Mutual
{
    [Table("Suppliers", Schema = "mutual")]
    public partial class Supplier
    {
        public Supplier()
        {
            Tickets = new HashSet<Ticket>();
        }

        [Key]
        public long SupplierIID { get; set; }

        public int? CompanyID { get; set; }

        public long? LoginID { get; set; }

        [StringLength(50)]
        public string SupplierCode { get; set; }

        public long? TitleID { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string VendorCR { get; set; }

        public DateTime? CRExpiry { get; set; }

        [StringLength(255)]
        public string VendorNickName { get; set; }

        [StringLength(255)]
        public string CompanyLocation { get; set; }

        public byte? StatusID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public long? EmployeeID { get; set; }

        public bool? IsMarketPlace { get; set; }

        public long? BranchID { get; set; }

        public long? BlockedBranchID { get; set; }

        public decimal? Profit { get; set; }

        public int? AliasID { get; set; }

        public int? ReturnMethodID { get; set; }

        public int? ReceivingMethodID { get; set; }

        [StringLength(50)]
        public string Telephone { get; set; }

        [StringLength(100)]
        public string SupplierEmail { get; set; }

        [StringLength(500)]
        public string SupplierAddress { get; set; }        
        
        public virtual Employee Employee { get; set; }
        
        public virtual Login Login { get; set; }
        
        public virtual ICollection<Ticket> Tickets { get; set; }        
    }
}