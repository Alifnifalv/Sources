using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class EventTransportAllocationView
    {
        public long EventTransportAllocationIID { get; set; }
        [StringLength(500)]
        public string EventName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EventDate { get; set; }
        [StringLength(554)]
        public string Driver { get; set; }
        [Required]
        [StringLength(6)]
        [Unicode(false)]
        public string Type { get; set; }
        [StringLength(50)]
        public string Vehicle { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
    }
}
