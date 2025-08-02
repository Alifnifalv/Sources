using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BoilerPlate
    {
        public BoilerPlate()
        {
            this.BoilerPlateParameters = new List<BoilerPlateParameter>();
            this.PageBoilerplateMaps = new List<PageBoilerplateMap>();
        }

        public long BoilerPlateID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Template { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public string ReferenceIDName { get; set; }
        public Nullable<bool> ReferenceIDRequired { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public virtual ICollection<BoilerPlateParameter> BoilerPlateParameters { get; set; }
        public virtual BoilerPlate BoilerPlates1 { get; set; }
        public virtual BoilerPlate BoilerPlate1 { get; set; }
        public virtual ICollection<PageBoilerplateMap> PageBoilerplateMaps { get; set; }
    }
}
