using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FilterColumnConditionMaps", Schema = "setting")]
    public partial class FilterColumnConditionMap
    {
        [Key]
        public long FilterColumnConditionMapID { get; set; }
        public byte? DataTypeID { get; set; }
        public long? FilterColumnID { get; set; }
        public byte? ConidtionID { get; set; }

        [ForeignKey("ConidtionID")]
        [InverseProperty("FilterColumnConditionMaps")]
        public virtual Condition Conidtion { get; set; }
        [ForeignKey("DataTypeID")]
        [InverseProperty("FilterColumnConditionMaps")]
        public virtual DataType DataType { get; set; }
        [ForeignKey("FilterColumnID")]
        [InverseProperty("FilterColumnConditionMaps")]
        public virtual FilterColumn FilterColumn { get; set; }
    }
}
