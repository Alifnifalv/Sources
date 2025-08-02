using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SalaryComponentRelationTypes", Schema = "payroll")]
    public partial class SalaryComponentRelationType
    {
        public SalaryComponentRelationType()
        {
            SalaryComponentRelationMaps = new HashSet<SalaryComponentRelationMap>();
        }

        [Key]
        public short RelationTypeID { get; set; }
        [StringLength(100)]
        public string RelationName { get; set; }

        [InverseProperty("RelationType")]
        public virtual ICollection<SalaryComponentRelationMap> SalaryComponentRelationMaps { get; set; }
    }
}
