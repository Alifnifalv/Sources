using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class TransportTerm3FeeNotGenerated
    {
        public double? StudentIID { get; set; }
        [StringLength(255)]
        public string AdmissionNumber { get; set; }
        public double? Status { get; set; }
        [StringLength(255)]
        public string StudentName { get; set; }
        [StringLength(255)]
        public string ClassName { get; set; }
        [StringLength(255)]
        public string SectionName { get; set; }
        public double? PickupRoute { get; set; }
        public double? DropRoute { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateFrom { get; set; }
        [StringLength(255)]
        public string CancelDate { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
        public double? AcademicYearID { get; set; }
    }
}
