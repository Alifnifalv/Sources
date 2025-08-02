using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.UserDevice;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Eduegate.Domain.Mappers.UserDevice
{
    public class UserDeviceMapMapper : DTOEntityDynamicMapper
    {
        public static UserDeviceMapMapper Mapper(CallContext context)
        {
            var mapper = new UserDeviceMapMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<UserDeviceMapDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }
        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private UserDeviceMapDTO ToDTO(long IID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.UserDeviceMaps.Where(x => x.UserDeviceMapIID == IID)
                    .Include(i => i.Login).AsNoTracking().FirstOrDefault();

                var dto = new UserDeviceMapDTO()
                {
                    UserDeviceMapIID = entity.UserDeviceMapIID,
                    LoginID = entity.LoginID,
                    DeviceToken = entity.DeviceToken,
                    LoginUserID = entity.LoginID.HasValue ? entity.Login?.LoginUserID : null,
                    IsActive = entity.IsActive,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                };

                return dto;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as UserDeviceMapDTO;

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateERPContext())
            {
                var entity = new UserDeviceMap()
                {
                    UserDeviceMapIID = toDto.UserDeviceMapIID,
                    LoginID = toDto.LoginID,
                    DeviceToken = toDto.DeviceToken,
                    IsActive = toDto.IsActive,
                    CreatedBy = toDto.UserDeviceMapIID == 0 ? Convert.ToInt32(_context.LoginID) : toDto.CreatedBy,
                    CreatedDate = toDto.UserDeviceMapIID == 0 ? DateTime.Now : toDto.CreatedDate,
                    UpdatedBy = toDto.UserDeviceMapIID != 0 ? Convert.ToInt32(_context.LoginID) : toDto.UpdatedBy,
                    UpdatedDate = toDto.UserDeviceMapIID != 0 ? DateTime.Now : toDto.UpdatedDate,
                };

                dbContext.UserDeviceMaps.Add(entity);

                if (entity.UserDeviceMapIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    var deviceList = dbContext.UserDeviceMaps.Where(x => x.LoginID == entity.LoginID && x.UserDeviceMapIID != entity.UserDeviceMapIID && x.IsActive == true).AsNoTracking().ToList();

                    if (deviceList.Count > 0)
                    {
                        foreach (var device in deviceList)
                        {
                            device.IsActive = false;

                            dbContext.Entry(device).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }
                }

                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.UserDeviceMapIID));
            }
        }

    }
}