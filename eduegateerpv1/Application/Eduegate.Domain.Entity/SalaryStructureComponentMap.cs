namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payroll.SalaryStructureComponentMaps")]
    public partial class SalaryStructureComponentMap
    {
        [Key]
        public long SalaryStructureComponentMapIID { get; set; }

        public int? SalaryComponentID { get; set; }

        public long? SalaryStructureID { get; set; }

        public decimal? Amount { get; set; }

        [StringLength(1000)]
        public string Formula { get; set; }

        public decimal? MinAmount { get; set; }

        public decimal? MaxAmount { get; set; }

        public virtual SalaryComponent SalaryComponent { get; set; }

        public virtual SalaryStructure SalaryStructure { get; set; }
    }
}
