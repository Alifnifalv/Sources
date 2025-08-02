using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class PageBoilerplateMap
    {
        public PageBoilerplateMap()
        {
            this.CategoryPageBoilerPlatMaps = new List<CategoryPageBoilerPlatMap>();
            this.PageBoilerplateMapParameters = new List<PageBoilerplateMapParameter>();
        }

        public long PageBoilerplateMapIID { get; set; }
        public Nullable<long> PageID { get; set; }
        public Nullable<long> ReferenceID { get; set; }
        public Nullable<long> BoilerplateID { get; set; }
        public Nullable<int> SerialNumber { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual BoilerPlate BoilerPlate { get; set; }
        public virtual ICollection<CategoryPageBoilerPlatMap> CategoryPageBoilerPlatMaps { get; set; }
        public virtual ICollection<PageBoilerplateMapParameter> PageBoilerplateMapParameters { get; set; }
        public virtual Page Page { get; set; }
    }
}
