using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Segments", Schema = "marketing")]
    public partial class Segment
    {
        public Segment()
        {
            EmailCampaigns = new HashSet<EmailCampaign>();
            SegmentCustomerMaps = new HashSet<SegmentCustomerMap>();
        }

        [Key]
        public long SegmentIID { get; set; }
        [StringLength(50)]
        public string SegmentName { get; set; }

        [InverseProperty("Segment")]
        public virtual ICollection<EmailCampaign> EmailCampaigns { get; set; }
        [InverseProperty("Segment")]
        public virtual ICollection<SegmentCustomerMap> SegmentCustomerMaps { get; set; }
    }
}
