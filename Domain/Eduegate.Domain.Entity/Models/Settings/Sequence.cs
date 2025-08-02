namespace Eduegate.Domain.Entity.Models.Settings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Sequences", Schema = "setting")]
    public partial class Sequence
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sequence()
        {
            ScreenFieldSettings = new HashSet<ScreenFieldSetting>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int SequenceID { get; set; }

        [StringLength(50)]
        public string SequenceType { get; set; }

        [StringLength(10)]
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

        public byte? SchoolID { get; set; } 

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScreenFieldSetting> ScreenFieldSettings { get; set; }
    }
}
