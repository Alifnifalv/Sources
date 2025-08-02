using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("EntityPropertyMaps", Schema = "mutual")]
    public class EntityPropertyMap
    {
        [Key]
        public long EntityPropertyMapIID { get; set; }
        public Nullable<int> EntityTypeID { get; set; }
        public Nullable<int> EntityPropertyTypeID { get; set; }
        public Nullable<long> EntityPropertyID { get; set; }
        public Nullable<long> ReferenceID { get; set; }
        public Nullable<short> Sequence { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
    }
}
