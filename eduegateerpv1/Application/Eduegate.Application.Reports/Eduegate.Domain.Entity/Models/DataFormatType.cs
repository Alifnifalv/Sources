using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DataFormatType
    {
        public DataFormatType()
        {
            this.DataFormats = new List<DataFormat>();
            this.UserDataFormatMaps = new List<UserDataFormatMap>();
        }

        public short DataFormatTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<DataFormat> DataFormats { get; set; }
        public virtual ICollection<UserDataFormatMap> UserDataFormatMaps { get; set; }
    }
}
