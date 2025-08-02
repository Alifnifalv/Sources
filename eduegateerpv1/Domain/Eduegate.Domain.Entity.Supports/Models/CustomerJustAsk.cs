using Eduegate.Domain.Entity.Supports.Models.Mutual;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Supports.Models
{
    [Table("CustomerJustAsk", Schema = "cms")]
    public partial class CustomerJustAsk
    {
        [Key]
        public long JustAskIID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string EmailID { get; set; }

        [Required]
        [StringLength(100)]
        public string Telephone { get; set; }

        [Required]
        [StringLength(300)]
        public string Description { get; set; }

        [Required]
        [StringLength(250)]
        public string IPAddress { get; set; }

        public byte CultureID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public virtual Culture Culture { get; set; }
    }
}