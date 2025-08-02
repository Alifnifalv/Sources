namespace Eduegate.Domain.Entity.Setting.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ViewActions", Schema = "setting")]
    public partial class ViewAction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ViewActionID { get; set; }

        public long? ViewID { get; set; }

        [StringLength(100)]
        public string ActionCaption { get; set; }

        [StringLength(1000)]
        public string ActionAttribute { get; set; }

        [StringLength(1000)]
        public string ActionAttribute2 { get; set; }

        public virtual View View { get; set; }
    }
}
