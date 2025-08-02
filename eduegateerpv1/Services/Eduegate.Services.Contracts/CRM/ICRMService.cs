using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Leads;
using System.Collections.Generic;

namespace Eduegate.Services.Contracts.CRM
{
    public interface ICRMService 
    {
        string AddLead(LeadDTO leadInfo);
        
        List<KeyValueDTO> GetAcademicYearCodeBySchool(int schoolID);
        
        List<KeyValueDTO> GetClassesBySchool(byte schoolID,int academicYearID);
        
        List<KeyValueDTO> GetLeadSource();
        
        string GetDeafaultSchool();

        List<KeyValueDTO> GetNationalities();
    }
}