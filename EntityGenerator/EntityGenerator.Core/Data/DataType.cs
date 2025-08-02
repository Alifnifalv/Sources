using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DataTypes", Schema = "setting")]
    public partial class DataType
    {
        public DataType()
        {
            FilterColumnConditionMaps = new HashSet<FilterColumnConditionMap>();
            FilterColumns = new HashSet<FilterColumn>();
        }

        [Key]
        public byte DataTypeID { get; set; }
        [StringLength(50)]
        public string DateTypeName { get; set; }

        [InverseProperty("DataType")]
        public virtual ICollection<FilterColumnConditionMap> FilterColumnConditionMaps { get; set; }
        [InverseProperty("DataType")]
        public virtual ICollection<FilterColumn> FilterColumns { get; set; }
    }
}
