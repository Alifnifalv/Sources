using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentGroupFeeMastersSearchView
    {
        public long StudentGroupFeeMasterIID { get; set; }
        public int? StudentGroupID { get; set; }
        [StringLength(50)]
        public string StudentGroupName { get; set; }
        public int? AcadamicYearID { get; set; }
        [StringLength(20)]
        public string SchoolAcadamicYear { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
    }
}
