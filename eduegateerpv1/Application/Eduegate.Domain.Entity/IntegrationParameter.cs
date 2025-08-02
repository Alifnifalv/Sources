namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("setting.IntegrationParameters")]
    public partial class IntegrationParameter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long IntegrationParameterId { get; set; }

        [StringLength(50)]
        public string ParameterType { get; set; }

        [StringLength(200)]
        public string ParameterName { get; set; }

        [StringLength(1000)]
        public string ParameterValue { get; set; }

        [StringLength(15)]
        public string ParameterDataType { get; set; }
    }
}
