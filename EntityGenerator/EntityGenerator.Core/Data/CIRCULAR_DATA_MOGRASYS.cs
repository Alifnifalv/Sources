using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class CIRCULAR_DATA_MOGRASYS
    {
        public byte SchoolID { get; set; }
        [Required]
        [StringLength(4)]
        public string lic_school_code { get; set; }
        [Required]
        [StringLength(50)]
        public string lic_school_name { get; set; }
        [Required]
        [StringLength(8)]
        public string sims_circular_number { get; set; }
        [StringLength(4)]
        public string sims_academic_year { get; set; }
        [StringLength(2)]
        public string sims_cur_code { get; set; }
        [Column(TypeName = "date")]
        public DateTime? sims_circular_date { get; set; }
        [Column(TypeName = "date")]
        public DateTime? sims_circular_publish_date { get; set; }
        [Column(TypeName = "date")]
        public DateTime? sims_circular_expiry_date { get; set; }
        [StringLength(50)]
        public string sims_circular_title { get; set; }
        [StringLength(50)]
        public string sims_circular_short_desc { get; set; }
        [Column(TypeName = "ntext")]
        public string sims_circular_desc { get; set; }
        [StringLength(100)]
        public string sims_circular_file_path1 { get; set; }
        [StringLength(100)]
        public string sims_circular_file_path2 { get; set; }
        [StringLength(100)]
        public string sims_circular_file_path3 { get; set; }
        [StringLength(2)]
        public string sims_circular_type { get; set; }
        [StringLength(2)]
        public string sims_circular_category { get; set; }
        [StringLength(7)]
        public string sims_circular_created_user_code { get; set; }
        [StringLength(2)]
        public string sims_circular_display_order { get; set; }
        [StringLength(1)]
        public string sims_circular_status { get; set; }
        [StringLength(250)]
        public string sims_circular_file_path1_en { get; set; }
        [StringLength(250)]
        public string sims_circular_file_path2_en { get; set; }
        [StringLength(250)]
        public string sims_circular_file_path3_en { get; set; }
        [StringLength(2)]
        public string sims_circular_alert_status { get; set; }
        [StringLength(2)]
        public string sims_circular_email_status { get; set; }
        [StringLength(5)]
        public string sims_dept { get; set; }
        [StringLength(5)]
        public string sims_desg { get; set; }
        [StringLength(2)]
        public string sims_gradecode { get; set; }
        [StringLength(4)]
        public string sims_sectioncode { get; set; }
        public int SectionID { get; set; }
        public int AcademicYearID { get; set; }
        public int ClassID { get; set; }
    }
}
