using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Web.Library.ViewModels.Security;
using Eduegate.Services.Contracts.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Web.Library.ViewModels.Common;
using Eduegate.Web.Library.Interface;
using System.Reflection;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using System.IO;

namespace Eduegate.Web.Library.ViewModels
{
    public class CRUDViewModel : ICRUD
    {
        public CRUDViewModel()
        {
            Urls = new List<UrlViewModel>();
            IsCacheable = false;
            IsSavePanelRequired = true;
            FieldSettings = new List<ScreenFieldSettingViewModel>();
            Parameters = new List<KeyValueViewModel>();
            ClientParameters = new List<ClientParameter>();
        }

        public string ListActionName { get; set; }
        public string Name { get; set; }
        public string View { get; set; }
        public string ViewFullPath { get; set; }
        public string DisplayName { get; set; }
        public string ListButtonDisplayName { get; set; }
        public BaseMasterViewModel ViewModel { get; set; }
        public BaseMasterViewModel DetailViewModel { get; set; }
        public BaseMasterViewModel MasterViewModel { get; set; }
        public long IID { get; set; }
        public string IID2 { get; set; }
        public string JsControllerName { get; set; }
        public List<UrlViewModel> Urls { get; set; }
        public bool IsCacheable { get; set; }
        public string CasheType { get; set; }
        public bool IsSavePanelRequired { get; set; }
        public bool IsGenericCRUDSave { get; set; }
        public string SaveCRUDMethod { get; set; }
        public string PrintPreviewReportName { get; set; }
        public string EntityType { get; set; }

        public List<UserRoleViewModel> UserRoles { get; set; }
        public List<ClaimViewModel> Claims { get; set; }
        public Eduegate.Infrastructure.Enums.Screens Screen { get; set; }
        public int ScreenTypeID { get; set; }
        public List<ScreenFieldSettingViewModel> FieldSettings { get; set; }
        public List<KeyValueViewModel> Parameters { get; set; }
        public List<ClientParameter> ClientParameters { get; set; }

        public static CRUDViewModel FromDTO(ScreenMetadataDTO dto , List<CultureDataInfoViewModel> cultures = null)
        {
            var crudVM = new CRUDViewModel()
            {
                Name = dto.Name,
                DisplayName = dto.DisplayName,
                IsCacheable = dto.IsCacheable.HasValue ? dto.IsCacheable.Value : false,
                IsSavePanelRequired = dto.IsSavePanelRequired.HasValue ? dto.IsSavePanelRequired.Value : false,
                IsGenericCRUDSave = dto.IsGenericCRUDSave.HasValue ? dto.IsGenericCRUDSave.Value : false,
                SaveCRUDMethod = dto.SaveCRUDMethod,
                JsControllerName = dto.JsControllerName,
                ListActionName = dto.ListActionName,
                ListButtonDisplayName = dto.ListButtonDisplayName,
                Urls = dto.Urls.IsNotNull() ? dto.Urls.Select(a => new UrlViewModel() { IsOnInit = a.IsOnInit.HasValue ? a.IsOnInit.Value : false, LookUpName = a.LookUpName, Url = a.Url, CallBack = a.CallBack }).ToList() : new List<UrlViewModel>(),
                ScreenTypeID = dto.ScreenTypeID,
                ViewFullPath = string.IsNullOrEmpty(dto.ViewFullPath) ? dto.View : dto.ViewFullPath,
                PrintPreviewReportName = dto.PrintPreviewReportName,
                EntityType = dto.EntityType

            };

            foreach(var field in dto.ScreenFieldSettings)
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
                crudVM.ViewModel = GetVM(dto.ModelViewModel, dto.ModelAssembly);
                crudVM.ViewModel.InitializeVM(cultures);
            }

            if (dto.MasterViewModel.IsNotNullOrEmpty())
            {
                crudVM.MasterViewModel = GetVM(dto.MasterViewModel, dto.ModelAssembly);
                crudVM.MasterViewModel.InitializeVM(cultures);
            }

            if (dto.DetailViewModel.IsNotNullOrEmpty())
                crudVM.DetailViewModel = GetVM(dto.DetailViewModel, dto.ModelAssembly);

            crudVM.Claims = ClaimViewModel.FromDTO(dto.Claims);
            return crudVM;
        }

        public static BaseMasterViewModel GetVM(string formTypeFullName, string assembly = "")
        {
            if (!string.IsNullOrEmpty(assembly))
            {
                string assemblyLocation = Assembly.GetExecutingAssembly().Location;
                string assemblyDirectory = Path.GetDirectoryName(assemblyLocation);
                string assemblyFullPath = Path.Combine(assemblyDirectory, assembly);
                var assemblyLoaded = Assembly.LoadFrom(assemblyFullPath);
                var typeReference = assemblyLoaded.CreateInstance(formTypeFullName);
                return typeReference as BaseMasterViewModel;
            }

            Type type = Type.GetType(formTypeFullName, true);
            return (BaseMasterViewModel)Activator.CreateInstance(type);
        }
    }
}