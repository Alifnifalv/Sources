using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Events", Schema = "collaboration")]
    public partial class Event
    {
        public Event()
        {
            EventAudienceMaps = new HashSet<EventAudienceMap>();
        }

        [Key]
        public long EventIID { get; set; }
        [StringLength(500)]
        public string EventTitle { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        public bool? IsThisAHoliday { get; set; }
        public bool? IsEnableReminders { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("Event")]
        public virtual ICollection<EventAudienceMap> EventAudienceMaps { get; set; }
    }
}
