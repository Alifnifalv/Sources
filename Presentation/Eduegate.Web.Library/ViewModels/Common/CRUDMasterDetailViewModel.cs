using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Web.Library.ViewModels.Security;
using Eduegate.Services.Contracts.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Web.Library.Interface;
using System.Reflection;
using System.IO;

namespace Eduegate.Web.Library.ViewModels.Common
{
    public class CRUDMasterDetailViewModel : ICRUD
    {
        public CRUDMasterDetailViewModel()
        {
            Urls = new List<UrlViewModel>();
            IsBreadCrumbRequired = true;
            HasWindowContainer = true;
            FieldSettings = new List<ScreenFieldSettingViewModel>();
            ClientParameters = new List<ClientParameter>();
            Claims = new List<ClaimViewModel>();
            Parameters = new List<KeyValueViewModel>();
            EntityType = Framework.Enums.EntityTypes.Transaction.ToString();
        }

        public string ListActionName { get; set; }
        public string Name { get; set; }
        public string ViewFullPath { get; set; }
        public string View { get; set; }
        public string DisplayName { get; set; }
        public string ListButtonDisplayName { get; set; }
        public BaseMasterViewModel Model { get; set; }
        public BaseMasterViewModel MasterViewModel { get; set; }
        public BaseMasterViewModel DetailViewModel { get; set; }
        public BaseMasterViewModel SummaryViewModel { get; set; }
        public string EntityType { get; set; }
        public long IID { get; set; }

        public List<UrlViewModel> Urls { get; set; }
        public string CasheType { get; set; }
        public bool? IsSavePanelRequired { get; set; }
        public bool? IsGenericCRUDSave { get; set; }
        public string SaveCRUDMethod { get; set; }
        public bool IsBreadCrumbRequired { get; set; }
        public string ReferenceIIDs { get; set; }
        public bool HasWindowContainer { get; set; }
        public string JsControllerName { get; set; }
        public string PrintPreviewReportName { get; set; }
        public List<UserRoleViewModel> UserRoles { get; set; }
        public Eduegate.Infrastructure.Enums.Screens Screen { get; set; }
        public List<ScreenFieldSettingViewModel> FieldSettings { get; set; }
        public List<ClientParameter> ClientParameters { get; set; }
        public List<KeyValueViewModel> Parameters { get; set; }
        public List<ClaimViewModel> Claims { get; set; }

        public static CRUDMasterDetailViewModel FromDTO(ScreenMetadataDTO dto, List<CultureDataInfoViewModel> cultures = null)
        {
            var crudVM = new CRUDMasterDetailViewModel()
            {
                Name = dto.Name,
                DisplayName = dto.DisplayName,
                JsControllerName = dto.JsControllerName,
                ListActionName = dto.ListActionName,
                ListButtonDisplayName = dto.ListButtonDisplayName,
                Urls = dto.Urls.IsNotNull() ? dto.Urls.Select(a => new UrlViewModel() { IsOnInit = a.IsOnInit.HasValue ? a.IsOnInit.Value : false, LookUpName = a.LookUpName, Url = a.Url }).ToList() : new List<UrlViewModel>(),
                CasheType = dto.CacheType,
                IsSavePanelRequired = dto.IsSavePanelRequired,
                IsGenericCRUDSave = dto.IsGenericCRUDSave,
                SaveCRUDMethod = dto.SaveCRUDMethod,
                ViewFullPath = string.IsNullOrEmpty(dto.ViewFullPath) ? dto.View : dto.ViewFullPath,
                PrintPreviewReportName = dto.PrintPreviewReportName,
                EntityType = string.IsNullOrEmpty(dto.EntityType) ? Framework.Enums.EntityTypes.Transaction.ToString() : dto.EntityType
            };

            foreach (var field in dto.ScreenFieldSettings)
            {
                crudVM.FieldSettings.Add(new ScreenFieldSettingViewModel()
                {
                    DateType = field.DateType,
                    DefaultFormat = field.DefaultFormat,
                    DefaultValue = field.DefaultValue,
                    FieldName = field.FieldName,
                    ModelName = field.ModelName,
                    LookupName = field.LookupName,
                    ScreenFieldID = field.ScreenFieldID,
                    ScreenFieldSettingID = field.ScreenFieldSettingID
                });
            }

            if (dto.ModelViewModel.IsNotNullOrEmpty())
            {
                crudVM.Model = GetVM(dto.ModelViewModel, dto.ModelAssembly);
                crudVM.Model.InitializeVM(cultures);
            }

            if (dto.MasterViewModel.IsNotNullOrEmpty())
            {
                crudVM.MasterViewModel = GetVM(dto.MasterViewModel, dto.ModelAssembly);
                crudVM.MasterViewModel.InitializeVM(cultures);
            }

            if (dto.DetailViewModel.IsNotNullOrEmpty())
            {
                crudVM.DetailViewModel = GetVM(dto.DetailViewModel, dto.ModelAssembly);
            }

            if (dto.SummaryViewModel.IsNotNullOrEmpty())
            {
                crudVM.SummaryViewModel = GetVM(dto.SummaryViewModel, dto.ModelAssembly);
            }

            crudVM.Claims = ClaimViewModel.FromDTO(dto.Claims);
            return crudVM;
        }

        public static BaseMasterViewModel GetVM(string fullName, string assembly = "")
        {
            if (!string.IsNullOrEmpty(assembly))
            {
                string assemblyLocation = Assembly.GetExecutingAssembly().Location;
                string assemblyDirectory = Path.GetDirectoryName(assemblyLocation);
                string assemblyFullPath = Path.Combine(assemblyDirectory, assembly);
                var assemblyLoaded = Assembly.LoadFrom(assemblyFullPath);
                var typeReference = assemblyLoaded.CreateInstance(fullName);
                return typeReference as BaseMasterViewModel;
            }

            Type type = Type.GetType(fullName, true);
            return (BaseMasterViewModel)Activator.CreateInstance(type);
        }
    }
}