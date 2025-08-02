using Eduegate.Domain.Mappers.CRM.Leads;
using Eduegate.Domain.Mappers.School.Academics;
using Eduegate.Domain.Mappers.School.Students;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.CRM;
using Eduegate.Services.Contracts.Leads;

namespace Eduegate.Services.CRM
{
    public class CRMService : BaseService, ICRMService
    {
        public string AddLead(LeadDTO leadInfo)
        {
            return LeadMapper.Mapper(CallContext).SaveEntity(leadInfo);
        }

        public List<KeyValueDTO> GetAcademicYearCodeBySchool(int schoolID)
        {
            return StudentApplicationMapper.Mapper(CallContext).GetAcademicYearCodeBySchool(schoolID);
        }

        public List<KeyValueDTO> GetClassesBySchool(byte schoolID, int academicYearID)
        {
            return ClassMapper.Mapper(CallContext).GetClassesBySchool(schoolID);
        }

        public List<KeyValueDTO> GetLeadSource()
        {
            return LeadMapper.Mapper(CallContext).GetLeadSource();

        }
        public string GetDeafaultSchool()
        {
            return LeadMapper.Mapper(CallContext).GetDeafaultSchool();
        }

        public List<KeyValueDTO> GetNationalities()
        {
            return LeadMapper.Mapper(CallContext).GetNationalities();
        }

    }
}