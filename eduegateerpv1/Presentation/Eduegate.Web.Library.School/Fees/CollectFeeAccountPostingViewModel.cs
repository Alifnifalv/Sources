
using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Eduegate.Web.Library.Common;
using System.Globalization;


namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "CollectFeeAccountPosting", "CRUDModel.ViewModel")]
    [DisplayName("Collect Fee Account Posting")]
    public class CollectFeeAccountPostingViewModel : BaseMasterViewModel
    {
        public CollectFeeAccountPostingViewModel()
        {
            DetailData = new List<CollectFeeAccountPostingDetailViewModel>() { new CollectFeeAccountPostingDetailViewModel() };
            PayModeData = new List<FeeAccountPostingPayModeViewModel>() { new FeeAccountPostingPayModeViewModel() };
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            CollectionDateFromString = DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture);
            CollectionDateToString = DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture);
        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("CollectionDateFrom")]
        public string CollectionDateFromString { get; set; }

        public System.DateTime? CollectionDateFrom { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("CollectionDateTo")]
        public string CollectionDateToString { get; set; }

        public System.DateTime? CollectionDateTo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Cashier", "String", false)]
        [CustomDisplay("Cashier")]
        [LookUp("LookUps.Cashier")]
        public KeyValueViewModel CashierEmployee { get; set; }
        public long? CashierID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Button)]
        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=FillCollectFee()")]
        [CustomDisplay("View")]
        public string GenerateButton { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=AccountPosting()")]
        [CustomDisplay("Post")]
        public string PostButton { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("PaymentModeDetails")]
        public List<FeeAccountPostingPayModeViewModel> PayModeData { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Details")]
        public List<CollectFeeAccountPostingDetailViewModel> DetailData { get; set; }

     

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as StudentFeeDueDTO);
        }
        public override BaseMasterDTO ToDTO()
        {
            Mapper<FeeDueGenerationViewModel, StudentFeeDueDTO>.CreateMap();
            Mapper<CollectFeeAccountPostingDetailViewModel, FeeDueFeeTypeMapDTO>.CreateMap();

            var dto = Mapper<CollectFeeAccountPostingViewModel, StudentFeeDueDTO>.Map(this);


            return dto;
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<CollectFeeAccountPostingViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<CollectFeeAccountDTO, CollectFeeAccountPostingViewModel>.CreateMap();
            Mapper<CollectFeeAccountDetailDTO, CollectFeeAccountPostingDetailViewModel>.CreateMap();
            Mapper<CollectFeeAccountSplitDTO, CollectFeeAccountPostingSplitViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var feeDto = dto as CollectFeeAccountDTO;
            var vm = Mapper<CollectFeeAccountDTO, CollectFeeAccountPostingViewModel>.Map(dto as CollectFeeAccountDTO);           
          
            vm.DetailData = new List<CollectFeeAccountPostingDetailViewModel>();

            if (feeDto.DetailDataDto != null)
            {
                foreach (var feeType in feeDto.DetailDataDto)
                {
                    if (feeType.Amount!=0)
                    {
                        var monthlySplit = new List<CollectFeeAccountPostingSplitViewModel>();
                        foreach (var feeMonthlySplit in feeType.FeeAccountSplit)
                        {
                            if (feeMonthlySplit.Amount.HasValue)
                            {
                                var entity = new CollectFeeAccountPostingSplitViewModel()
                                {
                                   
                                };
                                monthlySplit.Add(entity);
                            }
                        }

                        vm.DetailData.Add(new CollectFeeAccountPostingDetailViewModel()
                        {
                           
                        });
                    }
                }
            }

            return vm;
        }



    }
}
