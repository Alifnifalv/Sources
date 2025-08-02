using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FeePeriodMIssing_Term3Transport
    {
        public int? StudentIID { get; set; }
        [StringLength(5)]
        public string AdmissionNumber { get; set; }
        public int? Status { get; set; }
        [StringLength(47)]
        public string StudentName { get; set; }
        [StringLength(20)]
        public string ClassName { get; set; }
        [StringLength(2)]
        public string SectionName { get; set; }
        [StringLength(4)]
        public string PickupRoute { get; set; }
        public int? DropRoute { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateFrom { get; set; }
        [StringLength(4)]
        public string CancelDate { get; set; }
        [StringLength(74)]
        public string Remarks { get; set; }
    }
}
