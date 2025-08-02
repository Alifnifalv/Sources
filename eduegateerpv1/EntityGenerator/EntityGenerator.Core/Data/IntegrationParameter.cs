using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("IntegrationParameters", Schema = "setting")]
    public partial class IntegrationParameter
    {
        [Key]
        public long IntegrationParameterId { get; set; }
        [StringLength(50)]
        public string ParameterType { get; set; }
        [StringLength(200)]
        public string ParameterName { get; set; }
        [StringLength(1000)]
        public string ParameterValue { get; set; }
        [StringLength(15)]
        [Unicode(false)]
        public string ParameterDataType { get; set; }
    }
}
