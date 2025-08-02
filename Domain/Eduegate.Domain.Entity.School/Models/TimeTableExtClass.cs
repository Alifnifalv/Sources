using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("TimeTableExtClasses", Schema = "schools")]
    public partial class TimeTableExtClass
    {
        [Key]
        public long TimeTableExtClassIID { get; set; }

        public long TimeTableExtID { get; set; }

        public int ClassID { get; set; }

        public bool? IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Class Class { get; set; }

        public virtual TimeTableExtension TimeTableExt { get; set; }
    }
}
