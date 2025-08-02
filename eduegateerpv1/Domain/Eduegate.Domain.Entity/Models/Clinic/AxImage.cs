using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models.Clinic
{
    public partial class AxImage
    {
        [Key]
        public int AxId { get; set; }
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string border { get; set; }
        public string width { get; set; }
        public string height { get; set; }
        public string css { get; set; }
        public bool Edit { get; set; }
        public Nullable<int> CheckedOutByUser { get; set; }
        public Nullable<int> DocumentID { get; set; }
        public string ToolTip { get; set; }
        public string ImageFormat { get; set; }
        public virtual AxDocument AxDocument { get; set; }
    }
}
