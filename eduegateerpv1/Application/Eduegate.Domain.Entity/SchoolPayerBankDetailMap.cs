namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.SchoolPayerBankDetailMap")]
    public partial class SchoolPayerBankDetailMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchoolPayerBankDetailMap()
        {
            WPSDetails = new HashSet<WPSDetail>();
        }

        [Key]
        public long PayerBankDetailIID { get; set; }

        public byte? SchoolID { get; set; }

        public long? BankID { get; set; }

        public string PayerIBAN { get; set; }

        [StringLength(15)]
        public string PayerBankShortName { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public bool? IsMainOperating { get; set; }

        public virtual Bank Bank { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WPSDetail> WPSDetails { get; set; }

        public virtual School School { get; set; }
    }
}
