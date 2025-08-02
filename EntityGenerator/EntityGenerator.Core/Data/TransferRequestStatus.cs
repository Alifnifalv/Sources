using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TransferRequestStatuses", Schema = "schools")]
    public partial class TransferRequestStatus
    {
        public TransferRequestStatus()
        {
            StudentTransferRequests = new HashSet<StudentTransferRequest>();
        }

        [Key]
        public byte TransferRequestStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }

        [InverseProperty("TransferRequestStatus")]
        public virtual ICollection<StudentTransferRequest> StudentTransferRequests { get; set; }
    }
}
