using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class VehicleDetailReportView
    {
        [StringLength(50)]
        public string VehicleRegistrationNumber { get; set; }
        [StringLength(20)]
        public string VehicleCode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PurchaseDate { get; set; }
        [StringLength(50)]
        public string ModelName { get; set; }
        public int? YearMade { get; set; }
        public int? VehicleAge { get; set; }
        [StringLength(50)]
        public string Expr1 { get; set; }
        [StringLength(50)]
        public string ManufactureName { get; set; }
        [StringLength(50)]
        public string Color { get; set; }
        public int? AllowSeatingCapacity { get; set; }
        public int? MaximumSeatingCapacity { get; set; }
        public bool? IsActive { get; set; }
        [StringLength(50)]
        public string VehicleTypeName { get; set; }
        [StringLength(50)]
        public string OwnershipTypeName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RegistrationDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RegistrationExpiryDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsuranceIssueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsuranceExpiryDate { get; set; }
    }
}
