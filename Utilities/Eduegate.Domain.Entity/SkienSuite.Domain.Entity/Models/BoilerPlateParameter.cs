using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BoilerPlateParameter
    {
        public int BoilerPlateParameterID { get; set; }
        public Nullable<long> BoilerPlateID { get; set; }
        public string ParameterName { get; set; }
        public string Description { get; set; }
        public virtual BoilerPlate BoilerPlate { get; set; }
    }
}
