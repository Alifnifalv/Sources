using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("DataTypes", Schema = "setting")]
    public partial class DataType
    {
        public DataType()
        {
            this.FilterColumnConditionMaps = new List<FilterColumnConditionMap>();
            this.FilterColumns = new List<FilterColumn>();
        }

        [Key]
        public byte DataTypeID { get; set; }
        public string DateTypeName { get; set; }
        public virtual ICollection<FilterColumnConditionMap> FilterColumnConditionMaps { get; set; }
        public virtual ICollection<FilterColumn> FilterColumns { get; set; }
    }
}
