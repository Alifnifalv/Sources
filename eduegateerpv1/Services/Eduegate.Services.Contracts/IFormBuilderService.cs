using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.School.Forms;
using System.Collections.Generic;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFormBuilderService" in both code and config file together.
    public interface IFormBuilderService
    {
        OperationResultDTO SaveFormValues(int formID, List<FormValueDTO> formValueDTOs);

        FormValueDTO GetFormValuesByFormAndReferenceID(long? referenceID, int? formID);
    }
}