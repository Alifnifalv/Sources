using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class PageBoilerplateMapParameter
    {
        public PageBoilerplateMapParameter()
        {
            this.PageBoilerplateMapParameterCultureDatas = new List<PageBoilerplateMapParameterCultureData>();
        }
        public long PageBoilerplateMapParameterIID { get; set; }
        public Nullable<long> PageBoilerplateMapID { get; set; }
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual ICollection<PageBoilerplateMapParameterCultureData> PageBoilerplateMapParameterCultureDatas { get; set; }
        public virtual PageBoilerplateMap PageBoilerplateMap { get; set; }
    }
}
