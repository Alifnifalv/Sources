using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("News", Schema = "cms")]
    public partial class News
    {
        [Key]
        public long NewsIID { get; set; }
        public string Title { get; set; }
        public string NewsContentShort { get; set; }
        public string NewsContent { get; set; }
        public string ThumbnailUrl { get; set; }
        public Nullable<int> NewsTypeID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> SiteID { get; set; }
        public virtual NewsType NewsType { get; set; }
    }
}
