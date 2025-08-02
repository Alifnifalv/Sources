using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("NewsTypes", Schema = "cms")]
    public partial class NewsType
    {
        public NewsType()
        {
            this.News = new List<News>();
        }

        [Key]
        public int NewsTypeID { get; set; }
        public string NewsTypeName { get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}
