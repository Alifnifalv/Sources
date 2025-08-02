using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SupplierContentIDs", Schema = "mutual")]
    public partial class SupplierContentID
    {
        [Key]
        [Column("SupplierContentID")]
        public long SupplierContentID1 { get; set; }
        public long? SupplierID { get; set; }
        public long? LetterConfirmationFromBank { get; set; }
        public long? LatestAuditedFinancialStatements { get; set; }
        public long? LiabilityInsurance { get; set; }
        public long? WorkersCompensationInsurance { get; set; }
        public long? PrdctCategories { get; set; }
        public long? ISO9001 { get; set; }
        public long? OtherRelevantISOCertifications { get; set; }
        public long? ISO14001 { get; set; }
        public long? OtherEnviStandards { get; set; }
        public long? SA8000 { get; set; }
        public long? OtherSocialRespoStandards { get; set; }
        public long? OHSAS18001 { get; set; }
        public long? OtherRelevantHealthSafetyStandards { get; set; }
        public long? BusinessRegistration { get; set; }
        public long? TaxIdentificationNumber { get; set; }
        public long? DUNSNumberUpload { get; set; }
        public long? TradeLicense { get; set; }
        public long? EstablishmentLicense { get; set; }

        [ForeignKey("SupplierID")]
        [InverseProperty("SupplierContentIDs")]
        public virtual Supplier Supplier { get; set; }
    }
}
