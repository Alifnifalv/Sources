namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("setting.FilterColumnConditionMaps")]
    public partial class FilterColumnConditionMap
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long FilterColumnConditionMapID { get; set; }

        public byte? DataTypeID { get; set; }

        public long? FilterColumnID { get; set; }

        public byte? ConidtionID { get; set; }

        public virtual Condition Condition { get; set; }

        public virtual DataType DataType { get; set; }

        public virtual FilterColumn FilterColumn { get; set; }
    }
}
