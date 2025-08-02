using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Eduegate.Domain.Entity.Setting.Models
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
        public string ParameterDataType { get; set; }
    }
}
