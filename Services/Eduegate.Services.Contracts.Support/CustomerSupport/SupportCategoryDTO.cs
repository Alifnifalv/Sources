using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Support.CustomerSupport
{
    public class SupportCategoryDTO : BaseMasterDTO
    {
        public SupportCategoryDTO()
        {
        }

        [DataMember]
        public int SupportCategoryID { get; set; }

        [DataMember]
        [StringLength(100)]
        public string CategoryName { get; set; }

        [DataMember]
        public int? ParentCategoryID { get; set; }

        [DataMember]
        public int? SortOrder { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public string ParentCategoryName { get; set; }
    }
}