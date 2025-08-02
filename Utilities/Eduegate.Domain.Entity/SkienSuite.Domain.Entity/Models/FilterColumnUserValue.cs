using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class FilterColumnUserValue
    {
        public long FilterColumnUserValueIID { get; set; }
        public Nullable<long> ViewID { get; set; }
        public Nullable<long> LoginID { get; set; }
        public Nullable<long> FilterColumnID { get; set; }
        public Nullable<byte> ConditionID { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public virtual Login Login { get; set; }
        public virtual Condition Condition { get; set; }
        public virtual FilterColumn FilterColumn { get; set; }
        public virtual View View { get; set; }
    }
}
