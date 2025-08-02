using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductFamilyCultureData
    {
        [Key]
        public byte CultureID { get; set; }
        public long ProductFamilyID { get; set; }
        public string FamilyName { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual ProductFamily ProductFamily { get; set; }
        public virtual Culture Culture { get; set; }
    }
}
