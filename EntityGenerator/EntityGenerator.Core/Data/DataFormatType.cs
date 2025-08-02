using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DataFormatTypes", Schema = "setting")]
    public partial class DataFormatType
    {
        public DataFormatType()
        {
            DataFormats = new HashSet<DataFormat>();
            UserDataFormatMaps = new HashSet<UserDataFormatMap>();
        }

        [Key]
        public short DataFormatTypeID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }

        [InverseProperty("DataFormatType")]
        public virtual ICollection<DataFormat> DataFormats { get; set; }
        [InverseProperty("DataFormatType")]
        public virtual ICollection<UserDataFormatMap> UserDataFormatMaps { get; set; }
    }
}
