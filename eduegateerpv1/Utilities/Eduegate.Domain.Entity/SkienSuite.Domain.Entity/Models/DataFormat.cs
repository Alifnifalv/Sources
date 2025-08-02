using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DataFormat
    {
        public DataFormat()
        {
            this.UserDataFormatMaps = new List<UserDataFormatMap>();
        }

        public int DataFormatID { get; set; }
        public Nullable<short> DataFormatTypeID { get; set; }
        public string Format { get; set; }
        public Nullable<bool> IsDefaultFormat { get; set; }
        public virtual DataFormatType DataFormatType { get; set; }
        public virtual ICollection<UserDataFormatMap> UserDataFormatMaps { get; set; }
    }
}
