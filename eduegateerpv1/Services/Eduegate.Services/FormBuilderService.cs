using Eduegate.Domain.Mappers.School.Forms;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.School.Forms;

namespace Eduegate.Services
{
    public class FormBuilderService : BaseService, IFormBuilderService
    {
        public OperationResultDTO SaveFormValues(int formID, List<FormValueDTO> formValueDTOs)
        {
            return FormBuilderMapper.Mapper(CallContext).SaveFormValues(formID, formValueDTOs);
        }

        public FormValueDTO GetFormValuesByFormAndReferenceID(long? referenceID, int? formID)
        {
            return FormBuilderMapper.Mapper(CallContext).GetFormValuesByFormAndReferenceID(referenceID, formID);
        }

    }
}