using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EventAudienceTypes", Schema = "collaboration")]
    public partial class EventAudienceType
    {
        public EventAudienceType()
        {
            EventAudienceMaps = new HashSet<EventAudienceMap>();
        }

        [Key]
        public byte EventAudienceTypeID { get; set; }
        [StringLength(100)]
        public string AudienceTypeName { get; set; }

        [InverseProperty("EventAudienceType")]
        public virtual ICollection<EventAudienceMap> EventAudienceMaps { get; set; }
    }
}
