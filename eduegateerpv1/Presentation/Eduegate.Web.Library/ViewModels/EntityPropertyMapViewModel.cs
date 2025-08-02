using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels
{
    public class EntityPropertyMapViewModel : BaseMasterViewModel
    {
        public long EntityPropertyMapID { get; set; }
        public Nullable<int> EntityTypeID { get; set; }
        public Nullable<int> EntityPropertyTypeID { get; set; }
        public Nullable<long> EntityPropertyID { get; set; }
        public Nullable<long> ReferenceID { get; set; }
        public Nullable<short> Sequence { get; set; }
        public virtual string Value1 { get; set; }
        public virtual string Value2 { get; set; }
    }
}
