using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("CategoryCultureDatas", Schema = "catalog")]
    public partial class CategoryCultureData
    {
        //[Key]
        public byte CultureID { get; set; }
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual Category Category { get; set; }
        public virtual Culture Culture { get; set; }
    }
}
