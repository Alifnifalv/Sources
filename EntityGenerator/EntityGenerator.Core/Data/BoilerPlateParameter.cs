using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BoilerPlateParameters", Schema = "cms")]
    public partial class BoilerPlateParameter
    {
        [Key]
        public int BoilerPlateParameterID { get; set; }
        public long? BoilerPlateID { get; set; }
        [StringLength(50)]
        public string ParameterName { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }

        [ForeignKey("BoilerPlateID")]
        [InverseProperty("BoilerPlateParameters")]
        public virtual BoilerPlate BoilerPlate { get; set; }
    }
}
