using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BolierPlateSearchView
    {
        public long BoilerPlateID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Template { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<int> CompanyID { get; set; }
    }
}
