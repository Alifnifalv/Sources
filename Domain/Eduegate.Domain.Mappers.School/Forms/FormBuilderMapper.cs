using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.School.Forms;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Forms
{
    public class FormBuilderMapper : DTOEntityDynamicMapper
    {
        public static FormBuilderMapper Mapper(CallContext context)
        {
            var mapper = new FormBuilderMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<FormValueDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private FormValueDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.FormValues.AsNoTracking().FirstOrDefault(X => X.ReferenceID == IID);

                return ToDTO(entity);
            }
        }

        private FormValueDTO ToDTO(FormValue entity)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

                var routeStopMapDTO = new FormValueDTO()
                {
                    FormValueIID = entity.FormValueIID,
                    ReferenceID = entity.ReferenceID,
                    FormID = entity.FormID,
                    FormFieldID = entity.FormFieldID,
                    FormFieldValue = entity.FormFieldValue
                };

                return routeStopMapDTO;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as FormValueDTO;

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = new FormValue()
                {
                    FormValueIID = toDto.FormValueIID,
                    ReferenceID = toDto.ReferenceID,
                    FormID = toDto.FormID,
                    FormFieldID = toDto.FormFieldID,
                    FormFieldValue = toDto.FormFieldValue
                };

                dbContext.FormValues.Add(entity);

                if (entity.FormValueIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();

                return ToDTOString(ToDTO(Convert.ToInt64(entity.ReferenceID)));
            }
        }

        public OperationResultDTO SaveFormValues(int formID, List<FormValueDTO> formValueDTOs)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var message = new OperationResultDTO();
                try
                {
                    string field = string.Empty;
                    string formValue = string.Empty;
                    var formField = new FormField();

                    var referenceID = formValueDTOs.FirstOrDefault()?.ReferenceID;
                    
                    if(referenceID != null)
                    {
                        var fieldValues = dbContext.FormValues.Where(x => x.ReferenceID == referenceID).AsNoTracking().ToList();

                        if(fieldValues.Count > 0)
                        {
                            throw new Exception("Application already exist!");
                        }
                    }

                    var formFieldDatas = dbContext.FormFields.Where(f => f.FormID == formID && f.IsActive == true).AsNoTracking().ToList();

                    foreach (var dto in formValueDTOs)
                    {
                        formField = formFieldDatas.Find(x => x.FieldName == dto.FormFieldName);
                        dto.FormFieldID = formField?.FormFieldID;
                        SaveFormData(dbContext, dto);
                    }

                    //save system data                    
                    formField = formFieldDatas.Find(f => f.FieldName == "CreatedDate");
                    if(formField != null)
                    {
                        SaveFormData(dbContext, new FormValueDTO()
                        {
                            FormID = formID,
                            FormFieldID = formField.FormFieldID,
                            ReferenceID = referenceID,
                            FormFieldName = formField.FieldName,
                            FormFieldValue = DateTime.Now.ToString("o")
                        });
                    }

                    formField = formFieldDatas.Find(f => f.FieldName == "CreatedBy");
                    if (formField != null && _context.LoginID.HasValue)
                    {
                        SaveFormData(dbContext, new FormValueDTO()
                        {
                            FormID = formID,
                            FormFieldID = formField.FormFieldID,
                            ReferenceID = referenceID,
                            FormFieldName = formField.FieldName,
                            FormFieldValue = _context.LoginID.Value.ToString()
                        });
                    }

                    formField = formFieldDatas.Find(f => f.FieldName == "UpdatedDate");
                    if (formField != null)
                    {
                        SaveFormData(dbContext, new FormValueDTO()
                        {
                            FormID = formID,
                            FormFieldID = formField.FormFieldID,
                            ReferenceID = referenceID,
                            FormFieldName = formField.FieldName,
                            FormFieldValue = DateTime.Now.ToString("o")
                        });
                    }

                    formField = formFieldDatas.Find(f => f.FieldName == "UpdatedBy");
                    if (formField != null && _context.LoginID.HasValue) 
                    {
                        SaveFormData(dbContext, new FormValueDTO()
                        {
                            FormID = formID,
                            FormFieldID = formField.FormFieldID,
                            ReferenceID = referenceID,
                            FormFieldName = formField.FieldName,
                            FormFieldValue = _context.LoginID.Value.ToString()
                        });
                    }

                    dbContext.SaveChanges();

                    if (referenceID != null)
                    {
                        long workFlowID = new Domain.Setting.SettingBL(null).GetSettingValue<long>("APPLICATION_FORM_APPROVAL_WORKFLOW_ID");

                        Workflows.Helpers.WorkflowGeneratorHelper.GenerateWorkflows(workFlowID, (long)referenceID);
                    }

                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Success,
                        Message = "Successfully Saved!"
                    };
                }
                catch (Exception ex)
                {
                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = ex.Message
                    };
                }

                return message;
            }
        }

        public void SaveFormData(dbEduegateSchoolContext dbContext, FormValueDTO formValueDTO)
        {
            var entity = new FormValue()
            {
                FormValueIID = formValueDTO != null ? formValueDTO.FormValueIID : 0,
                ReferenceID = formValueDTO?.ReferenceID,
                FormID = formValueDTO?.FormID,
                FormFieldID = formValueDTO?.FormFieldID,
                FormFieldName = formValueDTO?.FormFieldName,
                FormFieldValue = formValueDTO?.FormFieldValue
            };

            if (entity.FormValueIID == 0)
            {
                dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            }
            else
            {
                dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
        }

        public FormValueDTO GetFormValuesByFormAndReferenceID(long? referenceID, int? formID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var formValueDTO = new FormValueDTO();

                var formValues = new List<FormValueDTO>();

                var formFieldList = dbContext.FormFields
                    .Where(x => x.FormID == formID && x.IsActive == true &&
                    (x.IsTitle == true || x.FieldName.Contains("Status") ||x.FieldName.Contains("Create") || x.FieldName.Contains("Update")))
                    .AsNoTracking().ToList();

                foreach (var formField in formFieldList)
                {
                    var formValue = dbContext.FormValues
                        .Where(v => v.ReferenceID == referenceID && v.FormID == formID && v.FormFieldID == formField.FormFieldID)
                        .OrderByDescending(o => o.FormValueIID)
                        .AsNoTracking().FirstOrDefault();

                    if (formValue != null)
                    {
                        formValues.Add(new FormValueDTO()
                        {
                            FormValueIID = formValue.FormValueIID,
                            FormFieldID = formValue.FormFieldID,
                            FormID = formValue.FormID,
                            ReferenceID = formValue.ReferenceID,
                            FormFieldName = formValue.FormFieldName,
                            FormFieldValue = formValue.FormFieldValue,
                        });
                    }
                }

                formValueDTO = new FormValueDTO()
                {
                    ReferenceID = referenceID,
                    FormID = formID,
                    FormValues = formValues,
                };

                return formValueDTO;
            }
        }

    }
}