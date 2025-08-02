using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ServiceProviders", Schema = "distribution")]
    public partial class ServiceProvider
    {
        public ServiceProvider()
        {
            this.ServiceProviderLogs = new List<ServiceProviderLog>();
            this.DeliveryCharges = new List<DeliveryCharge>();
            this.JobEntryHeads = new List<JobEntryHead>();
        }

        [Key]
        public int ServiceProviderID { get; set; }
        public string ProviderCode { get; set; }
        public string ProviderName { get; set; }
        public Nullable<int> CountryID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string ServiceProviderLink { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual ICollection<ServiceProviderLog> ServiceProviderLogs { get; set; }
        public virtual ICollection<DeliveryCharge> DeliveryCharges { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
    }
}
