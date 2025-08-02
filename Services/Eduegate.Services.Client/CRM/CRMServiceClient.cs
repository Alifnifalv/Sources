using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Service.Client;
using Eduegate.Services.Contracts.CRM;
using Eduegate.Services.Contracts.Leads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Client.CRM
{
    public class CRMServiceClient : BaseClient, ICRMService
    {
        public CRMServiceClient(CallContext context = null, Action<string> logger = null)
          : base(context, logger)
        {
        }
        public string AddLead(LeadDTO leadInfo)
        {
            throw new NotImplementedException();

        }


        public List<KeyValueDTO> GetAcademicYearCodeBySchool(int schoolID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetClassesBySchool(byte schoolID, int AcademicYearID)
        {
            throw new NotImplementedException();
        }
        public List<KeyValueDTO> GetLeadSource()
        {
            throw new NotImplementedException();
        }

        public string GetDeafaultSchool()
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetNationalities()
        {
            throw new NotImplementedException();
        }

    }
}
