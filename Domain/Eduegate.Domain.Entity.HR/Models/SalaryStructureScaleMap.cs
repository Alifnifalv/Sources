using Eduegate.Domain.Entity.HR.Payroll;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.HR.Models
{
    [Table("SalaryStructureScaleMaps", Schema = "payroll")]
    public partial class SalaryStructureScaleMap
    {
        [Key]
        public long StructureScaleID { get; set; }
        public bool? IsSponsored { get; set; }
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
        public int? AccomodationTypeID { get; set; }
        public string IncrementNote { get; set; }
        public int? MaritalStatusID { get; set; }
        public string LeaveTicket { get; set; }
        public long? SalaryStructureID { get; set; }

        public virtual AccomodationType AccomodationType { get; set; }
        public virtual MaritalStatus MaritalStatus { get; set; }
        public virtual SalaryStructure SalaryStructure { get; set; }
    }
}
