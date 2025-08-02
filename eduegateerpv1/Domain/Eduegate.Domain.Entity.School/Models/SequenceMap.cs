

namespace Eduegate.Domain.Entity.School.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Sequences", Schema = "schools")]
    public partial class SequenceMap
    {


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
    }
}
