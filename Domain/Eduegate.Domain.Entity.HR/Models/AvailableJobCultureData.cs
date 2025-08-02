using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.HR.Models
{
    [Table("AvailableJobCultureDatas", Schema = "hr")]
    public partial class AvailableJobCultureData
    {
        [Key]
        public byte CultureID { get; set; }
        public long JobIID { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobDetails { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
