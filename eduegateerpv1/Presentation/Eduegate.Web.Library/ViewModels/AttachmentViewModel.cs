using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Mutual;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels
{
    public class AttachmentViewModel : BaseMasterViewModel
    {
        public AttachmentViewModel()
        {
            //Comments = new List<CommentViewModel>();
            IsEdit = false;
        }

        public long AttachmentIID { get; set; }
        public List<AttachmentViewModel> Attachments { get; set; }
        public string AttachmentName { get; set; }
        public string Username { get; set; }
        public EntityTypes EntityType { get; set; }
        public long ReferenceID { get; set; }
        public bool IsEdit { get; set; }

        public string AttachmentDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Departments")]
        [DisplayName("Department")]
        public List<KeyValueViewModel> Department { get; set; }

        public Nullable<long> DepartmentID { get; set; }


        public static AttachmentViewModel FromDTO(AttachmentDTO dto)
        {
            Mapper<AttachmentDTO, AttachmentViewModel>.CreateMap();
            var mapper = Mapper<AttachmentDTO, AttachmentViewModel>.Map(dto);
            mapper.AttachmentDate = mapper.CreatedDate.HasValue ? mapper.CreatedDate.Value.ToString() : null;
            return mapper;
        }

        public static AttachmentDTO ToDTO(AttachmentViewModel vm)
        {
            Mapper<AttachmentViewModel, AttachmentDTO>.CreateMap();
            var map = Mapper<AttachmentViewModel, AttachmentDTO>.Map(vm);
            return map;
        }
    }
}
