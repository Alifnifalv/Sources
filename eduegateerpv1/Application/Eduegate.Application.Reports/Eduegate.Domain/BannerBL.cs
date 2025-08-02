using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain
{
    public class BannerBL
    {
        private BannerRepository bannerRepository;
        private Eduegate.Framework.CallContext _callContext { get; set; }

        public BannerBL(Eduegate.Framework.CallContext context)
        {
            _callContext = context;
            bannerRepository =  new BannerRepository();
        }

        public List<BannerMasterDTO> GetBanners()
        {
            var dtos = new List<BannerMasterDTO>();

            var banners = bannerRepository.GetBanners();

            foreach (var banner in banners)
            {
                dtos.Add(FromEntity(banner));
            }

            return dtos;
        }

        public List<BannerMasterDTO> GetBanners(BannerTypes bannerType, BannerStatuses status)
        {
            var dtos = new List<BannerMasterDTO>();

            var banners = bannerRepository.GetBanners((int)bannerType, (int)status, _callContext.IsNotNull() && _callContext.CompanyID.HasValue ? _callContext.CompanyID.Value : default(int));

            foreach (var banner in banners)
            {
                dtos.Add(FromEntity(banner));
            }

            return dtos;
        }

        public BannerMasterDTO GetBanner(long bannerID)
        {
            var banner = bannerRepository.GetBanner(bannerID, _callContext.CompanyID.HasValue?(int) _callContext.CompanyID : default(int));
            return FromEntity(banner);
        }

        public BannerMasterDTO SaveBanner(BannerMasterDTO banner)
        {
            var entity = ToEntity(banner, _callContext);
            var updatedEntity = bannerRepository.SaveBanner(entity, _callContext.CompanyID.HasValue? (int)_callContext.CompanyID : default(int));
            return FromEntity(updatedEntity);
        }

        public static BannerMasterDTO FromEntity(Banner entity)
        {
            return new BannerMasterDTO()
            {
                BannerIID = entity.BannerIID,
                BannerName = entity.BannerName,
                BannerTypeID = entity.BannerTypeID,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                BannerFile = entity.BannerFile,
                Frequency = entity.Frequency.HasValue ? entity.Frequency.Value : byte.MinValue,
                Link = entity.BannerActionLinkParameters,
                StatusID = entity.StatusID.Value,
                Target = entity.Target,
                TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                UpdatedBy = entity.CreatedBy,
                UpdatedDate = entity.UpdatedDate,
                ReferenceID = entity.ReferenceID,
                ActionLinkTypeID = entity.ActionLinkTypeID,
                SerialNo= entity.SerialNo.HasValue ? entity.SerialNo.Value : 0,
                CompanyID = (int)entity.CompanyID,
            };
        }

        public static Banner ToEntity(BannerMasterDTO dto, CallContext context)
        {
            var entity = new Banner()
            {
                BannerIID = dto.BannerIID,
                BannerName = dto.BannerName,
                BannerTypeID = dto.BannerTypeID.Value,
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                BannerFile = dto.BannerFile,
                Frequency = dto.Frequency,
                BannerActionLinkParameters = dto.Link,
                StatusID = dto.StatusID,
                Target = dto.Target,
                TimeStamps = string.IsNullOrEmpty(dto.TimeStamps) ? null : Convert.FromBase64String(dto.TimeStamps),
                UpdatedBy = dto.CreatedBy,
                UpdatedDate = dto.CreatedDate,
                ReferenceID = dto.ReferenceID,
                ActionLinkTypeID = dto.ActionLinkTypeID,
                SerialNo = dto.SerialNo,
                CompanyID = dto.CompanyID > 0 ? dto.CompanyID : context.CompanyID,
            };

            if (entity.BannerIID == 0)
            {
                entity.CreatedBy = int.Parse(context.LoginID.ToString());
                entity.CreatedDate = DateTime.Now;
            }

            return entity;
        }
    }
}
