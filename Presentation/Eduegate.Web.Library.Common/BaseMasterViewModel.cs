using Newtonsoft.Json;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace Eduegate.Web.Library.ViewModels
{
    public class BaseMasterViewModel
    {
        public BaseMasterViewModel() {
            this.IsError = false;
            NamePrefix = string.Empty;
            IsSummaryPanel = true;
        }

        public CallContext Context { get; set; }
        public List<CultureDataInfoDTO> Cultures {get;set;}      
        public int? CreatedBy { get; set; }
        public string CreatedByUserName { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedByUserName { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string TimeStamps { get; set; }
        public string UserMessage { get; set; }
        public bool IsError { get; set; }
        public string ErrorCode { get; set; }
        public string NamePrefix { get; set; }
        public bool IsSummaryPanel { get; set; }
        public bool IsHierarchicalGrid { get; set; }

        public virtual string AsDTOString(BaseMasterDTO dto)
        {
            return JsonConvert.SerializeObject(dto as BaseMasterDTO);
        }

        public virtual BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<BaseMasterViewModel>(jsonString);
        }

        public virtual BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<BaseMasterDTO, BaseMasterViewModel>.CreateMap();
            return Mapper<BaseMasterDTO, BaseMasterViewModel>.Map(dto);
        }

        public virtual BaseMasterViewModel ToVM(BaseMasterDTO dto, List<CultureDataInfoDTO> cultures)
        {
            return ToVM(dto);
        }

        public virtual void InitializeVM(List<CultureDataInfoViewModel> datas)
        {
        }

        public virtual BaseMasterDTO ToDTO()
        {
            Mapper<BaseMasterViewModel, BaseMasterDTO>.CreateMap();
            return Mapper<BaseMasterViewModel, BaseMasterDTO>.Map(this);
        }

        public virtual BaseMasterDTO ToDTO(CallContext context)
        {
            return ToDTO();
        }

        public virtual BaseMasterDTO ToDTO(string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString))
                return null;
            else
                return JsonConvert.DeserializeObject<BaseMasterDTO>(jsonString);
        }

        public virtual BaseMasterViewModel CloneVM()
        {
            try
            {
                PropertyInfo[] properties = GetType().GetProperties();
                foreach (PropertyInfo propertyInfo in properties)
                {
                    if (propertyInfo.Name.Contains("IID"))
                    {
                        propertyInfo.SetValue(this, 0, null);
                        return this;
                    }
                }

                return this;
            }
            catch
            {
                return this;
            }
        }

        public virtual void SetIdentityColumnsZero(BaseMasterViewModel vm)
        {
        }
    }
}