using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("TransportCancelRequests", Schema = "schools")]
    public partial class TransportCancelRequest
    {
        [Key]
        public long RequestIID { get; set; }
        public long? StudentRouteStopMapID { get; set; }
        public DateTime? AppliedDate { get; set; }
        public DateTime? ExpectedCancelDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? ApprovedBy { get; set; }
        public string Reason { get; set; }
        public string RemarksBySchool { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? StatusID { get; set; }
        public bool? CancelRequest { get; set; }

        public virtual TransportCancellationStatus Status { get; set; }
        public virtual StudentRouteStopMap StudentRouteStopMap { get; set; }
    }
}
