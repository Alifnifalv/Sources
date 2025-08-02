using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Service.Client;
using Eduegate.Services.Contracts.CRM;
using Eduegate.Services.Contracts.Leads;
using Eduegate.Services.CRM;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Client.Direct
{
    public class CRMServiceClient : BaseClient, ICRMService
    {
        CRMService service = new CRMService();

        public CRMServiceClient(CallContext context = null, Action<string> logger = null)
        {
            service.CallContext = context;
        }
       public string AddLead(LeadDTO leadInfo)
        {
            return service.AddLead(leadInfo);
        }
        public List<KeyValueDTO> GetAcademicYearCodeBySchool(int schoolID)
        {
            return service.GetAcademicYearCodeBySchool(schoolID);

        }
        public List<KeyValueDTO> GetClassesBySchool(byte schoolID,int academicYearID)
        {
            return service.GetClassesBySchool(schoolID, academicYearID);
        }
        public List<KeyValueDTO> GetLeadSource()
        {
            return service.GetLeadSource();
        }
        public string GetDeafaultSchool()
        {
            return service.GetDeafaultSchool();
        }

        public List<KeyValueDTO> GetNationalities()
        {
            return service.GetNationalities();
        }
    }
}
