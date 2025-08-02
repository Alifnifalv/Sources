using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ViewFilter
    {
        public ViewFilter()
        {
            this.UserViewFilterMaps = new List<UserViewFilterMap>();
        }

        [Key]
        public long ViewFilterIID { get; set; }
        public Nullable<long> ViewID { get; set; }
        public string PhysicalColumn { get; set; }
        public string ColumnTitle { get; set; }
        public Nullable<byte> UIControlTypeID { get; set; }
        public string DefaultValue { get; set; }
        public Nullable<int> LookupID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual Lookup Lookup { get; set; }
        public virtual UIControlType UIControlType { get; set; }
        public virtual ICollection<UserViewFilterMap> UserViewFilterMaps { get; set; }
        public virtual View View { get; set; }
    }
}
