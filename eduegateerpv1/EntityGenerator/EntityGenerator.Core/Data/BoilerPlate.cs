using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BoilerPlates", Schema = "cms")]
    public partial class BoilerPlate
    {
        public BoilerPlate()
        {
            BoilerPlateParameters = new HashSet<BoilerPlateParameter>();
            PageBoilerplateMaps = new HashSet<PageBoilerplateMap>();
            PageBoilerplateReports = new HashSet<PageBoilerplateReport>();
        }

        [Key]
        public long BoilerPlateID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Template { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(50)]
        public string ReferenceIDName { get; set; }
        public bool? ReferenceIDRequired { get; set; }
        public int? CompanyID { get; set; }

        [InverseProperty("BoilerPlate")]
        public virtual ICollection<BoilerPlateParameter> BoilerPlateParameters { get; set; }
        [InverseProperty("Boilerplate")]
        public virtual ICollection<PageBoilerplateMap> PageBoilerplateMaps { get; set; }
        [InverseProperty("BoilerPlate")]
        public virtual ICollection<PageBoilerplateReport> PageBoilerplateReports { get; set; }
    }
}
