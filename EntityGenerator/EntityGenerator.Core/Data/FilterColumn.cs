using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FilterColumns", Schema = "setting")]
    [Index("ViewID", Name = "IDX_FilterColumns_ViewID")]
    public partial class FilterColumn
    {
        public FilterColumn()
        {
            FilterColumnConditionMaps = new HashSet<FilterColumnConditionMap>();
            FilterColumnCultureDatas = new HashSet<FilterColumnCultureData>();
            FilterColumnUserValues = new HashSet<FilterColumnUserValue>();
        }

        [Key]
        public long FilterColumnID { get; set; }
        public int? SequenceNo { get; set; }
        public long? ViewID { get; set; }
        [StringLength(50)]
        public string ColumnCaption { get; set; }
        [StringLength(50)]
        public string ColumnName { get; set; }
        public byte? DataTypeID { get; set; }
        public byte? UIControlTypeID { get; set; }
        public string DefaultValues { get; set; }
        public bool? IsQuickFilter { get; set; }
        public int? LookupID { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public bool? IsLookupLazyLoad { get; set; }

        [ForeignKey("DataTypeID")]
        [InverseProperty("FilterColumns")]
        public virtual DataType DataType { get; set; }
        [ForeignKey("UIControlTypeID")]
        [InverseProperty("FilterColumns")]
        public virtual UIControlType UIControlType { get; set; }
        [ForeignKey("ViewID")]
        [InverseProperty("FilterColumns")]
        public virtual View View { get; set; }
        [InverseProperty("FilterColumn")]
        public virtual ICollection<FilterColumnConditionMap> FilterColumnConditionMaps { get; set; }
        [InverseProperty("FilterColumn")]
        public virtual ICollection<FilterColumnCultureData> FilterColumnCultureDatas { get; set; }
        [InverseProperty("FilterColumn")]
        public virtual ICollection<FilterColumnUserValue> FilterColumnUserValues { get; set; }
    }
}
