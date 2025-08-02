using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Eduegate.Framework;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels.Common;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Branch", "CRUDModel.ViewModel")]
    [DisplayName("Branch Details")]
    public class BranchViewModel : BaseMasterViewModel
    {
        public BranchViewModel()
        {
            PriceListMap = new PriceListTabViewModel();
            DocumentTypeMap = new DocumentTypeListViewModel();
            BranchNameMulti = new MultiLanguageText();
        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("BranchID")]
        public long BranchIID { get; set; }

        //TODO: Need to check this property
        //[IgnoreMap]
        [ControlType(Framework.Enums.ControlTypes.TextBoxWithMultiLanguage)]
        [Required]
        [CustomDisplay("BranchName")]
        public MultiLanguageText BranchNameMulti { get; set; }

        public string LogoUrl { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.FileUpload)]
        [CustomDisplay("LogoFile")]
        [FileUploadInfo("Mutual/UploadImages", EduegateImageTypes.Branch, "LogoUrl", "")]
        public string Logo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.BranchGroup")]
        [CustomDisplay("Group")]
        public string BranchGroup { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Warehouse")]
        [CustomDisplay("Warehouse")]
        public string Warehouse { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.BranchStatus")]
        [CustomDisplay("Status")]
        public string BranchStatus { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[Required]
        [CustomDisplay("Longitude")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string Longitude { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[Required]
        [CustomDisplay("Latitude")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string Latitude { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsMarketplace")]
        public bool IsMarketPlace { get; set; }

        public long? BranchGroupID { get; set; }
        public long? WarehouseID { get; set; }
        public byte StatusID { get; set; } 
        public Nullable<int> CompanyID { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "PriceList", "PriceListsMap")]
        [CustomDisplay("PriceList")]
        public PriceListTabViewModel PriceListMap { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "DocumentType", "DocumentTypeMap")]
        [CustomDisplay("DocumentType(Inventory)")]
        public DocumentTypeListViewModel DocumentTypeMap { get; set; }

        public override void InitializeVM(List<CultureDataInfoViewModel> datas)
        {
            this.BranchNameMulti = new MultiLanguageText(datas); 
        }

        public static List<BranchViewModel> FromDTO(List<BranchDTO> dtos, List<CultureDataInfoViewModel> datas)
        {
            var vms = new List<BranchViewModel>();

            foreach (var dto in dtos)
            {
                vms.Add(FromDTO(dto, datas));
            }

            return vms;
        }

        public static BranchViewModel FromDTO(BranchDTO dto, List<CultureDataInfoViewModel> datas)
        {
            Mapper<BranchDTO, BranchViewModel>.CreateMap();
            var mapper = Mapper<BranchDTO, BranchViewModel>.Map(dto);
            mapper.BranchGroup = dto.BranchGroupID.ToString();
            mapper.Warehouse = dto.WarehouseID.ToString();
            mapper.BranchStatus = dto.StatusID.ToString();
            mapper.IsMarketPlace = dto.IsMarketPlace.HasValue ? dto.IsMarketPlace.Value : false;
            mapper.PriceListMap.PriceLists = new List<BranchPriceListMapViewModel>();
            mapper.DocumentTypeMap.DocumentTypes = new List<DocumentTypeMapViewModel>();
            mapper.LogoUrl = dto.Logo;
            mapper.CompanyID = dto.CompanyID;

            mapper.PriceListMap.PriceLists = new List<BranchPriceListMapViewModel>();
            if (dto.PriceLists != null)
            {
                foreach (var lst in dto.PriceLists)
                {
                    mapper.PriceListMap.PriceLists.Add(new BranchPriceListMapViewModel()
                    {
                        ProductPriceListBranchMapIID = lst.ProductPriceListBranchMapIID,
                        PriceListID = lst.PriceListID.ToString(),
                        PriceDescription = lst.PriceDescription
                    });
                }
            }
            if (mapper.PriceListMap.PriceLists.Count == 0)
            {
                mapper.PriceListMap.PriceLists.Add(new BranchPriceListMapViewModel());
            }

            mapper.DocumentTypeMap.DocumentTypes = new List<DocumentTypeMapViewModel>();
            if (dto.DocumentTypeMaps != null)
            {
                foreach (var doc in dto.DocumentTypeMaps)
                {
                    mapper.DocumentTypeMap.DocumentTypes.Add(new DocumentTypeMapViewModel()
                    {
                        BranchDocumentTypeMapIID = doc.BranchDocumentTypeMapIID,
                        DocumentTypeID = doc.DocumentTypeID.ToString()
                    });
                }
            }
            if (mapper.DocumentTypeMap.DocumentTypes.Count == 0)
            {
                mapper.DocumentTypeMap.DocumentTypes.Add(new DocumentTypeMapViewModel());
            }

            bool isFirst = true;
            mapper.BranchNameMulti = new MultiLanguageText(datas);
            foreach (var cultureData in datas)
            {
                var data = dto.BranchCultureDatas.FirstOrDefault(a => a.CultureID == cultureData.CultureID);

                if (isFirst && data == null)
                {
                    data = new BranchCultureDataDTO()
                    {
                        CultureID = cultureData.CultureID,
                        BranchName = dto.BranchName,
                    };

                    isFirst = false;
                }

                if (data != null)
                    mapper.BranchNameMulti.SetValueByCultureID(cultureData, data.BranchName, cultureData.TimeStamps);
            }

            return mapper;
        }

        public static List<BranchDTO> ToDTO(List<BranchViewModel> vms, CallContext context, List<CultureDataInfoViewModel> datas)
        {
            var dtos = new List<BranchDTO>();

            foreach (var vm in vms)
            {
                dtos.Add(ToDTO(vm, context, datas));
            }

            return dtos;
        }

        public static BranchDTO ToDTO(BranchViewModel vm, CallContext _context, List<CultureDataInfoViewModel> datas)
        {
            Mapper<BranchViewModel, BranchDTO>.CreateMap();
            var mapper = Mapper<BranchViewModel, BranchDTO>.Map(vm);
            mapper.BranchGroupID = string.IsNullOrEmpty(vm.BranchGroup) ? (long?)null : long.Parse(vm.BranchGroup);
            mapper.WarehouseID = string.IsNullOrEmpty(vm.Warehouse) ? (long?)null : long.Parse(vm.Warehouse);
            mapper.StatusID = byte.Parse(vm.BranchStatus);
            mapper.IsMarketPlace = vm.IsMarketPlace;
            mapper.PriceLists = new List<Services.Contracts.Catalog.PriceListDetailDTO>();
            mapper.Logo = vm.Logo;
            mapper.CompanyID = vm.CompanyID.IsNotNull() ? vm.CompanyID : _context.CompanyID;
            mapper.BranchName = vm.BranchNameMulti.Text;

            var fileName = System.IO.Path.GetFileName(vm.LogoUrl);
            //move from temparary folder to oringal location
            string tempFolderPath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}",
                    new Domain.Setting.SettingBL().GetSettingValue<string>("ImagesPhysicalPath").ToString(), EduegateImageTypes.Branch.ToString(), Constants.TEMPFOLDER, _context.LoginID, fileName);

            if (System.IO.File.Exists(tempFolderPath))
            {
                string orignalFolderPath = string.Format("{0}//{1}//{2}",
                       new Domain.Setting.SettingBL().GetSettingValue<string>("ImagesPhysicalPath").ToString(), EduegateImageTypes.Branch.ToString(), fileName);

                if (!Directory.Exists(Path.GetDirectoryName(orignalFolderPath)))
                    Directory.CreateDirectory(orignalFolderPath);

                System.IO.File.Copy(tempFolderPath, orignalFolderPath, true);

                vm.Logo = fileName;
            }

            vm.PriceListMap.PriceLists = vm.PriceListMap.PriceLists.Where(x => !string.IsNullOrEmpty(x.PriceListID)).ToList();

            foreach (var lst in vm.PriceListMap.PriceLists)
            {
                if (lst.PriceListID != null)
                {
                    var pListID = long.Parse(lst.PriceListID);
                    if (!mapper.PriceLists.Any(x => x.PriceListID == pListID))
                    {
                        mapper.PriceLists.Add(new Services.Contracts.Catalog.PriceListDetailDTO()
                        {
                            ProductPriceListBranchMapIID = lst.ProductPriceListBranchMapIID,
                            PriceListID = pListID
                        });
                    }
                }
            }

            vm.DocumentTypeMap.DocumentTypes = vm.DocumentTypeMap.DocumentTypes.Where(x => !string.IsNullOrEmpty(x.DocumentTypeID)).ToList();
            mapper.DocumentTypeMaps = new List<Services.Contracts.Catalog.DocumentTypeDetailDTO>();

            foreach (var doc in vm.DocumentTypeMap.DocumentTypes)
            {
                if (doc.DocumentTypeID != null)
                {
                    var docTypeID = long.Parse(doc.DocumentTypeID);
                    if (!mapper.DocumentTypeMaps.Any(x => x.DocumentTypeID == docTypeID))
                    {
                        mapper.DocumentTypeMaps.Add(new Services.Contracts.Catalog.DocumentTypeDetailDTO()
                        {
                            BranchDocumentTypeMapIID = doc.BranchDocumentTypeMapIID,
                            DocumentTypeID = docTypeID
                        });
                    }
                }
            }

            mapper.BranchCultureDatas = ToCultureDTO(vm, datas);
            return mapper;
        }

        public static List<BranchCultureDataDTO> ToCultureDTO(BranchViewModel vm, List<CultureDataInfoViewModel> cultures)
        {
            var dtos = new List<BranchCultureDataDTO>();
            bool isFrist = true;

            foreach (var culture in cultures)
            {
                //Assume that first one is the default culture which will be there by default.
                if (isFrist)
                {
                    isFrist = false;
                    continue;
                }

                dtos.Add(new BranchCultureDataDTO()
                {
                    CultureID = culture.CultureID,
                    BranchID = vm.BranchIID,
                    BranchName = vm.BranchNameMulti.GetValueByCultureID(culture.CultureID),
                    TimeStamps = vm.BranchNameMulti.GetTimeStampByCultureID(culture.CultureID),
                });
            }

            return dtos;
        }

    }
}
