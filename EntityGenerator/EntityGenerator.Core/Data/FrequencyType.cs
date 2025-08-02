using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FrequencyTypes", Schema = "saloon")]
    public partial class FrequencyType
    {
        public FrequencyType()
        {
            Appointments = new HashSet<Appointment>();
        }

        [Key]
        public int FrequencyTypeID { get; set; }
        [StringLength(200)]
        public string Description { get; set; }

        [InverseProperty("FrequencyType")]
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
