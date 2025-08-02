using Newtonsoft.Json;
using Eduegate.Framework;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.HR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.HR
{
    public class JobDepartmentViewModel : BaseMasterViewModel
    {
        public JobDepartmentViewModel()
        {
            Tags = new List<KeyValueViewModel>();
        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Department ID")]
        public long DepartmentID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.TextBox)]

        [Required]
        [DisplayName("Department Name")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string DepartmentName { get; set; }

        [Required]
        public string LogFile { get; set; }

        public string LogoUrl { get; set; }

        [Required]
        [DisplayName("Logo")]
        [FileUploadInfo("Mutual/UploadImages", EduegateImageTypes.DepartmentLogo, "LogoUrl", "")]
        [ControlType(Framework.Enums.ControlTypes.FileUpload)]
        public string LogoUploadFile { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Tags")]
        [Select2("Tags", "String", true, null, true, isTagUpper: false)]
        [LookUp("LookUps.Tags")]
        public List<KeyValueViewModel> Tags { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[LookUp("LookUps.DepartmentStatus")]
        //[DisplayName("Status")]
        //public string DepartmentStatus { get; set; }
        public byte StatusID { get; set; }
        public Nullable<int> CompanyID { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<JobDepartmentViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            var jobDTO = dto as JobDepartmentDTO;
            Mapper<JobDepartmentDTO, JobDepartmentViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var mapper = Mapper<JobDepartmentDTO, JobDepartmentViewModel>.Map(jobDTO);
            mapper.LogFile = jobDTO.Logo;
            mapper.LogoUrl = string.Format("{0}/{1}/{2}", new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl"), EduegateImageTypes.DepartmentLogo, jobDTO.Logo);

            return mapper;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<JobDepartmentDTO>(jsonString);
        }

        public override BaseMasterDTO ToDTO(CallContext context)
        {
            Mapper<JobDepartmentViewModel, JobDepartmentDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var toDTO = Mapper<JobDepartmentViewModel, JobDepartmentDTO>.Map(this);

            var userID = context.LoginID;
            var fileName = System.IO.Path.GetFileName(this.LogoUrl);
            //move from temparary folder to oringal location
            string tempFolderPath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}",
                    new Domain.Setting.SettingBL().GetSettingValue<string>("ImagesPhysicalPath").ToString(), EduegateImageTypes.DepartmentLogo.ToString(), Constants.TEMPFOLDER, userID, fileName);
            string orignalFolderPath = string.Format("{0}//{1}//{2}",
                     new Domain.Setting.SettingBL().GetSettingValue<string>("ImagesPhysicalPath").ToString(), EduegateImageTypes.DepartmentLogo.ToString(), fileName);

            if (System.IO.File.Exists(tempFolderPath))
            {
                if (!Directory.Exists(Path.GetDirectoryName(orignalFolderPath)))
                    Directory.CreateDirectory(orignalFolderPath);

                System.IO.File.Copy(tempFolderPath, orignalFolderPath, true);
                toDTO.Logo = fileName;
            }

            return toDTO;
        }
    }
}
