using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class JobCardMaster
    {
        public JobCardMaster()
        {
            this.JobCardMasterLogs = new List<JobCardMasterLog>();
        }

        public int JobCardMasterID { get; set; }
        public string SystemType { get; set; }
        public string SrNo { get; set; }
        public string CustomerName { get; set; }
        public string Telephone { get; set; }
        public System.DateTime ReceivingDate { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public bool Warranty { get; set; }
        public string InvoiceNo { get; set; }
        public System.DateTime InvoiceDate { get; set; }
        public string Accessories { get; set; }
        public string Problems { get; set; }
        public bool WithData { get; set; }
        public string Solution { get; set; }
        public decimal EstimatedCharges { get; set; }
        public string JobCardStatus { get; set; }
        public short CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public virtual ICollection<JobCardMasterLog> JobCardMasterLogs { get; set; }
    }
}
