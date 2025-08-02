using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Leads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.CRM
{
    [ServiceContract]
   public interface ICRMService 
    {

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "AddLead")]
        string AddLead(LeadDTO leadInfo);
        [OperationContract]
        [WebInvoke(Method = "Get", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAcademicYearCodeBySchool")]
        List<KeyValueDTO> GetAcademicYearCodeBySchool(int schoolID);
        [WebInvoke(Method = "Get", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetClassesBySchool")]
        List<KeyValueDTO> GetClassesBySchool(byte schoolID,int academicYearID);
        [WebInvoke(Method = "Get", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetLeadSource")]
        List<KeyValueDTO> GetLeadSource();
        [WebInvoke(Method = "Get", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetDeafaultSchool")]
        string GetDeafaultSchool();

        [WebInvoke(Method = "Get", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetNationalities")]
        List<KeyValueDTO> GetNationalities();

    }
}
