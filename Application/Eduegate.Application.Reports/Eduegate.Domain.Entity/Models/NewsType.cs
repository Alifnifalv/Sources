using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class NewsType
    {
        public NewsType()
        {
            this.News = new List<News>();
        }

        public int NewsTypeID { get; set; }
        public string NewsTypeName { get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}
