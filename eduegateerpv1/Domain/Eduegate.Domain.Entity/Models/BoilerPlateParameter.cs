using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("BoilerPlateParameters", Schema = "cms")]
    public partial class BoilerPlateParameter
    {
        [Key]
        public int BoilerPlateParameterID { get; set; }
        public Nullable<long> BoilerPlateID { get; set; }
        public string ParameterName { get; set; }
        public string Description { get; set; }
        public virtual BoilerPlate BoilerPlate { get; set; }
    }
}
