using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("CustomerJustAsk", Schema = "cms")]
    public partial class CustomerJustAsk
    {
        [Key]
        public long JustAskIID { get; set; }
        public string Name { get; set; }
        public string EmailID { get; set; }
        public string Telephone { get; set; }
        public string Description { get; set; }
        public string IPAddress { get; set; }
        public byte CultureID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual Culture Culture { get; set; }
    }
}
