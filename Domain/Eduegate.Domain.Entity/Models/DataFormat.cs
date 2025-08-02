using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("DataFormats", Schema = "setting")]
    public partial class DataFormat
    {
        public DataFormat()
        {
            this.UserDataFormatMaps = new List<UserDataFormatMap>();
        }

        [Key]
        public int DataFormatID { get; set; }
        public Nullable<short> DataFormatTypeID { get; set; }
        public string Format { get; set; }
        public Nullable<bool> IsDefaultFormat { get; set; }
        public virtual DataFormatType DataFormatType { get; set; }
        public virtual ICollection<UserDataFormatMap> UserDataFormatMaps { get; set; }
    }
}
