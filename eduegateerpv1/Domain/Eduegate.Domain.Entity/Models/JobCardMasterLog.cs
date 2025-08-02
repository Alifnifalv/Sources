using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class JobCardMasterLog
    {
        [Key]
        public int JobCardMasterLogID { get; set; }
        public int RefJobCardMasterID { get; set; }
        public string JobCardStatus { get; set; }
        public short UpdatedBy { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public virtual JobCardMaster JobCardMaster { get; set; }
    }
}
