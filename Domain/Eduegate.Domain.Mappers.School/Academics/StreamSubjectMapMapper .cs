using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class StreamSubjectMapMapper : DTOEntityDynamicMapper
    {
        public static StreamSubjectMapMapper Mapper(CallContext context)
        {
            var mapper = new StreamSubjectMapMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<StreamSubjectMapDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }
        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private StreamSubjectMapDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.StreamSubjectMaps.Where(x => x.StreamID == IID)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.Stream)
                    .AsNoTracking()
                    .FirstOrDefault();

                var subMapEntity = dbContext.StreamSubjectMaps
                   .Where(x => x.StreamID == IID)
                   .Include(i => i.Subject)
                   .AsNoTracking().ToList();

                var optionalsubEntity = dbContext.StreamOptionalSubjectMaps
                   .Where(x => x.StreamID == IID)
                   .Include(i => i.Subject)
                   .AsNoTracking().ToList();

                var dto = new StreamSubjectMapDTO()
                {
                    StreamID = entity.StreamID,
                    StreamSubjectMapIID = entity.StreamSubjectMapIID,
                    //IsOptionalSubject = entity.IsOptionalSubject,
                    SchoolID = entity.SchoolID,
                    AcademicYearID = entity.AcademicYearID,
                    //AcademicYearName = entity.AcademicYearID.HasValue ? entity.AcademicYear.Description + " (" + entity.AcademicYear.AcademicYearCode + ")" : null,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    UpdatedBy = entity.UpdatedBy
                };

                dto.Subject = new List<KeyValueDTO>();
                if (subMapEntity != null)
                {
                    foreach (var map in subMapEntity)
                    {
                        dto.Subject.Add(new KeyValueDTO()
                        {
                            Key = map.SubjectID.ToString(),
                            Value = map.Subject.SubjectName,
                        });
                    }
                }

                dto.OptionalSubject = new List<KeyValueDTO>();
                if (optionalsubEntity != null)
                {
                    foreach (var optionalmap in optionalsubEntity)
                    {
                        dto.OptionalSubject.Add(new KeyValueDTO()
                        {
                            Key = optionalmap.SubjectID.ToString(),
                            Value = optionalmap.Subject.SubjectName,
                        });
                    }
                }

                return dto;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as StreamSubjectMapDTO;

            //convert the dto to entity
            using (var dbContext = new dbEduegateSchoolContext())
            {
                //Check duplicate entry of data when create stream subject map
                if(toDto.StreamSubjectMapIID == 0)
                {
                    var compulsorySubjects = dbContext.StreamSubjectMaps.Where(x => x.StreamID == toDto.StreamID && x.SchoolID == _context.SchoolID).AsNoTracking().FirstOrDefault();
                    if (compulsorySubjects != null)
                    {
                        throw new Exception("subject map is already exist for this stream. please use edit option");
                    }                
                }

                //delete old list data and recreate new
                else
                {
                    var optionalSubjects = dbContext.StreamOptionalSubjectMaps.Where(x => x.StreamID == toDto.StreamID).AsNoTracking().ToList();
                    var compulsorySubjects = dbContext.StreamSubjectMaps.Where(x => x.StreamID == toDto.StreamID).AsNoTracking().ToList();

                    foreach (var del in optionalSubjects)
                    {
                        dbContext.StreamOptionalSubjectMaps.Remove(del);
                    }

                    foreach (var delete in compulsorySubjects)
                    {
                        dbContext.StreamSubjectMaps.Remove(delete);
                    }
                    dbContext.SaveChanges();
                }

                if (toDto.StreamCompulsorySubjectMap.Count > 0)
                {
                    foreach (var map in toDto.StreamCompulsorySubjectMap)
                    {
                        if (map.Subject.Key != null)
                        {
                            var entity = new StreamSubjectMap()
                            {
                                //StreamSubjectMapIID = toDto.StreamSubjectMapIID,
                                StreamID = toDto.StreamID,
                                SubjectID = int.Parse(map.Subject.Key),
                                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                                //IsOptionalSubject = toDto.IsOptionalSubject,
                                CreatedBy = toDto.StreamSubjectMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                                UpdatedBy = toDto.StreamSubjectMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                                CreatedDate = toDto.StreamSubjectMapIID == 0 ? DateTime.Now.Date : dto.CreatedDate,
                                UpdatedDate = toDto.StreamSubjectMapIID > 0 ? DateTime.Now.Date : dto.UpdatedDate,
                                OrderBy = map.OrderBy,
                            };

                            dbContext.StreamSubjectMaps.Add(entity);
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                            
                        }
                    }
                    dbContext.SaveChanges();
                }

                if (toDto.StreamOptionalSubjectMap.Count > 0)
                {
                    foreach (var optionalMap in toDto.StreamOptionalSubjectMap)
                    {
                        if (optionalMap.OptionalSubject.Key != null)
                        {
                            var optionalSubject = new StreamOptionalSubjectMap()
                            {
                                //StreamOptionalSubjectIID = toDto.StreamSubjectMapIID,
                                StreamID = toDto.StreamID,
                                SubjectID = int.Parse(optionalMap.OptionalSubject.Key),
                                CreatedBy = toDto.StreamSubjectMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                                UpdatedBy = toDto.StreamSubjectMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                                CreatedDate = toDto.StreamSubjectMapIID == 0 ? DateTime.Now.Date : dto.CreatedDate,
                                UpdatedDate = toDto.StreamSubjectMapIID > 0 ? DateTime.Now.Date : dto.UpdatedDate,
                                OrderBy = optionalMap.OrderBy,
                            };

                            dbContext.Entry(optionalSubject).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            
                        }
                    }
                    dbContext.SaveChanges();
                }

                return ToDTOString(ToDTO((long)toDto.StreamID));
            }
        }


        public List<KeyValueDTO> GetStreamCompulsorySubjects(byte? streamGroupID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var streamSubList = new List<KeyValueDTO>();

                var streamSubjects = dbContext.StreamSubjectMaps.Where(x => x.StreamID == streamGroupID)
                    .Include(i => i.Subject)
                    .AsNoTracking().ToList();

                foreach (var dat in streamSubjects)
                {
                    streamSubList.Add(new KeyValueDTO
                    {
                        Key = dat.SubjectID.ToString(),
                        Value = dat.Subject.SubjectName,
                    });
                }

                return streamSubList;
            }
        }

        public List<KeyValueDTO> GetStreamOptionalSubjects(byte? streamGroupID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var streamOptSubList = new List<KeyValueDTO>();

                var streamSubjects = dbContext.StreamOptionalSubjectMaps.Where(x => x.StreamID == streamGroupID)
                    .Include(x=> x.Subject)
                    .AsNoTracking().ToList();

                foreach (var dat in streamSubjects)
                {
                    streamOptSubList.Add(new KeyValueDTO
                    {
                        Key = dat.SubjectID.ToString(),
                        Value = dat.Subject.SubjectName,
                    });
                }

                return streamOptSubList;
            }
        }

        public List<StreamDTO> GetFullStreamListDatas()
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var stremDTO = new List<StreamDTO>();

                var streams = dbContext.Streams.Where(s => s.StreamID != 0 && s.IsActive == true)
                    .Include(i => i.StreamSubjectMaps).ThenInclude(i => i.Subject)
                    .Include(i => i.StreamOptionalSubjectMaps).ThenInclude(i => i.Subject)
                    .AsNoTracking().ToList();

                foreach (var stream in streams)
                {
                    var subjectList = new List<KeyValueDTO>();
                    var optionalSubjectList = new List<KeyValueDTO>();

                    foreach (var cum_map in stream.StreamSubjectMaps)
                    {
                        subjectList.Add(new KeyValueDTO()
                        {
                            Key = cum_map.SubjectID.ToString(),
                            Value = cum_map.Subject?.SubjectName,
                        });
                    }

                    foreach (var opt_map in stream.StreamOptionalSubjectMaps)
                    {
                        optionalSubjectList.Add(new KeyValueDTO()
                        {
                            Key = opt_map.SubjectID.ToString(),
                            Value = opt_map.Subject?.SubjectName,
                        });
                    }

                    stremDTO.Add(new StreamDTO()
                    {
                        StreamID = stream.StreamID,
                        Code = stream.Code,
                        Description = stream.Description,
                        OptionalSubjects = optionalSubjectList,
                        CompulsorySubjects = subjectList,
                    });
                }

                return stremDTO;
            }

        } 

    }
}