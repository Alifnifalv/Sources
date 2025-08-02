using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("Sequences", Schema = "setting")]
    public partial class Sequence
    {
        public Sequence()
        {
            //ScreenFieldSettings = new HashSet<ScreenFieldSetting>();
        }

        [Key]
        public int SequenceID { get; set; }

        [StringLength(50)]
        public string SequenceType { get; set; }

        [StringLength(15)]
        public string Prefix { get; set; }

        [StringLength(50)]
        public string Format { get; set; }

        public long? LastSequence { get; set; }

        public bool? IsAuto { get; set; }

        public int? ZeroPadding { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }


        //public virtual ICollection<ScreenFieldSetting> ScreenFieldSettings { get; set; }
    }
}
