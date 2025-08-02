namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.StudentFeeRelationMaps")]
    public partial class StudentFeeRelationMap
    {
        [Key]
        public long SFRM_IID { get; set; }

        [StringLength(100)]
        public string SFRM_STable { get; set; }

        [StringLength(100)]
        public string SFRM_SColumn { get; set; }

        public long? SFRM_SValue { get; set; }

        public int? SFRM_SSchoolID { get; set; }

        public int? SFRM_SAcademicYearID { get; set; }

        public int? SFRM_SClassID { get; set; }

        public int? SFRM_SSectionID { get; set; }

        [StringLength(100)]
        public string SFRM_DTable { get; set; }

        [StringLength(100)]
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
