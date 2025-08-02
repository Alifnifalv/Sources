using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("DataFormatTypes", Schema = "setting")]
    public partial class DataFormatType
    {
        public DataFormatType()
        {
            this.DataFormats = new List<DataFormat>();
            this.UserDataFormatMaps = new List<UserDataFormatMap>();
        }

        [Key]
        public short DataFormatTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<DataFormat> DataFormats { get; set; }
        public virtual ICollection<UserDataFormatMap> UserDataFormatMaps { get; set; }
    }
}
