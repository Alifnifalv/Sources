using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PageBoilerplateReports", Schema = "cms")]
    public partial class PageBoilerplateReport
    {
        [Key]
        public long PageBoilerplateReportID { get; set; }
        public long? BoilerPlateID { get; set; }
        public long? PageID { get; set; }
        [StringLength(50)]
        public string ReportName { get; set; }
        [StringLength(50)]
        public string ReportHeader { get; set; }
        [StringLength(50)]
        public string ParameterName { get; set; }
        public string Remarks { get; set; }

        [ForeignKey("BoilerPlateID")]
        [InverseProperty("PageBoilerplateReports")]
        public virtual BoilerPlate BoilerPlate { get; set; }
        [ForeignKey("PageID")]
        [InverseProperty("PageBoilerplateReports")]
        public virtual Page Page { get; set; }
    }
}
