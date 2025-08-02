using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels
{
    public class NewsViewModel : BaseMasterViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("NewsID")]
        public long NewsIID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.NewsType")]
        [CustomDisplay("NewsType")]
        public string Type { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("NewsTitle")]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 50o!")]
        public string Title { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.RichTextEditor)]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [CustomDisplay("NewsShortContent")]
        public string NewsContentShort { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.RichTextEditor)]
        [MaxLength(1000, ErrorMessage = "Maximum Length should be within 1000!")]
        [CustomDisplay("NewsContent")]
        public string NewsContent { get; set; }
        //[ControlType(Framework.Enums.ControlTypes.DatePicker)]
        //[DisplayName("Date")]
        public string Date { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.FileUpload)]
        [CustomDisplay("NewsImage")]
        [FileUploadInfo("Mutual/UploadImages", EduegateImageTypes.News, "ImageUrl", "")]
        public string NewsImage { get; set; }
        public int CompanyID { get; set; }

        public static NewsViewModel FromDTO(NewsDTO dto)
        {
            return new NewsViewModel()
                {
                    Date = dto.Date,  //dto.Date.HasValue ? dto.Date.Value.ToString(ConfigurationManager.AppSettings["DateFormat"]) : string.Empty,
                    NewsIID = dto.NewsIID,
                    ImageUrl = dto.ImageUrl,
                    NewsContent = dto.NewsContent,
                    NewsContentShort = dto.NewsContentShort,
                    Title = dto.Name,
                    Type = ((int) dto.NewsType).ToString(),
                    TimeStamps = dto.TimeStamps,
                    CompanyID = (int)dto.CompanyID
                };
        }

        public static NewsDTO ToDTO(NewsViewModel dto)
        {
            return new NewsDTO()
                {
                    Date = dto.Date, // string.IsNullOrEmpty(dto.Date) ? (DateTime?)null : DateTime.ParseExact(dto.Date, ConfigurationManager.AppSettings["DateFormat"], CultureInfo.InvariantCulture),
                    NewsIID = dto.NewsIID,
                    ImageUrl = dto.ImageUrl,
                    NewsContent = dto.NewsContent,
                    NewsContentShort = dto.NewsContentShort,
                    Name = dto.Title,
                    NewsType = (Eduegate.Services.Contracts.Enums.NewsTypes)int.Parse(dto.Type),
                    TimeStamps = dto.TimeStamps,
                    CompanyID = (int)dto.CompanyID
                };
        }

    }
}