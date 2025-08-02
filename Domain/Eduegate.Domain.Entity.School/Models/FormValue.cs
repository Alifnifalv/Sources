using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("FormValues", Schema = "form")]
    public partial class FormValue
    {
        [Key]
        public long FormValueIID { get; set; }

        public long? FormFieldID { get; set; }

        public int? FormID { get; set; }

        public long? ReferenceID { get; set; }

        public string FormFieldName { get; set; }

        public string FormFieldValue { get; set; }

        public virtual FormField FormField { get; set; }

        public virtual Form Form { get; set; }
    }
}