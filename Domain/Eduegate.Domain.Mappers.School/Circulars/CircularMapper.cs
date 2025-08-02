using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Mappers.Notification.Notifications;
using Eduegate.Domain.Mappers.Notification.Notifications.Helpers;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Enums.Notifications;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Services.Contracts.School.Circulars;
using Eduegate.Services.Contracts.School.Students;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Eduegate.Domain.Mappers.School.Circulars
{
    public class CircularMapper : DTOEntityDynamicMapper
    {
        public static CircularMapper Mapper(CallContext context)
        {
            var mapper = new CircularMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<CircularDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private CircularDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Circulars.Where(X => X.CircularIID == IID)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.CircularMaps).ThenInclude(i => i.Class)
                    .Include(i => i.CircularMaps).ThenInclude(i => i.Section)
                    .Include(i => i.CircularMaps).ThenInclude(i => i.Departments1)
                    .Include(i => i.CircularUserTypeMaps).ThenInclude(i => i.CircularUserType)
                    .Include(i => i.CircularAttachmentMaps)
                    .AsNoTracking()
                    .FirstOrDefault();

                var circularDTO = new CircularDTO()
                {
                    CircularIID = entity.CircularIID,
                    SchoolID = entity.SchoolID,
                    AcadamicYearID = entity.AcadamicYearID,
                    CircularTypeID = entity.CircularTypeID,
                    CircularPriorityID = entity.CircularPriorityID,
                    CircularDate = entity.CircularDate,
                    ExpiryDate = entity.ExpiryDate,
                    CircularCode = entity.CircularCode,
                    Title = entity.Title,
                    ShortTitle = entity.ShortTitle,
                    Message = entity.Message,
                    CircularStatusID = entity.CircularStatusID,
                    AttachmentReferenceID = entity.AttachmentReferenceID,
                    AcademicYear = entity.AcadamicYearID.HasValue ? new KeyValueDTO() { Key = entity.AcadamicYearID.Value.ToString(), Value = entity.AcademicYear.AcademicYearCode + " ( " + entity.AcademicYear.Description + " )" } : new KeyValueDTO(),
                    IsSendPushNotification = false,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                };

                foreach (var map in entity.CircularMaps)
                {
                    if (!circularDTO.CircularMaps.Any(x => x.ClassID == map.ClassID &&
                        x.SectionID == map.SectionID && x.DepartmentID == map.DepartmentID
                        && x.AllClass == map.AllClass && x.AllDepartment == map.AllDepartment &&
                        x.AllSection == map.AllSection))
                    {
                        circularDTO.CircularMaps.Add(new CircularMapDTO()
                        {
                            CircularMapIID = map.CircularMapIID,
                            ClassID = map.ClassID,
                            AllClass = map.AllClass,
                            AllDepartment = map.AllDepartment,
                            AllSection = map.AllSection,
                            SectionID = map.SectionID,
                            DepartmentID = map.DepartmentID,
                            Class = map.ClassID.HasValue ? new KeyValueDTO() { Key = map.ClassID.Value.ToString(), Value = map.Class.ClassDescription } : new KeyValueDTO() { Key = null, Value = "All Classes" },
                            Section = map.SectionID.HasValue ? new KeyValueDTO() { Key = map.SectionID.Value.ToString(), Value = map.Section.SectionName } : new KeyValueDTO() { Key = null, Value = "All Section" },
                            Department = map.DepartmentID.HasValue ? new KeyValueDTO() { Key = map.DepartmentID.Value.ToString(), Value = map.Departments1.DepartmentName } : new KeyValueDTO() { Key = null, Value = "All Departments" },
                            CreatedBy = map.CreatedBy,
                            CreatedDate = map.CreatedDate,
                            UpdatedBy = map.UpdatedBy,
                            UpdatedDate = map.UpdatedDate,
                        });
                    }
                }

                foreach (var map in entity.CircularUserTypeMaps)
                {
                    circularDTO.CircularUserTypeMaps.Add(new CircularUserTypeMapDTO()
                    {
                        CircularUserTypeMapIID = map.CircularUserTypeMapIID,
                        CircularUserType = map.CircularUserTypeID.HasValue ? new KeyValueDTO() { Key = map.CircularUserTypeID.Value.ToString(), Value = map.CircularUserType.UserType } : new KeyValueDTO() { Key = null, Value = "All User" },
                    });
                }

                circularDTO.CircularAttachmentMaps = new List<CircularAttachmentMapDTO>();

                if (entity.CircularAttachmentMaps.Count > 0)
                {
                    foreach (var attachment in entity.CircularAttachmentMaps)
                    {
                        if (attachment.AttachmentReferenceID.HasValue || !string.IsNullOrEmpty(attachment.AttachmentName))
                        {
                            circularDTO.CircularAttachmentMaps.Add(new CircularAttachmentMapDTO()
                            {
                                CircularAttachmentMapIID = attachment.CircularAttachmentMapIID,
                                CircularID = attachment.CircularID.HasValue ? attachment.CircularID : null,
                                AttachmentReferenceID = attachment.AttachmentReferenceID,
                                AttachmentName = attachment.AttachmentName,
                                AttachmentDescription = attachment.AttachmentDescription,
                                Notes = attachment.Notes,
                                CreatedBy = attachment.CreatedBy,
                                CreatedDate = attachment.CreatedDate,
                            });
                        }
                    }
                }

                return circularDTO;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as CircularDTO;

            if (toDto.CircularMaps.Count == 0)
            {
                throw new Exception("Please select atleast one Class or Department");
            }

            if (toDto.CircularUserTypeMaps.Count == 0)
            {
                throw new Exception("Please select atleast one User");
            }

            if (toDto.Message == null || toDto.Message == "")
            {
                throw new Exception("Message cannot be left empty");
            }

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (toDto.CircularIID != 0)
                {
                    var circularData = dbContext.Circulars.Where(X => X.CircularIID == toDto.CircularIID).AsNoTracking().FirstOrDefault();
                    if (circularData != null)
                    {
                        if (circularData.CircularStatusID == 2)
                        {
                            throw new Exception("The circular can't be edited; it has already been published!");
                        }
                    }
                }
            }

            try
            {
                toDto.CircularIID = SaveAndSendCircular(toDto);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"Circular Save and Mail, Push Notification failed. Error message: {errorMessage}", ex);

            }

            toDto.CircularAttachmentMaps = new List<CircularAttachmentMapDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var circularAttachmentMaps = dbContext.CircularAttachmentMaps.Where(X => X.CircularID == toDto.CircularIID).AsNoTracking().ToList();
                if (circularAttachmentMaps.Count() > 0)
                {
                    foreach (var attachment in circularAttachmentMaps)
                    {
                        if (attachment.AttachmentReferenceID.HasValue || !string.IsNullOrEmpty(attachment.AttachmentName))
                        {
                            toDto.CircularAttachmentMaps.Add(new CircularAttachmentMapDTO()
                            {
                                CircularAttachmentMapIID = attachment.CircularAttachmentMapIID,
                                CircularID = attachment.CircularID.HasValue ? attachment.CircularID : null,
                                AttachmentReferenceID = attachment.AttachmentReferenceID,
                                AttachmentName = attachment.AttachmentName,
                                AttachmentDescription = attachment.AttachmentDescription,
                                Notes = attachment.Notes,
                                CreatedBy = attachment.CreatedBy,
                                CreatedDate = attachment.CreatedDate,
                            });
                        }
                    }
                }
            }

            return ToDTOString(ToDTO(toDto.CircularIID));
        }

        private long SaveAndSendCircular(CircularDTO toDto)
        {
            var entity = SaveCircular(toDto);

            //Send Circular mail only when status is publish
            if (toDto.CircularStatusID != null && toDto.CircularStatusID == (byte?)CircularStatuses.Publish)
            {
                if (toDto.IsSendPushNotification == true)
                {
                    SendAndSavePushNotification(entity, toDto);
                }

                toDto.CircularIID = entity.CircularIID;
                
                //Send Circular Notification Mail
                var result = SendCircular(toDto);

            }
            return entity.CircularIID;
        }

        private void SendAndSavePushNotification(Circular entity, CircularDTO dto)
        {
            var loginIDs = new List<long?>();
            List<long?> departmentIds = null;

            if (entity.CircularStatusID.HasValue && entity.CircularStatusID == 2)
            {
                using (var dbContext = new dbEduegateSchoolContext())
                {
                    if (dto.CircularUserTypeMaps.Any(x => x.CircularUserType.Value.ToLower().Contains("all")))
                    {
                        loginIDs = GetStaffLoginIDs(entity, null);

                        loginIDs.AddRange(GetParentLoginIDs(entity));
                    }
                    else if (dto.CircularUserTypeMaps.Any(x => x.CircularUserType.Value.ToLower().Contains("parent")))
                    {
                        loginIDs = GetParentLoginIDs(entity);
                    }
                    else if(dto.CircularUserTypeMaps.Any(x => x.CircularUserType.Value.ToLower().Contains("staff")))
                    {
                        // Get department IDs if specific departments are selected
                        if (dto.CircularMaps.Any(x => x.AllDepartment == false))
                        {
                            departmentIds = dto.CircularMaps
                                .Select(x =>
                                {
                                    if (!string.IsNullOrEmpty(x.Department?.Key))
                                    {
                                        return (long?)long.Parse(x.Department.Key);
                                    }
                                    return null;
                                }).Where(id => id.HasValue).ToList();
                        }

                        loginIDs.AddRange(GetStaffLoginIDs(entity, departmentIds));
                    }

                    var message = "Circular " + entity.CircularCode + " has been uploaded.";
                    var title = "New Circular";
                    // Optimize: Fetch parents and staff in one go to reduce database queries
                    var parentLoginIDs = dbContext.Parents
                                                   .Where(p => loginIDs.Contains(p.LoginID))
                                                   .Select(p => p.LoginID)
                                                   .ToHashSet();

                    var staffLoginIDs = dbContext.Employees
                                                  .Where(e => loginIDs.Contains(e.LoginID))
                                                  .Select(e => e.LoginID)
                                                  .ToHashSet();

                    foreach (var login in loginIDs)
                    {
                        long toLoginID = Convert.ToInt32(login);
                        long fromLoginID = _context?.LoginID != null ? (long)_context.LoginID : 2;

                        Dictionary<string, string> settings = parentLoginIDs.Contains(login)
                            ? NotificationSetting.GetParentAppSettings()
                            : staffLoginIDs.Contains(login)
                                ? NotificationSetting.GetEmployeeAppSettings()
                                : null;

                        PushNotificationMapper.SendAndSavePushNotification(toLoginID, fromLoginID, message, title, settings);
                    }
                }
            }
        }

        private async Task SendCircular(CircularDTO toDto)
        {
          var t = await Task.Run(() => CircularEmailProcess(toDto));
        }


        // Get Staff Mail IDs
        public List<string> GetActiveStaffWorkingEmailIDs(List<long?> departmentIDs)
        {
            var emailIDs = new List<string>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var query = dbContext.Employees.Where(x => x.IsActive == true && x.WorkEmail != null && x.BranchID == _context.SchoolID);

                if (departmentIDs != null && departmentIDs.Any())
                {
                    query = query.Where(x => departmentIDs.Contains(x.DepartmentID));
                }

                emailIDs = query
                    .Select(x => x.WorkEmail)
                    .ToList();
            }
            return emailIDs;
        }

        private Circular SaveCircular(CircularDTO toDto)
        {
            MutualRepository mutualRepository = new MutualRepository();
            Entity.Models.Settings.Sequence sequence = new Entity.Models.Settings.Sequence();

            if (toDto.CircularIID == 0)
            {
                try
                {
                    sequence = mutualRepository.GetNextSequence("CircularCode",null);
                }
                catch (Exception ex)
                {
                    Eduegate.Logger.LogHelper<CircularMapper>.Fatal(ex.Message, ex);
                    throw new Exception("Please generate sequence with 'CircularCode'");
                }
            }

            //convert the dto to entity and pass to the repository.
            var entity = new Circular()
            {
                CircularIID = toDto.CircularIID,
                CircularCode = toDto.CircularIID == 0 ? sequence.Prefix + sequence.LastSequence : toDto.CircularCode,
                SchoolID = toDto.SchoolID.HasValue ? toDto.SchoolID : Convert.ToByte(_context.SchoolID),
                AcadamicYearID = toDto.AcadamicYearID.HasValue ? toDto.AcadamicYearID : _context.AcademicYearID,
                CircularTypeID = toDto.CircularTypeID,
                CircularPriorityID = toDto.CircularPriorityID,
                CircularDate = toDto.CircularDate,
                ExpiryDate = toDto.ExpiryDate,
                Title = toDto.Title,
                ShortTitle = toDto.ShortTitle,
                Message = toDto.Message,
                CircularStatusID = toDto.CircularStatusID,
                AttachmentReferenceID = toDto.AttachmentReferenceID,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var IIDs = toDto.CircularAttachmentMaps
                    .Select(a => a.CircularAttachmentMapIID).ToList();

                //delete maps
                var entities = dbContext.CircularAttachmentMaps.Where(x =>
                    x.CircularID == entity.CircularIID &&
                    !IIDs.Contains(x.CircularAttachmentMapIID)).AsNoTracking().ToList();

                if (entities.IsNotNull())
                    dbContext.CircularAttachmentMaps.RemoveRange(entities);
            }

            entity.CircularAttachmentMaps = new List<CircularAttachmentMap>();

            if (toDto.CircularAttachmentMaps.Count > 0)
            {
                foreach (var attach in toDto.CircularAttachmentMaps)
                {
                    if (attach.AttachmentReferenceID.HasValue || !string.IsNullOrEmpty(attach.AttachmentName))
                    {
                        entity.CircularAttachmentMaps.Add(new CircularAttachmentMap()
                        {
                            CircularAttachmentMapIID = attach.CircularAttachmentMapIID,
                            CircularID = attach.CircularID.HasValue ? attach.CircularID : null,
                            AttachmentReferenceID = attach.AttachmentReferenceID,
                            AttachmentName = attach.AttachmentName,
                            AttachmentDescription = attach.AttachmentDescription,
                            Notes = attach.Notes,
                            CreatedBy = attach.CircularAttachmentMapIID == 0 ? (int)_context.LoginID : toDto.CreatedBy,
                            UpdatedBy = attach.CircularAttachmentMapIID > 0 ? (int)_context.LoginID : toDto.UpdatedBy,
                            CreatedDate = attach.CircularAttachmentMapIID == 0 ? DateTime.Now : toDto.CreatedDate,
                            UpdatedDate = attach.CircularAttachmentMapIID > 0 ? DateTime.Now : toDto.UpdatedDate,
                        });
                    }
                }
            }

            if (toDto.CircularIID == 0)
            {
                entity.CreatedBy = Convert.ToInt32(_context.LoginID);
                entity.CreatedDate = DateTime.Now;
            }
            else
            {
                entity.UpdatedBy = Convert.ToInt32(_context.LoginID);
                entity.UpdatedDate = DateTime.Now;
            }

            foreach (var cirMap in toDto.CircularMaps)
            {
                if (!entity.CircularMaps.Any(x => x.ClassID == cirMap.ClassID &&
                   x.SectionID == cirMap.SectionID && x.DepartmentID == cirMap.DepartmentID
                   && x.AllClass == cirMap.AllClass && x.AllDepartment == cirMap.AllDepartment &&
                   x.AllSection == cirMap.AllSection))
                {
                    entity.CircularMaps.Add(new CircularMap()
                    {
                        CircularMapIID = cirMap.CircularMapIID,
                        ClassID = cirMap.ClassID,
                        AllClass = cirMap.AllClass,
                        AllDepartment = cirMap.AllDepartment,
                        AllSection = cirMap.AllSection,
                        SectionID = cirMap.SectionID,
                        DepartmentID = cirMap.DepartmentID,
                        CreatedBy = cirMap.CircularMapIID == 0 ? Convert.ToInt32(_context.LoginID) : cirMap.CreatedBy,
                        CreatedDate = cirMap.CircularMapIID == 0 ? DateTime.Now : cirMap.CreatedDate,
                        UpdatedBy = cirMap.CircularMapIID != 0 ? Convert.ToInt32(_context.LoginID) : cirMap.UpdatedBy,
                        UpdatedDate = cirMap.CircularMapIID != 0 ? DateTime.Now : cirMap.UpdatedDate,
                    });
                }
            }

            entity.CircularUserTypeMaps = new List<CircularUserTypeMap>();
            foreach (var userTypemap in toDto.CircularUserTypeMaps)
            {
                entity.CircularUserTypeMaps.Add(new CircularUserTypeMap()
                {
                    CircularUserTypeMapIID = userTypemap.CircularUserTypeMapIID,
                    CircularUserTypeID = userTypemap.CircularUserTypeID,
                    CreatedBy = userTypemap.CircularUserTypeMapIID == 0 ? Convert.ToInt32(_context.LoginID) : userTypemap.CreatedBy,
                    CreatedDate = userTypemap.CircularUserTypeMapIID == 0 ? DateTime.Now : userTypemap.CreatedDate,
                    UpdatedBy = userTypemap.CircularUserTypeMapIID != 0 ? Convert.ToInt32(_context.LoginID) : userTypemap.UpdatedBy,
                    UpdatedDate = userTypemap.CircularUserTypeMapIID != 0 ? DateTime.Now : userTypemap.UpdatedDate,
                });
            }

            using (var dbContext = new dbEduegateSchoolContext())
            {
                dbContext.Circulars.Add(entity);

                if (entity.CircularIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    using (var dbContext1 = new dbEduegateSchoolContext())
                    {
                        var mapEntities = dbContext1.CircularMaps
                            .Where(x => x.CircularID == toDto.CircularIID).AsNoTracking().ToList();

                        if (mapEntities != null && mapEntities.Count > 0)
                        {
                            dbContext1.CircularMaps.RemoveRange(mapEntities);
                        }

                        var typeEntities = dbContext1.CircularUserTypeMaps
                            .Where(x => x.CircularID == toDto.CircularIID).AsNoTracking().ToList();

                        if (typeEntities != null && typeEntities.Count > 0)
                        {
                            dbContext1.CircularUserTypeMaps.RemoveRange(typeEntities);
                        }

                        dbContext1.SaveChanges();
                    }

                    if (entity.CircularMaps.Count > 0)
                    {
                        foreach (var circularMap in entity.CircularMaps)
                        {
                            if (circularMap.CircularMapIID == 0)
                            {
                                dbContext.Entry(circularMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(circularMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }
                    }

                    if (entity.CircularUserTypeMaps.Count > 0)
                    {
                        foreach (var typeMap in entity.CircularUserTypeMaps)
                        {
                            if (typeMap.CircularUserTypeMapIID == 0)
                            {
                                dbContext.Entry(typeMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(typeMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }
                    }

                    if (entity.CircularAttachmentMaps.Count > 0)
                    {
                        foreach (var atch in entity.CircularAttachmentMaps)
                        {
                            if (atch.CircularAttachmentMapIID == 0)
                            {
                                dbContext.Entry(atch).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(atch).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            //if (entity.CircularStatusID == 2)
            //{

            //    var t = await Task.Run(() => CircularEmailProcess(entity));

            //}
            return entity;
        }

        #region Not using Code
        //private async Task<long> SaveCircular(CircularDTO toDto)
        //{

        //    MutualRepository mutualRepository = new MutualRepository();
        //    Entity.Models.Settings.Sequence sequence = new Entity.Models.Settings.Sequence();

        //    if (toDto.CircularIID == 0)
        //    {
        //        try
        //        {
        //            sequence = mutualRepository.GetNextSequence("CircularCode");
        //        }
        //        catch (Exception ex)
        //        {
        //            Eduegate.Logger.LogHelper<CircularMapper>.Fatal(ex.Message, ex);
        //            throw new Exception("Please generate sequence with 'CircularCode'");
        //        }
        //    }
        //    //convert the dto to entity and pass to the repository.
        //    var entity = new Circular()
        //    {
        //        CircularIID = toDto.CircularIID,
        //        CircularCode = toDto.CircularIID == 0 ? sequence.Prefix + sequence.LastSequence : toDto.CircularCode,
        //        SchoolID = toDto.SchoolID.HasValue ? toDto.SchoolID : Convert.ToByte(_context.SchoolID),
        //        AcadamicYearID = toDto.AcadamicYearID.HasValue ? toDto.AcadamicYearID : _context.AcademicYearID,
        //        CircularTypeID = toDto.CircularTypeID,
        //        CircularPriorityID = toDto.CircularPriorityID,
        //        CircularDate = toDto.CircularDate,
        //        ExpiryDate = toDto.ExpiryDate,
        //        Title = toDto.Title,
        //        ShortTitle = toDto.ShortTitle,
        //        Message = toDto.Message,
        //        CircularStatusID = toDto.CircularStatusID,
        //        AttachmentReferenceID = toDto.AttachmentReferenceID,
        //    };
        //    using (var dbContext = new dbEduegateSchoolContext())
        //    {
        //        var IIDs = toDto.CircularAttachmentMaps
        //            .Select(a => a.CircularAttachmentMapIID).ToList();

        //        //delete maps
        //        var entities = dbContext.CircularAttachmentMaps.Where(x =>
        //            x.CircularID == entity.CircularIID &&
        //            !IIDs.Contains(x.CircularAttachmentMapIID)).ToList();

        //        if (entities.IsNotNull())
        //            dbContext.CircularAttachmentMaps.RemoveRange(entities);
        //    }

        //    entity.CircularAttachmentMaps = new List<CircularAttachmentMap>();

        //    if (toDto.CircularAttachmentMaps.Count > 0)
        //    {
        //        foreach (var attach in toDto.CircularAttachmentMaps)
        //        {
        //            if (attach.AttachmentReferenceID.HasValue || !string.IsNullOrEmpty(attach.AttachmentName))
        //            {
        //                entity.CircularAttachmentMaps.Add(new CircularAttachmentMap()
        //                {
        //                    CircularAttachmentMapIID = attach.CircularAttachmentMapIID,
        //                    CircularID = attach.CircularID.HasValue ? attach.CircularID : null,
        //                    AttachmentReferenceID = attach.AttachmentReferenceID,
        //                    AttachmentName = attach.AttachmentName,
        //                    AttachmentDescription = attach.AttachmentDescription,
        //                    Notes = attach.Notes,
        //                    CreatedBy = attach.CircularAttachmentMapIID == 0 ? (int)_context.LoginID : toDto.CreatedBy,
        //                    UpdatedBy = attach.CircularAttachmentMapIID > 0 ? (int)_context.LoginID : toDto.UpdatedBy,
        //                    CreatedDate = attach.CircularAttachmentMapIID == 0 ? DateTime.Now : toDto.CreatedDate,
        //                    UpdatedDate = attach.CircularAttachmentMapIID > 0 ? DateTime.Now : toDto.UpdatedDate,
        //                });
        //            }
        //        }
        //    }

        //    if (toDto.CircularIID == 0)
        //    {
        //        entity.CreatedBy = Convert.ToInt32(_context.LoginID);
        //        entity.CreatedDate = DateTime.Now;
        //    }
        //    else
        //    {
        //        entity.UpdatedBy = Convert.ToInt32(_context.LoginID);
        //        entity.UpdatedDate = DateTime.Now;
        //    }

        //    foreach (var cirMap in toDto.CircularMaps)
        //    {
        //        if (!entity.CircularMaps.Any(x => x.ClassID == cirMap.ClassID &&
        //           x.SectionID == cirMap.SectionID && x.DepartmentID == cirMap.DepartmentID
        //           && x.AllClass == cirMap.AllClass && x.AllDepartment == cirMap.AllDepartment &&
        //           x.AllSection == cirMap.AllSection))
        //        {
        //            entity.CircularMaps.Add(new CircularMap()
        //            {
        //                CircularMapIID = cirMap.CircularMapIID,
        //                ClassID = cirMap.ClassID,
        //                AllClass = cirMap.AllClass,
        //                AllDepartment = cirMap.AllDepartment,
        //                AllSection = cirMap.AllSection,
        //                SectionID = cirMap.SectionID,
        //                DepartmentID = cirMap.DepartmentID,
        //                CreatedBy = cirMap.CircularMapIID == 0 ? Convert.ToInt32(_context.LoginID) : cirMap.CreatedBy,
        //                CreatedDate = cirMap.CircularMapIID == 0 ? DateTime.Now : cirMap.CreatedDate,
        //                UpdatedBy = cirMap.CircularMapIID != 0 ? Convert.ToInt32(_context.LoginID) : cirMap.UpdatedBy,
        //                UpdatedDate = cirMap.CircularMapIID != 0 ? DateTime.Now : cirMap.UpdatedDate,
        //            });
        //        }
        //    }

        //    foreach (var userTypemap in toDto.CircularUserTypeMaps)
        //    {
        //        entity.CircularUserTypeMaps.Add(new CircularUserTypeMap()
        //        {
        //            CircularUserTypeMapIID = userTypemap.CircularUserTypeMapIID,
        //            CircularUserTypeID = userTypemap.CircularUserTypeID,
        //            CreatedBy = userTypemap.CircularUserTypeMapIID == 0 ? Convert.ToInt32(_context.LoginID) : userTypemap.CreatedBy,
        //            CreatedDate = userTypemap.CircularUserTypeMapIID == 0 ? DateTime.Now : userTypemap.CreatedDate,
        //            UpdatedBy = userTypemap.CircularUserTypeMapIID != 0 ? Convert.ToInt32(_context.LoginID) : userTypemap.UpdatedBy,
        //            UpdatedDate = userTypemap.CircularUserTypeMapIID != 0 ? DateTime.Now : userTypemap.UpdatedDate,
        //        });
        //    }

        //    using (var dbContext = new dbEduegateSchoolContext())
        //    {
        //        dbContext.Circulars.Add(entity);

        //        if (entity.CircularIID == 0)
        //        {
        //            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
        //        }
        //        else
        //        {
        //            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //        }

        //        var mapEntity = dbContext.CircularMaps
        //            .Where(x => x.CircularID == toDto.CircularIID);

        //        if (mapEntity != null)
        //        {
        //            dbContext.CircularMaps.RemoveRange(mapEntity);
        //        }

        //        if (entity.CircularMaps.Count > 0)
        //        {
        //            foreach (var circularMap in entity.CircularMaps)
        //            {
        //                if (circularMap.CircularMapIID == 0)
        //                {
        //                    dbContext.Entry(circularMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
        //                }
        //                else
        //                {
        //                    dbContext.Entry(circularMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //                }
        //            }
        //        }

        //        var typeEntity = dbContext.CircularUserTypeMaps
        //            .Where(x => x.CircularID == toDto.CircularIID);

        //        if (typeEntity != null)
        //        {
        //            dbContext.CircularUserTypeMaps
        //                .RemoveRange(typeEntity);
        //        }

        //        if (entity.CircularUserTypeMaps.Count > 0)
        //        {
        //            foreach (var typeMap in entity.CircularUserTypeMaps)
        //            {
        //                if (typeMap.CircularUserTypeMapIID == 0)
        //                {
        //                    dbContext.Entry(typeMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
        //                }
        //                else
        //                {
        //                    dbContext.Entry(typeMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //                }
        //            }
        //        }

        //        if (entity.CircularAttachmentMaps.Count > 0)
        //        {
        //            foreach (var atch in entity.CircularAttachmentMaps)
        //            {
        //                if (atch.CircularAttachmentMapIID == 0)
        //                {
        //                    dbContext.Entry(atch).State = Microsoft.EntityFrameworkCore.EntityState.Added;
        //                }
        //                else
        //                {
        //                    dbContext.Entry(atch).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //                }
        //            }
        //        }

        //        dbContext.SaveChanges();
        //    }

        //    if (entity.CircularStatusID == 2)
        //    {

        //        var t = await Task.Run(() => CircularEmailProcess(entity));

        //    }
        //    return entity.CircularIID;
        //}

        //public List<CircularListDTO> GetCircularList(long loginID)
        //{

        //    using (var dbContext = new dbEduegateSchoolContext())
        //    {
        //        var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
        //        var studentDTO = (from s in dbContext.Students
        //                          join p in dbContext.Parents on s.ParentID equals p.ParentIID
        //                          where (p.LoginID == loginID && s.IsActive == true)
        //                          orderby s.StudentIID ascending
        //                          select new StudentDTO()
        //                          {
        //                              ClassID = s.ClassID,
        //                              SectionID = s.SectionID,
        //                              SchoolID = s.SchoolID
        //                          });
        //        var circulars = dbContext.CircularMaps.Include(i => i.Circular").Where(y => y.Circular.CircularStatusID == 2
        //                            && studentDTO.Select(z => z.SchoolID).Contains(y.Circular.SchoolID)
        //                            && (((  studentDTO.Any(z=> z.ClassID==y.ClassID && !(y.AllClass??false)))  || (y.ClassID == null || y.AllClass == true))
        //                            && ((studentDTO.Any(z => z.SectionID == y.SectionID && !(y.AllSection ?? false))) || (y.SectionID == null || y.AllSection == true))
        //                            || (y.AllClass == true || y.AllSection == true))).Select(x => x.CircularID.Value).Distinct();

        //        var ListCirular = circulars.ToList();
        //        var circularmaps = dbContext.CircularMaps.Where(c => ListCirular.Contains(c.CircularID.Value)).ToList();
        //        var circularData = dbContext.Circulars.Where(c => ListCirular.Contains(c.CircularIID)).OrderByDescending(y => y.CircularDate).ToList();
        //        var circularList = (from c in circularData

        //                            select new CircularListDTO()
        //                            {
        //                                AcademicYear = c.AcademicYear.AcademicYearCode,
        //                                CircularCode = c.CircularCode,
        //                                CircularDate = c.CircularDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture),
        //                                ExpiryDate = c.ExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture),
        //                                Class = GetName(circularmaps, "Class", c.CircularIID),
        //                                Section = GetName(circularmaps, "Section", c.CircularIID),
        //                                Message = c.Message,
        //                                ShortTitle = c.ShortTitle,
        //                                Title = c.Title,
        //                                School = c.School.SchoolName,
        //                                CircularIID = c.CircularIID
        //                            });
        //        return circularList.ToList();

        //    }
        //}
        #endregion

        public decimal GetCircularCount(long loginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

                var studentDTO = (from s in dbContext.Students
                                  join p in dbContext.Parents on s.ParentID equals p.ParentIID
                                  where (p.LoginID == loginID && s.IsActive == true)
                                  orderby s.StudentIID ascending
                                  select new StudentDTO()
                                  {
                                      ClassID = s.ClassID,
                                      SectionID = s.SectionID,
                                      SchoolID = s.SchoolID
                                  }).Distinct().AsNoTracking();

                var circularmaps = (from n in dbContext.CircularMaps
                                    join m in ((from n in dbContext.CircularMaps
                                                join m in dbContext.Circulars on n.CircularID equals m.CircularIID
                                                where m.CircularStatusID == 2
                                                && m.ExpiryDate.Value.Date >= DateTime.Now.Date
                                                && studentDTO.Any(w => w.SchoolID == m.SchoolID)
                                                && ((n.AllClass ?? false) || (studentDTO.Any(w => w.ClassID == n.ClassID)))
                                                select m).Distinct().AsNoTracking()) on n.CircularID equals m.CircularIID
                                    select n).Distinct().AsNoTracking().ToList();

                List<long> _sLstCirculars = circularmaps.Select(w
                    => w.CircularID ?? 0).Distinct().ToList<long>();


                var dCircular = (from n in dbContext.CircularMaps.Where(w => _sLstCirculars.Contains(w.CircularID ?? 0))
                                 where ((n.AllSection ?? false) || (studentDTO.Any(x => x.SectionID == n.SectionID)))
                                 select n).Distinct().AsNoTracking();
                if (dCircular.Any())
                    circularmaps.AddRange(dCircular);


                var circularData = dbContext.Circulars.Where(c => _sLstCirculars.Contains(c.CircularIID)).OrderByDescending(y => y.CircularDate).AsNoTracking().ToList();
                return circularData.Count();
            }
        }

        public List<CircularListDTO> GetCircularList(long loginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat", "dd/MM/yyyy");

                var studentDTO = (from s in dbContext.Students
                                  join p in dbContext.Parents on s.ParentID equals p.ParentIID
                                  where (p.LoginID == loginID && s.IsActive == true)
                                  orderby s.StudentIID ascending
                                  select new StudentDTO()
                                  {
                                      ClassID = s.ClassID,
                                      SectionID = s.SectionID,
                                      SchoolID = s.SchoolID,
                                      AcademicYearID = s.AcademicYearID
                                  }).Distinct().AsNoTracking();

                #region old code
                //var circularmaps = (from n in dbContext.CircularMaps
                //                    join m in
                //                    ((from n in dbContext.CircularMaps
                //                      join m in dbContext.Circulars on n.CircularID equals m.CircularIID
                //                      where m.CircularStatusID == 2
                //                      && m.ExpiryDate >= DateTime.Now
                //                      && studentDTO.Any(w => w.SchoolID == m.SchoolID)
                //                      && ((n.AllClass ?? false) || (studentDTO.Any(w => w.ClassID == n.ClassID)))
                //                      select m).Distinct()) on n.CircularID equals m.CircularIID
                //                    where ((n.AllSection ?? false) || (studentDTO.Any(x => x.SectionID == n.SectionID)))
                //                    select n).Distinct().ToList();
                //List<long> sectionCircularList = new List<long>();
                //List<long> allCircularList = new List<long>();
                //var circularmaps = new List<CircularMap>();
                //var circularClassSectionmaps = new List<CircularMap>();
                //foreach (var sData in studentDTO)
                //{
                //    circularmaps = (from n in dbContext.CircularMaps
                //                    join m in ((from n in dbContext.CircularMaps
                //                                join m in dbContext.Circulars on n.CircularID equals m.CircularIID
                //                                where m.CircularStatusID == 2
                //                                && m.ExpiryDate >= DateTime.Now
                //                                && studentDTO.Any(w => w.SchoolID == m.SchoolID)
                //                                && ((n.AllClass ?? false) || (sData.ClassID == n.ClassID))
                //                                select m).Distinct()) on n.CircularID equals m.CircularIID
                //                    select n).Distinct().Include(i => i.Class).Include(i => i.Section).AsNoTracking().ToList();

                //    circularClassSectionmaps.AddRange(circularmaps);
                //    List<long> _sLstCirculars = circularmaps.Select(w
                //        => w.CircularID ?? 0).Distinct().ToList<long>();


                //    var dCircular = (from n in dbContext.CircularMaps.Where(w => _sLstCirculars.Contains(w.CircularID ?? 0))
                //                     where ((n.AllSection ?? false) || (sData.SectionID == n.SectionID))
                //                     select n).Distinct().Include(i => i.Class).Include(i => i.Section).AsNoTracking();
                //    if (dCircular.Any())
                //        circularClassSectionmaps.AddRange(dCircular);

                //    sectionCircularList = dCircular.Select(w
                //         => w.CircularID ?? 0).Distinct().ToList<long>();
                //    allCircularList.AddRange(sectionCircularList);
                //    //_sLstCirculars.RemoveRange(sectionCircularList);
                //}
                #endregion

                List<long> sectionCircularList = new List<long>();
                List<long> allCircularList = new List<long>();
                var circularClassSectionmaps = new List<CircularMap>();

                foreach (var sData in studentDTO)
                {
                    var circularmaps = (from n in dbContext.CircularMaps
                                        join m in dbContext.Circulars on n.CircularID equals m.CircularIID
                                        where m.CircularStatusID == 2
                                              && m.AcadamicYearID == sData.AcademicYearID
                                              && m.ExpiryDate.Value.Date >= DateTime.Now.Date
                                              && studentDTO.Any(w => w.SchoolID == m.SchoolID)
                                              && (n.AllClass ?? false || sData.ClassID == n.ClassID)
                                        select n).Distinct().Include(i => i.Class).Include(i => i.Section).AsNoTracking().ToList();

                    circularClassSectionmaps.AddRange(circularmaps);

                    var _sLstCirculars = circularmaps.Select(w => w.CircularID ?? 0).Distinct().ToList();

                    var dCircular = dbContext.CircularMaps
                                    .Where(w => _sLstCirculars.Contains(w.CircularID ?? 0) && (w.AllSection ?? false || sData.SectionID == w.SectionID)) 
                                    .Distinct().Include(i => i.Class).Include(i => i.Section).AsNoTracking().ToList();

                    circularClassSectionmaps.AddRange(dCircular);

                    var sectionCircularIds = dCircular.Select(w => w.CircularID ?? 0).Distinct().ToList();
                    sectionCircularList.AddRange(sectionCircularIds);
                    allCircularList.AddRange(sectionCircularIds);
                }

                var circularData = dbContext.Circulars.Where(c => allCircularList.Contains(c.CircularIID))
                    .Include(i => i.AcademicYear)
                    .Include(i => i.School)
                    .Include(i => i.CircularAttachmentMaps)
                    .OrderByDescending(y => y.CircularDate)
                    .ThenByDescending(y => y.CircularIID)
                    .AsNoTracking()
                    .ToList();

                var circularList = (from c in circularData
                                    select new CircularListDTO()
                                    {
                                        AcademicYear = c.AcademicYear.AcademicYearCode,
                                        CircularCode = c.CircularCode,
                                        CircularDate = c.CircularDate.HasValue ? c.CircularDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                        ExpiryDate = c.ExpiryDate.HasValue ? c.ExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                        Class = GetName(circularClassSectionmaps, "Class", c.CircularIID),
                                        Section = GetName(circularClassSectionmaps, "Section", c.CircularIID),
                                        Message = c.Message,
                                        ShortTitle = c.ShortTitle,
                                        Title = c.Title,
                                        School = c.School.SchoolName,
                                        CircularIID = c.CircularIID,
                                        CircularAttachmentMaps = (from aat in c.CircularAttachmentMaps

                                                                  select new CircularAttachmentMapDTO()
                                                                  {
                                                                      CircularAttachmentMapIID = aat.CircularAttachmentMapIID,
                                                                      CircularID = aat.CircularID,
                                                                      Notes = aat.Notes,
                                                                      AttachmentName = aat.AttachmentName,
                                                                      AttachmentDescription = aat.AttachmentDescription,
                                                                      AttachmentReferenceID = aat.AttachmentReferenceID,
                                                                  }).ToList(),
                                    });

                return circularList.ToList();
            }
        }

        public List<CircularListDTO> GetCircularListByStudentID(long studentID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat", "dd/MM/yyyy");

                var studentDTO = (from s in dbContext.Students where s.StudentIID== studentID
                                  select new StudentDTO()
                                  {
                                      ClassID = s.ClassID,
                                      SectionID = s.SectionID,
                                      SchoolID = s.SchoolID
                                  }).AsNoTracking().FirstOrDefault();

                

                List<long> sectionCircularList = new List<long>();
                List<long> allCircularList = new List<long>();
                var circularClassSectionmaps = new List<CircularMap>();

                if (studentDTO != null)
                {
                    var circularmaps = (from n in dbContext.CircularMaps
                                        join m in dbContext.Circulars on n.CircularID equals m.CircularIID
                                        where m.CircularStatusID == 2
                                              && m.ExpiryDate.Value.Date >= DateTime.Now.Date
                                              && studentDTO.SchoolID == m.SchoolID
                                              
                                              && (n.AllClass ?? false || studentDTO.ClassID == n.ClassID)
                                         select n).Distinct().Include(i => i.Class).Include(i => i.Section).AsNoTracking().ToList();

                    circularClassSectionmaps.AddRange(circularmaps);

                    var _sLstCirculars = circularmaps.Select(w => w.CircularID ?? 0).Distinct().ToList();

                    var dCircular = dbContext.CircularMaps
                                    .Where(w => _sLstCirculars.Contains(w.CircularID ?? 0) && (w.AllSection ?? false || studentDTO.SectionID == w.SectionID))
                                    .Distinct().Include(i => i.Class).Include(i => i.Section).AsNoTracking().ToList();

                    circularClassSectionmaps.AddRange(dCircular);

                    var sectionCircularIds = dCircular.Select(w => w.CircularID ?? 0).Distinct().ToList();
                    sectionCircularList.AddRange(sectionCircularIds);
                    allCircularList.AddRange(sectionCircularIds);
                }

                var circularData = dbContext.Circulars.Where(c => allCircularList.Contains(c.CircularIID))
                    .Include(i => i.AcademicYear)
                    .Include(i => i.School)
                    .Include(i => i.CircularAttachmentMaps)
                    .OrderByDescending(y => y.CircularDate)
                    .ThenByDescending(y => y.CircularIID)
                    .AsNoTracking()
                    .ToList();

                var circularList = (from c in circularData
                                    select new CircularListDTO()
                                    {
                                        AcademicYear = c.AcademicYear.AcademicYearCode,
                                        CircularCode = c.CircularCode,
                                        CircularDate = c.CircularDate.HasValue ? c.CircularDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                        ExpiryDate = c.ExpiryDate.HasValue ? c.ExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                        Class = GetName(circularClassSectionmaps, "Class", c.CircularIID),
                                        Section = GetName(circularClassSectionmaps, "Section", c.CircularIID),
                                        Message = c.Message,
                                        ShortTitle = c.ShortTitle,
                                        Title = c.Title,
                                        School = c.School.SchoolName,
                                        CircularIID = c.CircularIID,
                                        CircularAttachmentMaps = (from aat in c.CircularAttachmentMaps

                                                                  select new CircularAttachmentMapDTO()
                                                                  {
                                                                      CircularAttachmentMapIID = aat.CircularAttachmentMapIID,
                                                                      CircularID = aat.CircularID,
                                                                      Notes = aat.Notes,
                                                                      AttachmentName = aat.AttachmentName,
                                                                      AttachmentDescription = aat.AttachmentDescription,
                                                                      AttachmentReferenceID = aat.AttachmentReferenceID,
                                                                  }).ToList(),
                                    });

                return circularList.ToList();
            }
        }


        private string GetName(List<CircularMap> CircularMapList, string fieldType, long circularID)
        {
            string _sStudent_IDs = string.Empty;

            if (fieldType == "Class")
            {
                var classname = CircularMapList.Where(y => y.CircularID == circularID && y.ClassID != null);
                return classname.Count() > 0 ? string.Join(",", classname.Select(x => x.Class.ClassDescription)) : "All Class";


            }
            if (fieldType == "Section")
            {
                var section = CircularMapList.Where(y => y.CircularID == circularID && y.SectionID != null);
                return section.Count() > 0 ? string.Join(",", section.Select(x => x.Section.SectionName)) : "All Section";
            }
            return null;
        }


        private List<string> GetParentEmailIDs(Circular entity)
        {
            List<string> emailIDs = new List<string>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var classIDs = dbContext.Classes.AsNoTracking().Select(s => s.ClassID).ToList();
                var sectionIDs = dbContext.Sections.AsNoTracking().Select(s => s.SectionID).ToList();

                if (entity.CircularMaps.Any(map => !(map.AllClass ?? false) && map.ClassID > 0))
                {
                    classIDs.ToList().RemoveAll(cls => !entity.CircularMaps.Any(x => x.ClassID == cls));
                }
                if (entity.CircularMaps.Any(map => !(map.AllSection ?? false) && map.SectionID > 0))
                {
                    sectionIDs.ToList().RemoveAll(sec => !entity.CircularMaps.Any(x => x.SectionID == sec));
                }

                emailIDs = dbContext.Parents
                    .Where(p => p.Students.Any(std => classIDs.Any(c => c == std.ClassID) && sectionIDs.Any(s => s == std.SectionID)
                    && std.IsActive == true && std.SchoolID == entity.SchoolID && std.AcademicYearID == entity.AcadamicYearID)
                    && p.LoginID.HasValue)
                    .Include(i => i.Students)
                    .AsNoTracking()
                    .Select(l => l.GaurdianEmail)
                    .Distinct()
                    .ToList();
            }

            return emailIDs;
        }

        private List<StudentDTO> GetDetailsForMail(CircularDTO toDto)
        {
            List<StudentDTO> studentDTOs = new List<StudentDTO>();
            var activeStudStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUDENT_ACTIVE_STATUSID");

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var classIDs = dbContext.Classes.AsNoTracking().Select(s => s.ClassID).ToList();
                var sectionIDs = dbContext.Sections.AsNoTracking().Select(s => s.SectionID).ToList();

                var entity = dbContext.Circulars
                    .AsNoTracking().Include(x => x.CircularMaps).FirstOrDefault(x => x.CircularIID == toDto.CircularIID);

                //TODO
                if (entity.CircularMaps.Any(map => !(map.AllClass ?? false) && map.ClassID > 0))
                {
                    classIDs.RemoveAll(cls => !entity.CircularMaps.Any(x => x.ClassID == cls));
                }
                if (entity.CircularMaps.Any(map => !(map.AllSection ?? false) && map.SectionID > 0))
                {
                    sectionIDs.RemoveAll(sec => !entity.CircularMaps.Any(x => x.SectionID == sec));
                }

                foreach (var classID in classIDs)
                {
                    var gaurdianEmails = dbContext.Parents
                        .Include(p => p.Students)
                        .Where(p => p.Students.Any(std => std.ClassID == classID && std.SchoolID == _context.SchoolID && std.AcademicYearID == toDto.AcadamicYearID 
                        && sectionIDs.Any(s => s == std.SectionID) && p.GaurdianEmail != null 
                        && std.IsActive == true && std.Status == byte.Parse(activeStudStatus) )
                        && p.LoginID.HasValue).AsNoTracking().Select(l => l.GaurdianEmail).Distinct().ToList();

                    var classData = new Eduegate.Domain.Setting.SettingBL(null).GetClassDetailByClassID(classID);

                    foreach (var emailID in gaurdianEmails)
                    {
                        studentDTOs.Add(new StudentDTO()
                        {
                            ParentEmailID = emailID,
                            ClassID = classID,
                            ClassCode = classData?.Code?.ToLower(),
                        });
                    }
                }
            }

            return studentDTOs;
        }

        public string CircularEmailProcess(CircularDTO toDto)
        {
            var parentMailIDs = new List<StudentDTO>();
            var staffEmailIDs = new List<string>();
            List<long?> departmentIds = null;

            // Determine user types involved
            bool includeAllUsers = toDto.CircularUserTypeMaps.Any(x =>
                x.CircularUserType.Value.Contains(CircularUserTypes.All.ToString(), StringComparison.OrdinalIgnoreCase));
            bool includeParents = toDto.CircularUserTypeMaps.Any(x =>
                x.CircularUserType.Value.Contains(CircularUserTypes.Parents.ToString(), StringComparison.OrdinalIgnoreCase));
            bool includeStaffs = toDto.CircularUserTypeMaps.Any(x =>
                x.CircularUserType.Value.Contains(CircularUserTypes.Staffs.ToString(), StringComparison.OrdinalIgnoreCase));

            // Collect Parent emails if needed
            if (includeAllUsers || includeParents)
            {
                parentMailIDs = GetDetailsForMail(toDto);
            }

            // Collect Staff emails if needed
            if (includeAllUsers || includeStaffs)
            {
                // Get department IDs if specific departments are selected
                if (toDto.CircularMaps.Any(x => x.AllDepartment == false))
                {
                    departmentIds = toDto.CircularMaps
                        .Select(x =>
                        {
                            if (!string.IsNullOrEmpty(x.Department?.Key))
                            {
                                return (long?)long.Parse(x.Department.Key);
                            }
                            return null;
                        }).Where(id => id.HasValue).ToList();
                }

                staffEmailIDs = GetActiveStaffWorkingEmailIDs(departmentIds);
            }

            // Send emails to Parents
            if (parentMailIDs.Any())
            {
                foreach (var parent in parentMailIDs)
                {
                    EmailProcess(parent.ParentEmailID, parent.ClassCode, toDto.Title);
                }
            }

            // Send emails to Staffs
            if (staffEmailIDs.Any())
            {
                foreach (var email in staffEmailIDs)
                {
                    EmailProcess(email, null, toDto.Title);
                }
            }

            return null;
        }

        public void EmailProcess(string toEmail, string classCode, string title)
        {
            var hostDet = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");

            string defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");

            var emailTemplate = EmailTemplateMapper.Mapper(_context).GetEmailTemplateDetails(EmailTemplates.Circular.ToString());

            var emailSubject = string.Empty;
            var emailBody = string.Empty;

            if (emailTemplate != null && !string.IsNullOrEmpty(emailTemplate.EmailTemplate))
            {
                emailSubject = emailTemplate.Subject;

                emailBody = emailTemplate.EmailTemplate;

                emailSubject = emailSubject.Replace("{Title}", title);
            }

            try
            {
                classCode = classCode?.ToLower();
                string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_context).PopulateBody(toEmail, emailBody);

                var mailParameters = new Dictionary<string, string>()
                {
                    { "CLASS_CODE", classCode},
                };

                if (emailBody != "")
                {
                    if (hostDet.ToLower() == "live")
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(toEmail, emailSubject, mailMessage, EmailTypes.CircularPublish, mailParameters);
                    }
                    else
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(defaultMail, emailSubject, mailMessage, EmailTypes.CircularPublish, mailParameters);
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"Circular Mailing failed. Error message: {errorMessage}", ex);
            }
        }

        private List<long?> GetParentLoginIDs(Circular entity)
        {
            List<long?> loginIDs = new List<long?>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var classIDs = dbContext.Classes.AsNoTracking().Select(s => s.ClassID).ToList();
                var sectionIDs = dbContext.Sections.AsNoTracking().Select(s => s.SectionID).ToList();

                //TODO
                if (entity.CircularMaps.Any(map => !(map.AllClass ?? false) && map.ClassID > 0))
                {
                    classIDs.RemoveAll(cls => !entity.CircularMaps.Any(x => x.ClassID == cls));
                }
                if (entity.CircularMaps.Any(map => !(map.AllSection ?? false) && map.SectionID > 0))
                {
                    sectionIDs.RemoveAll(sec => !entity.CircularMaps.Any(x => x.SectionID == sec));
                }

                loginIDs = dbContext.Parents
                    .Where(p => p.Students.Any(std => classIDs.Any(c => c == std.ClassID) && sectionIDs.Any(s => s == std.SectionID)
                    && std.IsActive == true && std.SchoolID == entity.SchoolID && std.AcademicYearID == entity.AcadamicYearID)
                    && p.LoginID.HasValue)
                    .Include(i => i.Students)
                    .AsNoTracking()
                    .Select(l => l.LoginID)
                    .Distinct()
                    .ToList();
            }

            return loginIDs;
        }

        public List<CircularListDTO> GetCircularsByEmployee(long loginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat", "dd/MM/yyyy");

                var employeeDTO = (from s in dbContext.Employees
                                   where (s.LoginID == loginID && s.IsActive == true)
                                   select new EmployeeDTO()
                                  {
                                      BranchID = s.BranchID,
                                      //DepartmentID = s.DepartmentID
                                  }).Distinct().AsNoTracking();

                //var studentDTO = (from s in dbContext.Students
                //                  join p in dbContext.Parents on s.ParentID equals p.ParentIID
                //                  where (p.LoginID == loginID && s.IsActive == true)
                //                  orderby s.StudentIID ascending
                //                  select new StudentDTO()
                //                  {
                //                      ClassID = s.ClassID,
                //                      SectionID = s.SectionID,
                //                      SchoolID = s.SchoolID
                //                  }).Distinct().AsNoTracking();

                //var circularmaps = (from n in dbContext.CircularMaps
                //                    join m in
                //                    ((from n in dbContext.CircularMaps
                //                      join m in dbContext.Circulars on n.CircularID equals m.CircularIID
                //                      where m.CircularStatusID == 2
                //                      && m.ExpiryDate >= DateTime.Now
                //                      && studentDTO.Any(w => w.SchoolID == m.SchoolID)
                //                      && ((n.AllClass ?? false) || (studentDTO.Any(w => w.ClassID == n.ClassID)))
                //                      select m).Distinct()) on n.CircularID equals m.CircularIID
                //                    where ((n.AllSection ?? false) || (studentDTO.Any(x => x.SectionID == n.SectionID)))
                //                    select n).Distinct().ToList();
                //List<long> sectionCircularList = new List<long>();
                //List<long> allCircularList = new List<long>();
                //var circularmaps = new List<CircularMap>();
                //var circularClassSectionmaps = new List<CircularMap>();
                //foreach (var sData in studentDTO)
                //{
                //    circularmaps = (from n in dbContext.CircularMaps
                //                    join m in ((from n in dbContext.CircularMaps
                //                                join m in dbContext.Circulars on n.CircularID equals m.CircularIID
                //                                where m.CircularStatusID == 2
                //                                && m.ExpiryDate >= DateTime.Now
                //                                && studentDTO.Any(w => w.SchoolID == m.SchoolID)
                //                                && ((n.AllClass ?? false) || (sData.ClassID == n.ClassID))
                //                                select m).Distinct()) on n.CircularID equals m.CircularIID
                //                    select n).Distinct().Include(i => i.Class).Include(i => i.Section).AsNoTracking().ToList();

                //    circularClassSectionmaps.AddRange(circularmaps);
                //    List<long> _sLstCirculars = circularmaps.Select(w
                //        => w.CircularID ?? 0).Distinct().ToList<long>();


                //    var dCircular = (from n in dbContext.CircularMaps.Where(w => _sLstCirculars.Contains(w.CircularID ?? 0))
                //                     where ((n.AllSection ?? false) || (sData.SectionID == n.SectionID))
                //                     select n).Distinct().Include(i => i.Class).Include(i => i.Section).AsNoTracking();
                //    if (dCircular.Any())
                //        circularClassSectionmaps.AddRange(dCircular);

                //    sectionCircularList = dCircular.Select(w
                //         => w.CircularID ?? 0).Distinct().ToList<long>();
                //    allCircularList.AddRange(sectionCircularList);
                //    //_sLstCirculars.RemoveRange(sectionCircularList);
                //}


                var allCircularList = dbContext.Circulars.Where(i => i.CircularStatusID == 2 && i.ExpiryDate.Value.Date >= DateTime.Now.Date && employeeDTO.Any(w => w.BranchID == i.SchoolID)).Select(i => i.CircularIID).ToList();

                var circularData = dbContext.Circulars.Where(c => allCircularList.Contains(c.CircularIID))
                    .Include(i => i.CircularMaps)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.School)
                    .Include(i => i.CircularAttachmentMaps)
                       .Include(i => i.CircularUserTypeMaps)
                    .OrderByDescending(y => y.CircularDate)
                    .ThenByDescending(y => y.CircularIID)
                    .AsNoTracking()
                    .ToList();

                var circularList = (from c in circularData
                                    select new CircularListDTO()
                                    {
                                        AcademicYear = c.AcademicYear.AcademicYearCode,
                                        CircularCode = c.CircularCode,
                                        CircularDate = c.CircularDate.HasValue ? c.CircularDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                        ExpiryDate = c.ExpiryDate.HasValue ? c.ExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                        //Class = GetName(c.CircularMaps, "Class", c.CircularIID),
                                        //Section = GetName(circularClassSectionmaps, "Section", c.CircularIID),
                                        Message = c.Message,
                                        ShortTitle = c.ShortTitle,
                                        Title = c.Title,
                                        School = c.School.SchoolName,
                                        CircularIID = c.CircularIID,
                                        Class = string.Join(", ", c.CircularMaps
                                            .Where(cm => cm.ClassID.HasValue)
                                            .Join(dbContext.Classes, cm => cm.ClassID, cls => cls.ClassID, (cm, cls) => cls.ClassDescription)
                                            .ToList()),
                                        Section = string.Join(", ", c.CircularMaps
                                            .Where(cm => cm.SectionID.HasValue)
                                            .Join(dbContext.Sections, cm => cm.SectionID, s => s.SectionID, (cm, s) => s.SectionName)
                                            .ToList()),
                                        CircularUserTypes = (from cutm in c.CircularUserTypeMaps
                                                             join cut in dbContext.CircularUserTypes on cutm.CircularUserTypeID equals cut.CircularUserTypeID
                                                             select new CircularUserTypeMapDTO()
                                                             {
                                                                 CircularUserTypeID = cut.CircularUserTypeID,
                                                                 UserType = cut.UserType // Include UserType
                                                             }).ToList(),
                                        CircularAttachmentMaps = (from aat in c.CircularAttachmentMaps

                                                                  select new CircularAttachmentMapDTO()
                                                                  {
                                                                      CircularAttachmentMapIID = aat.CircularAttachmentMapIID,
                                                                      CircularID = aat.CircularID,
                                                                      Notes = aat.Notes,
                                                                      AttachmentName = aat.AttachmentName,
                                                                      AttachmentDescription = aat.AttachmentDescription,
                                                                      AttachmentReferenceID = aat.AttachmentReferenceID,
                                                                  }).ToList(),
                                    });

                return circularList.ToList();
            }
        }

        private List<long?> GetStaffLoginIDs(Circular entity, List<long?> departmentIDs)
        {
            List<long?> loginIDs = new List<long?>();
            using (var dbContext = new dbEduegateSchoolContext())
            {

                var query = dbContext.Employees.Where(x => x.IsActive == true && x.LoginID != null && x.BranchID == _context.SchoolID);

                if (departmentIDs != null && departmentIDs.Any())
                {
                    query = query.Where(x => departmentIDs.Contains(x.DepartmentID));
                }

                loginIDs = query
                    .Select(x => x.LoginID)
                    .ToList();
            }

            return loginIDs;
        }

        public decimal GetLatestStaffCircularCount(long loginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

                var EmployeeDTO = (from s in dbContext.Employees
                                  where (s.LoginID == loginID && s.IsActive == true)
                                  orderby s.EmployeeIID ascending
                                  select new EmployeeDTO()
                                  {
                                      BranchID = s.BranchID
                                  }).Distinct().AsNoTracking();

                var circularmaps = (from n in dbContext.CircularMaps
                                    join m in ((from n in dbContext.CircularMaps
                                                join m in dbContext.Circulars on n.CircularID equals m.CircularIID
                                                where m.CircularStatusID == 2
                                                && m.ExpiryDate.Value.Date >= DateTime.Now.Date
                                                && EmployeeDTO.Any(w => w.BranchID == m.SchoolID)
                                                select m).Distinct().AsNoTracking()) on n.CircularID equals m.CircularIID
                                    select n).Distinct().AsNoTracking().ToList();

                List<long> _sLstCirculars = circularmaps.Select(w
                    => w.CircularID ?? 0).Distinct().ToList<long>();


                var dCircular = (from n in dbContext.CircularMaps.Where(w => _sLstCirculars.Contains(w.CircularID ?? 0))
                                 select n).Distinct().AsNoTracking();
                if (dCircular.Any())
                    circularmaps.AddRange(dCircular);


                var circularData = dbContext.Circulars.Where(c => _sLstCirculars.Contains(c.CircularIID)).OrderByDescending(y => y.CircularDate).AsNoTracking().ToList();
                return circularData.Count();
            }
        }

        public List<string> GetCircularUserTypes()
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                return dbContext.CircularUserTypes
                                .Select(x => x.UserType)
                                .Distinct()
                                .OrderBy(x => x)
                                .ToList();
            }
        }

    }
}