using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("TransportCancellationStatuses", Schema = "schools")]
    public partial class TransportCancellationStatus
    {
        public TransportCancellationStatus()
        {
            TransportCancelRequests = new HashSet<TransportCancelRequest>();
        }

        [Key]
        public int StatusID { get; set; }

        public string StatusName { get; set; }

        public virtual ICollection<TransportCancelRequest> TransportCancelRequests { get; set; }
    }
}
