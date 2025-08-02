using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ViewActions", Schema = "setting")]
    public partial class ViewAction
    {
        public ViewAction()
        {
            
        }

        [Key]
        public long ViewActionID { get; set; }
        public Nullable<long> ViewID { get; set; }
        public string ActionCaption { get; set; }
        public string ActionAttribute { get; set; }
        public string ActionAttribute2 { get; set; }

        public virtual View View { get; set; }
    }
}
