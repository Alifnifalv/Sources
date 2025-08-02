using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SalaryComponentRelationMaps", Schema = "payroll")]
    public partial class SalaryComponentRelationMap
    {
        [Key]
        public long SalaryComponentRelationMapIID { get; set; }
        public int? SalaryComponentID { get; set; }
        public int? RelatedComponentID { get; set; }
        public short? RelationTypeID { get; set; }
        public int? NoOfDaysApplicable { get; set; }

        [ForeignKey("RelatedComponentID")]
        [InverseProperty("SalaryComponentRelationMapRelatedComponents")]
        public virtual SalaryComponent RelatedComponent { get; set; }
        [ForeignKey("RelationTypeID")]
        [InverseProperty("SalaryComponentRelationMaps")]
        public virtual SalaryComponentRelationType RelationType { get; set; }
        [ForeignKey("SalaryComponentID")]
        [InverseProperty("SalaryComponentRelationMapSalaryComponents")]
        public virtual SalaryComponent SalaryComponent { get; set; }
    }
}
