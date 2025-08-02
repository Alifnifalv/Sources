using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Search
{
    [DataContract]
    public class SearchParameterDTO
    {
        [DataMember]
        public Eduegate.Services.Contracts.Enums.SearchView SearchView {get;set;}
        [DataMember]
        public int CurrentPage {get;set;}
        [DataMember]
        public int PageSize {get;set;}
        [DataMember]
        public string OrderBy {get;set;}
        [DataMember]
        public string RuntimeFilter { get; set; }
        [DataMember]
        public char ViewType { get; set; } = '\0';
        [DataMember]
        public short? SchoolID { get; set; } = null;
        [DataMember]
        public int? AcademicYearID { get; set; } = null;
    }
}
