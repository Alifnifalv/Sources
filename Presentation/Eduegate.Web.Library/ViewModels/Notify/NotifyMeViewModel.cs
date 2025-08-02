using System;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Translator;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Eduegate.Web.Library.ViewModels.Notify
{
    public class NotifyMeViewModel : BaseMasterViewModel
    {
       public NotifyMeViewModel()
       {

       }

       public NotifyMeViewModel(long skuID,int siteID)
       {
           ProductSKUMapID = skuID;
           SiteID = siteID;
       }

       [Required(ErrorMessage = "Email id is required")]
       [DataType(DataType.EmailAddress)]
       [Display(Name = "Email")]
       [EmailAddress(ErrorMessage = "Please enter a valid email id")]
       public string EmailID { get; set; }

       [Required]
       public long ProductSKUMapID { get; set; }

       [Required]
       public int SiteID { get; set; }

       public int NotifyIID { get; set; }

       public Nullable<bool> IsEmailSend { get; set; }

       public short StatusID { get; set; }

       public string StatusMessage { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as NotifyMeDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<NotifyMeViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<NotifyMeDTO, NotifyMeViewModel>.CreateMap();
            var notifDTO = dto as NotifyMeDTO;
            var vm = Mapper<NotifyMeDTO, NotifyMeViewModel>.Map(notifDTO);

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<NotifyMeViewModel, NotifyMeDTO>.CreateMap();
            var dto = Mapper<NotifyMeViewModel, NotifyMeDTO>.Map(this);

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<NotifyMeDTO>(jsonString);
        }

    }
}