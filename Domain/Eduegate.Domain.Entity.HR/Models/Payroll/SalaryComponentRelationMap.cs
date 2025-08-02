namespace Eduegate.Domain.Entity.HR.Payroll
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SalaryComponentRelationMaps", Schema = "payroll")]
    public partial class SalaryComponentRelationMap
    {
        [Key]
        public long SalaryComponentRelationMapIID { get; set; }

        public int? SalaryComponentID { get; set; }

        public int? RelatedComponentID { get; set; }

        public short? RelationTypeID { get; set; }

        public int? NoOfDaysApplicable { get; set; }

        public virtual SalaryComponent SalaryComponent { get; set; }

        public virtual SalaryComponent SalaryComponent1 { get; set; }

        public virtual SalaryComponentRelationType SalaryComponentRelationType { get; set; }
    }
}
