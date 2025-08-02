using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Basket
    {
        public Basket()
        {
            this.JobEntryHeads = new List<JobEntryHead>();
        }

        public int BasketID { get; set; }
        public string BasketCode { get; set; }
        public string Description { get; set; }
        public string Barcode { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
    }
}
