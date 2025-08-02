using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentFeeRelationMaps", Schema = "mutual")]
    [Index("SFRM_STable", "SFRM_SColumn", "SFRM_SValue", "SFRM_DTable", "SFRM_DColumn", "StudentID", "FeeMasterID", Name = "IDX_StudentFeeRelationMaps_SFRM_STable__SFRM_SColumn__SFRM_SValue__SFRM_DTable__SFRM_DColumn__Stude")]
    [Index("SFRM_STable", "SFRM_SColumn", "SFRM_SValue", "SFRM_SSchoolID", "SFRM_SAcademicYearID", "SFRM_SClassID", "SFRM_SSectionID", "StudentID", "FeeMasterID", Name = "IDX_StudentFeeRelationMaps_SFRM_STable__SFRM_SColumn__SFRM_SValue__SFRM_SSchoolID__SFRM_SAcademicYe")]
    public partial class StudentFeeRelationMap
    {
        [Key]
        public long SFRM_IID { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string SFRM_STable { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string SFRM_SColumn { get; set; }
        public long? SFRM_SValue { get; set; }
        public int? SFRM_SSchoolID { get; set; }
        public int? SFRM_SAcademicYearID { get; set; }
        public int? SFRM_SClassID { get; set; }
        public int? SFRM_SSectionID { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string SFRM_DTable { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string SFRM_DColumn { get; set; }
        public long? SFRM_DValue { get; set; }
        public long? SFRM_DSchoolID { get; set; }
        public int? SFRM_DAcademicYearID { get; set; }
        public int? SFRM_DClassID { get; set; }
        public int? SFRM_DSectionID { get; set; }
        public long? StudentID { get; set; }
        public int? FeeMasterID { get; set; }
    }
}
