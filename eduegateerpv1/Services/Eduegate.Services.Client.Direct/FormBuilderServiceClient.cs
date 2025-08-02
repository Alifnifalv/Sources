using System;
using System.Collections.Generic;
using Eduegate.Framework;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.School.Forms;

namespace Eduegate.Services.Client.Direct
{
    public class FormBuilderServiceClient : IFormBuilderService
    {
        FormBuilderService service = new FormBuilderService();

        public FormBuilderServiceClient(CallContext context = null, Action<string> logger = null)
        {
            service.CallContext = context;
        }

        public OperationResultDTO SaveFormValues(int formID, List<FormValueDTO> formValueDTOs)
        {
            return service.SaveFormValues(formID, formValueDTOs);
        }

        public FormValueDTO GetFormValuesByFormAndReferenceID(long? referenceID, int? formID)
        {
            return service.GetFormValuesByFormAndReferenceID(referenceID, formID);
        }

    }
}