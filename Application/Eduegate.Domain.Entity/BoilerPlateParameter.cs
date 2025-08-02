namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cms.BoilerPlateParameters")]
    public partial class BoilerPlateParameter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BoilerPlateParameterID { get; set; }

        public long? BoilerPlateID { get; set; }

        [StringLength(50)]
        public string ParameterName { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        public virtual BoilerPlate BoilerPlate { get; set; }
    }
}
