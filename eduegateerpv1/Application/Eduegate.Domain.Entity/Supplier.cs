namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.Suppliers")]
    public partial class Supplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplier()
        {
            SupplierAccountMaps = new HashSet<SupplierAccountMap>();
            Tickets = new HashSet<Ticket>();
            TransactionHeads = new HashSet<TransactionHead>();
            TransactionShipments = new HashSet<TransactionShipment>();
            TransactionShipments1 = new HashSet<TransactionShipment>();
            CustomerSupplierMaps = new HashSet<CustomerSupplierMap>();
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

        [Column(TypeName = "date")]
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

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplierAccountMap> SupplierAccountMaps { get; set; }

        public virtual Login Login { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Tickets { get; set; }

        public virtual ReceivingMethod ReceivingMethod { get; set; }

        public virtual ReturnMethod ReturnMethod { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionShipment> TransactionShipments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionShipment> TransactionShipments1 { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual Company Company { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerSupplierMap> CustomerSupplierMaps { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual SupplierStatus SupplierStatus { get; set; }
    }
}
