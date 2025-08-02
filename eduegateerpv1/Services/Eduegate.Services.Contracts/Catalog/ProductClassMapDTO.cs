using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Services.Contracts.School.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class ProductClassMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ProductClassMapDTO()
        {
            ProductClassMaps = new List<ProductClassMapDetailDTO>();

        }

        [DataMember]
        public long ProductClassMapIID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public string AcademicYearName { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }


        [DataMember]
        public List<ProductClassMapDetailDTO> ProductClassMaps { get; set; }

    }
}
