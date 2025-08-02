using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("FilterColumnConditionMaps", Schema = "setting")]
    public partial class FilterColumnConditionMap
    {
        [Key]
        public long FilterColumnConditionMapID { get; set; }
        public Nullable<byte> DataTypeID { get; set; }
        public Nullable<long> FilterColumnID { get; set; }
        public Nullable<byte> ConidtionID { get; set; }
        public virtual Condition Condition { get; set; }
        public virtual DataType DataType { get; set; }
        public virtual FilterColumn FilterColumn { get; set; }
    }
}
