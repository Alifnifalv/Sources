using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Eduegate.Web.Library.School.Academics
{
    public class StudentAssignmentMapViewModel : BaseMasterViewModel
    {
        public StudentAssignmentMapViewModel()
        {
            AssignmentDetails = new List<StudentAssignmentMapDetailViewModel>() { new StudentAssignmentMapDetailViewModel() };
        }
        public long StudentAssignmentMapIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Class")]
        public string ClassName { get; set; }
        public int? ClassID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Section")]
        public string SectionName { get; set; }
        public int? SectionID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Subject")]
        public string SubjectName { get; set; }
        public int? SubjectID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Assignment")]
        public string AssignmentName { get; set; }
        public long? AssignmentID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("AssignmentDetails")]
        public List<StudentAssignmentMapDetailViewModel> AssignmentDetails { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as StudentAssignmentDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentAssignmentMapViewModel>(jsonString);
        }
        
        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<StudentAssignmentDTO, StudentAssignmentMapViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var mpdto = dto as StudentAssignmentDTO;
            var vm = Mapper<StudentAssignmentDTO, StudentAssignmentMapViewModel>.Map(dto as StudentAssignmentDTO);

            vm.StudentAssignmentMapIID = mpdto.StudentAssignmentMapIID;
            vm.ClassName = mpdto.ClassID.HasValue ? mpdto.ClassName : null;

            vm.AssignmentDetails = new List<StudentAssignmentMapDetailViewModel>();

            foreach (var det in mpdto.StudentAssignmentMaps)
            {
                if (det.StudentName != null)
                {
                    vm.AssignmentDetails.Add(new StudentAssignmentMapDetailViewModel()
                    {
                        StudentAssignmentMapIID = det.StudentAssignmentMapIID,
                        StudentName = det.StudentName,
                        Remarks = det.Remarks,
                        ProfileUrl = det.AttachmentReferenceId.Value.ToString(),
                        ImageName = det.AttachmentReferenceId.Value.ToString(),
                    });
                }
            }

            return vm;

        }
        public override BaseMasterDTO ToDTO()
        {
            Mapper<StudentAssignmentMapViewModel, StudentAssignmentDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<StudentAssignmentMapViewModel, StudentAssignmentDTO>.Map(this);

            dto.StudentAssignmentMapIID = this.StudentAssignmentMapIID;
            foreach (var detail in this.AssignmentDetails)
            {
                if (detail.StudentName != null)
                {
                        dto.StudentAssignmentMaps.Add(new StudentAssignmentMapDTO()
                        {
                            StudentAssignmentMapIID = detail.StudentAssignmentMapIID,
                            AttachmentReferenceId = long.Parse(detail.ImageName),
                            Remarks = detail.Remarks,
                        });
                    }
            }
            return dto;
        }
        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentAssignmentDTO>(jsonString);
        }

    }   
}
