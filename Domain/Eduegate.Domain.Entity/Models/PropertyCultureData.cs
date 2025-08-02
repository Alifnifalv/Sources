using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("PropertyCultureDatas", Schema = "catalog")]
    public partial class PropertyCultureData
    {
        [Key]
        public byte CultureID { get; set; }
        public long PropertyID { get; set; }
        public string PropertyName { get; set; }
        public string PropertyDescription { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual Property Property { get; set; }
        public virtual Culture Culture { get; set; }
    }
}
