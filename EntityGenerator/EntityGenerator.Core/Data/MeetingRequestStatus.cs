using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("MeetingRequestStatuses", Schema = "signup")]
    public partial class MeetingRequestStatus
    {
        public MeetingRequestStatus()
        {
            MeetingRequests = new HashSet<MeetingRequest>();
        }

        [Key]
        public byte MeetingRequestStatusID { get; set; }
        [StringLength(100)]
        public string RequestStatusName { get; set; }
        public int? StatusOrder { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [InverseProperty("MeetingRequestStatus")]
        public virtual ICollection<MeetingRequest> MeetingRequests { get; set; }
    }
}
