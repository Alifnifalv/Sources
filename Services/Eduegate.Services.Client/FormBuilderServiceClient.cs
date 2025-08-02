using System;
using Eduegate.Framework;
using Eduegate.Services.Contracts;
using Eduegate.Service.Client;
using Eduegate.Services.Contracts.School.Forms;
using System.Collections.Generic;
using Eduegate.Services.Contracts.Commons;

namespace Eduegate.Services.Client
{
    public class FormBuilderServiceClient : BaseClient, IFormBuilderService
    {
        public FormBuilderServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public OperationResultDTO SaveFormValues(int formID, List<FormValueDTO> formValueDTOs)
        {
            throw new NotImplementedException();
        }

        public FormValueDTO GetFormValuesByFormAndReferenceID(long? referenceID, int? formID)
        {
            throw new NotImplementedException();
        }

    }
}