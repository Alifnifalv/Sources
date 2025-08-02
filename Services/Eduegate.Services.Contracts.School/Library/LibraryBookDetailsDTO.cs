using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Library
{
    [DataContract]
    public class LibraryBookDetailsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public LibraryBookDetailsDTO()
        {

        }

        [DataMember]
        public long LibraryBookIID { get; set; }
        [DataMember]
        public string BookTitle { get; set; }
        [DataMember]
        public string BookNumber { get; set; }       
        [DataMember]
        public string Subject { get; set; }
        [DataMember]
        public string RackNumber { get; set; }
        [DataMember]
        public int? Quatity { get; set; }
        [DataMember]
        public int? IssueCount { get; set; }
        [DataMember]
        public int? returnCount { get; set; }
        [DataMember]
        public decimal? BookPrice { get; set; }
        [DataMember]
        public decimal? PostPrice { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public byte? BookTypeID { get; set; }
        [DataMember]
        public byte? BookConditionID { get; set; }
        [DataMember]
        public byte? BookStatusID { get; set; }
        [DataMember]
        public string BookCategories { get; set; }

        [DataMember]
        public string Subjects { get; set; }
    }
}



