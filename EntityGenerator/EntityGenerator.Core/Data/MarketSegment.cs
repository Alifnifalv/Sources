using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("MarketSegments", Schema = "crm")]
    public partial class MarketSegment
    {
        public MarketSegment()
        {
            Leads = new HashSet<Lead>();
        }

        [Key]
        public byte MarketSegmentID { get; set; }
        [StringLength(50)]
        public string MarketSegmentName { get; set; }

        [InverseProperty("MarketSegment")]
        public virtual ICollection<Lead> Leads { get; set; }
    }
}
