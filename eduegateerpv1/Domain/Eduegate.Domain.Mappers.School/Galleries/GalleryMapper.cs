using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Galleries;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Galleries
{
    public class GalleryMapper : DTOEntityDynamicMapper
    {
        public static GalleryMapper Mapper(CallContext context)
        {
            var mapper = new GalleryMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<GalleryDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }


        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private GalleryDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Galleries.Where(x => x.GalleryIID == IID)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.GalleryAttachmentMaps)
                    .AsNoTracking()
                    .FirstOrDefault();

                var GalleryDTO = new GalleryDTO()
                {
                    GalleryIID = entity.GalleryIID,
                    SchoolID = entity.SchoolID,
                    Description = entity.Description,
                    GalleryDate = entity.GalleryDate,
                    StartDate = entity.StartDate,
                    ExpiryDate = entity.ExpiryDate,
                    ISActive = entity.ISActive,
                    AcademicYearID = entity.AcademicYearID,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                    AcademicYear = entity.AcademicYearID.HasValue ? new KeyValueDTO() { Key = entity.AcademicYearID.Value.ToString(), Value = entity.AcademicYear.Description + " " + "( " + entity.AcademicYear.AcademicYearCode + ")" } : new KeyValueDTO(),
                };


                GalleryDTO.GalleryAttachmentMaps = new List<GalleryAttachmentMapDTO>();
                if (entity.GalleryAttachmentMaps.Count > 0)
                {
                    foreach (var attachment in entity.GalleryAttachmentMaps)
                    {
                        if (attachment.AttachmentContentID.HasValue || !string.IsNullOrEmpty(attachment.AttachmentName))
                        {
                            GalleryDTO.GalleryAttachmentMaps.Add(new GalleryAttachmentMapDTO()
                            {
                                GalleryAttachmentMapIID = attachment.GalleryAttachmentMapIID,
                                GalleryID = attachment.GalleryID.HasValue ? attachment.GalleryID : null,
                                AttachmentContentID = attachment.AttachmentContentID,
                                AttachmentName = attachment.AttachmentName,
                                CreatedBy = attachment.CreatedBy,
                                CreatedDate = attachment.CreatedDate,
                            });
                        }
                    }
                }

                return GalleryDTO;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as GalleryDTO;

            if (toDto.StartDate > toDto.ExpiryDate)
            {
                throw new Exception("From-date cannot be greater than to-date!");
            }

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = new Gallery()
                {
                    GalleryIID = toDto.GalleryIID,
                    SchoolID = toDto.SchoolID.HasValue ? toDto.SchoolID : Convert.ToByte(_context.SchoolID),
                    AcademicYearID = toDto.AcademicYearID.HasValue ? toDto.AcademicYearID : _context.AcademicYearID,
                    GalleryDate = toDto.GalleryDate,
                    StartDate = toDto.StartDate,
                    ExpiryDate = toDto.ExpiryDate,
                    ISActive = toDto.ISActive,
                    Description = toDto.Description,
                };

                var IIDs = toDto.GalleryAttachmentMaps
                    .Select(a => a.GalleryAttachmentMapIID).ToList();

                //delete maps
                var entities = dbContext.GalleryAttachmentMaps.Where(x =>
                    x.GalleryID == entity.GalleryIID &&
                    !IIDs.Contains(x.GalleryAttachmentMapIID)).AsNoTracking().ToList();

                if (entities.IsNotNull())
                    dbContext.GalleryAttachmentMaps.RemoveRange(entities);

                foreach (var attachment in toDto.GalleryAttachmentMaps)
                {
                    if (attachment.AttachmentContentID.HasValue || !string.IsNullOrEmpty(attachment.AttachmentName))
                    {
                        entity.GalleryAttachmentMaps.Add(new GalleryAttachmentMap()
                        {
                            GalleryAttachmentMapIID = attachment.GalleryAttachmentMapIID,
                            GalleryID = entity.GalleryIID,
                            AttachmentContentID = attachment.AttachmentContentID,
                            AttachmentName = attachment.AttachmentName,
                            CreatedBy = attachment.CreatedBy,
                            UpdatedBy = attachment.UpdatedBy,
                            CreatedDate = attachment.CreatedDate,
                            UpdatedDate = attachment.UpdatedDate,
                        });
                    }
                }

                dbContext.Galleries.Add(entity);

                if (entity.GalleryIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                foreach (var galleryMap in entity.GalleryAttachmentMaps)
                {
                    if (galleryMap.GalleryAttachmentMapIID != 0)
                    {
                        dbContext.Entry(galleryMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    else
                    {
                        dbContext.Entry(galleryMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                }

                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.GalleryIID));
            }
        }

        public List<GalleryDTO> GetGalleryView(long academicYearID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var currentDate = DateTime.Now;

                var galleryDatas = dbContext.Galleries
                    .Where(x => x.AcademicYearID == academicYearID && x.ISActive == true && (x.StartDate <= currentDate.Date && x.ExpiryDate >= currentDate.Date))
                    .Include(i => i.GalleryAttachmentMaps)
                    .OrderByDescending(o => o.GalleryDate)
                    .AsNoTracking().ToList();

                var galleryList = (from gallery in galleryDatas
                                   select new GalleryDTO()
                                   {
                                       Description = gallery.Description,
                                       GalleryDate = gallery.GalleryDate,
                                       GalleryAttachmentMaps = (from aat in gallery.GalleryAttachmentMaps

                                                                select new GalleryAttachmentMapDTO()
                                                                {
                                                                    GalleryAttachmentMapIID = aat.GalleryAttachmentMapIID,
                                                                    GalleryID = aat.GalleryID,
                                                                    AttachmentName = aat.AttachmentName,
                                                                    AttachmentContentID = aat.AttachmentContentID,
                                                                }).ToList(),
                                   }).ToList();

                return galleryList;
            }
        }

    }
}