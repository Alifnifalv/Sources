namespace Eduegate.Domain.Entity.Setting.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Squences", Schema = "setting")]
    public partial class Sequence
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sequence()
        {
            ScreenFieldSettings = new HashSet<ScreenFieldSetting>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SquenceID { get; set; }

        [StringLength(50)]
        public string SequenceType { get; set; }

        [StringLength(10)]
        public string Prefix { get; set; }

        [StringLength(50)]
        public string Format { get; set; }

        public long? LastSequence { get; set; }

        public bool? IsAuto { get; set; }

        public int? ZeroPadding { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScreenFieldSetting> ScreenFieldSettings { get; set; }
    }
}
