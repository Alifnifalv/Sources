using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Web.Library.ViewModels.Common;
using Eduegate.Services.Contracts.Commons;

namespace Eduegate.Web.Library.ViewModels
{
    public class SeoMetadataViewModel : BaseMasterViewModel
    {
        public List<CultureDataInfoViewModel> SuppertedCultures { get; set; }
        public long SEOMetadataIID { get; set; }
        public MultiLanguageText PageTitle { get; set; }
        public MultiLanguageText MetaKeywords { get; set; }
        public MultiLanguageText MetaDescription { get; set; }
        public MultiLanguageText UrlKey { get; set; }

        public void InitializeCultureData(List<CultureDataInfoViewModel> datas)
        {
            SuppertedCultures = datas;
            PageTitle = new MultiLanguageText(datas);
            MetaKeywords = new MultiLanguageText(datas);
            MetaDescription = new MultiLanguageText(datas);
            UrlKey = new MultiLanguageText(datas);
        }

        public static SeoMetadataViewModel FromDTO(SeoMetadataDTO dto, List<CultureDataInfoDTO> cultures)
        {
            if (dto == null)
                return null;
            //Mapper<SeoMetadataDTO, SeoMetadataViewModel>.CreateMap();
            //var vm = Mapper<SeoMetadataDTO, SeoMetadataViewModel>.Map(dto);
            var cultureVm = CultureDataInfoViewModel.FromDTO(cultures);

            var vm = new SeoMetadataViewModel()
            {
                MetaDescription = new MultiLanguageText() { Text = dto.MetaDescription, CultureDatas = CultureDataInfoViewModel.FromDTO(cultures) },
                MetaKeywords = new MultiLanguageText() { Text = dto.MetaKeywords, CultureDatas = CultureDataInfoViewModel.FromDTO(cultures) },
                PageTitle = new MultiLanguageText() { Text = dto.PageTitle, CultureDatas = CultureDataInfoViewModel.FromDTO(cultures) },
                UrlKey = new MultiLanguageText() { Text = dto.UrlKey, CultureDatas = CultureDataInfoViewModel.FromDTO(cultures) },

                TimeStamps = dto.TimeStamps,
                SEOMetadataIID = dto.SEOMetadataIID,
            };

            if (cultures != null)
            {
                bool isFirst = true;

                foreach (var culture in cultureVm)
                {
                    var cultureDTO = dto.CultureDatas.FirstOrDefault(a => a.CultureID == culture.CultureID);

                    if (isFirst && cultureDTO == null)
                    {
                        cultureDTO = new SeoMetadataCultureDataDTO()
                        {
                            CultureID = culture.CultureID,
                            MetaDescription = dto.MetaDescription,
                            MetaKeywords = dto.MetaKeywords,
                            PageTitle = dto.PageTitle,
                            UrlKey = dto.UrlKey,
                            SEOMetadataID = dto.SEOMetadataIID,
                        };

                        isFirst = false;
                        continue;
                    }
                    vm.PageTitle.SetValueByCultureID(culture, cultureDTO == null ? string.Empty : cultureDTO.PageTitle, cultureDTO == null ? null : cultureDTO.TimeStamps);
                    vm.MetaDescription.SetValueByCultureID(culture, cultureDTO == null ? string.Empty : cultureDTO.MetaDescription, cultureDTO == null ? null : cultureDTO.TimeStamps);
                    vm.MetaKeywords.SetValueByCultureID(culture, cultureDTO == null ? string.Empty : cultureDTO.MetaKeywords, cultureDTO == null ? null : cultureDTO.TimeStamps);
                    vm.UrlKey.SetValueByCultureID(culture, cultureDTO == null ? string.Empty : cultureDTO.UrlKey, cultureDTO == null ? null : cultureDTO.TimeStamps);
                }
            }

            return vm;
        }

        public static SeoMetadataDTO ToDTO(SeoMetadataViewModel vm, List<CultureDataInfoViewModel> cultures)
        {
            Mapper<SeoMetadataViewModel, SeoMetadataDTO>.CreateMap();
            var dto = Mapper<SeoMetadataViewModel, SeoMetadataDTO>.Map(vm);
            dto.CultureDatas = ToCultureDTO(vm, cultures);
            return dto;
        }

        public static List<SeoMetadataCultureDataDTO> ToCultureDTO(SeoMetadataViewModel vm, List<CultureDataInfoViewModel> cultures)
        {
            var dtos = new List<SeoMetadataCultureDataDTO>();
            bool isFrist = true;

            foreach (var culture in cultures)
            {
                //Assume that first one is the default culture which will be there by default.
                if (isFrist)
                {
                    isFrist = false;
                    continue;
                }

                dtos.Add(new SeoMetadataCultureDataDTO()
                {
                    CultureID = culture.CultureID,
                    SEOMetadataID = vm.SEOMetadataIID,
                    PageTitle = vm.PageTitle.GetValueByCultureID(culture.CultureID),
                    MetaDescription = vm.MetaDescription.GetValueByCultureID(culture.CultureID),
                    MetaKeywords = vm.MetaKeywords.GetValueByCultureID(culture.CultureID),
                    UrlKey = vm.UrlKey.GetValueByCultureID(culture.CultureID),
                    TimeStamps = vm.PageTitle.GetTimeStampByCultureID(culture.CultureID),
                });
            }

            return dtos;
        }
    }
}
