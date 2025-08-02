using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DataFormats", Schema = "setting")]
    public partial class DataFormat
    {
        public DataFormat()
        {
            UserDataFormatMaps = new HashSet<UserDataFormatMap>();
        }

        [Key]
        public int DataFormatID { get; set; }
        public short? DataFormatTypeID { get; set; }
        [StringLength(100)]
        public string Format { get; set; }
        public bool? IsDefaultFormat { get; set; }

        [ForeignKey("DataFormatTypeID")]
        [InverseProperty("DataFormats")]
        public virtual DataFormatType DataFormatType { get; set; }
        [InverseProperty("DataFormat")]
        public virtual ICollection<UserDataFormatMap> UserDataFormatMaps { get; set; }
    }
}
