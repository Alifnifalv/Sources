using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("UserJobApplications", Schema = "cms")]
    public partial class UserJobApplication
    {
        [Key]
        public long JobApplicationIID { get; set; }
        public long JobID { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        public string Telephone { get; set; }
        [Required]
        [StringLength(150)]
        public string Resume { get; set; }
        [Required]
        [StringLength(250)]
        public string IPAddress { get; set; }
        public byte CultureID { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}
