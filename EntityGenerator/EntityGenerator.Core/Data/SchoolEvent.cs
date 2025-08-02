using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SchoolEvents", Schema = "schools")]
    public partial class SchoolEvent
    {
        public SchoolEvent()
        {
            EventTransportAllocations = new HashSet<EventTransportAllocation>();
        }

        [Key]
        public long SchoolEventIID { get; set; }
        [StringLength(500)]
        public string EventName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EventDate { get; set; }
        public string Description { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        [StringLength(1000)]
        public string Destination { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("Event")]
        public virtual ICollection<EventTransportAllocation> EventTransportAllocations { get; set; }
    }
}
