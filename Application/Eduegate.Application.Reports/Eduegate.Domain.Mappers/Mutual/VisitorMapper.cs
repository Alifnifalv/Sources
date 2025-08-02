using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.Models.Mutual;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Framework.Security;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Mutual;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Eduegate.Domain.Mappers.Mutual
{
    public class VisitorMapper : DTOEntityDynamicMapper
    {
        public static VisitorMapper Mapper(CallContext context)
        {
            var mapper = new VisitorMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<VisitorDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private VisitorDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.Visitors
                    .Where(a => a.VisitorIID == IID)
                    .Include(i => i.Login)
                    .Include(i => i.VisitorAttachmentMaps)
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private VisitorDTO ToDTO(Visitor entity)
        {
            var visitorDTO = new VisitorDTO()
            {
                VisitorIID = entity.VisitorIID,
                VisitorNumber = entity.VisitorNumber,
                QID = entity.QID,
                PassportNumber = entity.PassportNumber,
                FirstName = entity.FirstName,
                MiddleName = entity.MiddleName,
                LastName = entity.LastName,
                VisitorFullName = entity.FirstName + (string.IsNullOrEmpty(entity.MiddleName) ? "" : " " + entity.MiddleName) + " " + entity.LastName,
                MobileNumber1 = entity.MobileNumber1,
                MobileNumber2 = entity.MobileNumber2,
                EmailID = entity.EmailID,
                LoginID = entity.LoginID,
                OtherDetails = entity.OtherDetails,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate,
            };

            visitorDTO.VisitorAttachmentMapDTOs = new List<VisitorAttachmentMapDTO>();

            if (entity.VisitorAttachmentMaps != null)
            {
                foreach (var attach in entity.VisitorAttachmentMaps)
                {
                    visitorDTO.VisitorAttachmentMapDTOs.Add(new VisitorAttachmentMapDTO()
                    {
                        VisitorAttachmentMapIID = attach.VisitorAttachmentMapIID,
                        VisitorID = attach.VisitorID,
                        QIDFrontAttachmentID = attach.QIDFrontAttachmentID,
                        QIDBackAttachmentID = attach.QIDBackAttachmentID,
                        PassportFrontAttachmentID = attach.PassportFrontAttachmentID,
                        PassportBackAttachmentID = attach.PassportBackAttachmentID,
                        VisitorProfileID = attach.VisitorProfileID,
                        CreatedBy = attach.CreatedBy,
                        CreatedDate = attach.CreatedDate,
                        UpdatedBy = attach.UpdatedBy,
                        UpdatedDate = attach.UpdatedDate,
                    });
                }
            }

            return visitorDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as VisitorDTO;

            if (!toDto.LoginID.HasValue)
            {
                using (var dbContext = new dbEduegateERPContext())
                {
                    var loginDetails = dbContext.Logins.FirstOrDefault(l => l.LoginUserID == toDto.VisitorNumber);

                    if (loginDetails != null)
                    {
                        toDto.LoginID = loginDetails.LoginIID;
                    }
                }
            }

            toDto.CreatedBy = toDto.VisitorIID == 0 ? (int)_context.LoginID : dto.CreatedBy;
            toDto.UpdatedBy = toDto.VisitorIID > 0 ? (int)_context.LoginID : dto.UpdatedBy;
            toDto.CreatedDate = toDto.VisitorIID == 0 ? DateTime.Now : dto.CreatedDate;
            toDto.UpdatedDate = toDto.VisitorIID > 0 ? DateTime.Now : dto.UpdatedDate;

            var visitoreIID = SaveVisitorData(toDto);

            return ToDTOString(ToDTO(visitoreIID));
        }

        public long SaveVisitorData(VisitorDTO toDto)
        {
            var entity = new Visitor()
            {
                VisitorIID = toDto.VisitorIID,
                VisitorNumber = toDto.VisitorNumber,
                QID = toDto.QID,
                PassportNumber = toDto.PassportNumber,
                FirstName = toDto.FirstName,
                MiddleName = toDto.MiddleName,
                LastName = toDto.LastName,
                MobileNumber1 = toDto.MobileNumber1,
                MobileNumber2 = toDto.MobileNumber2,
                EmailID = toDto.EmailID,
                LoginID = toDto.LoginID,
                OtherDetails = toDto.OtherDetails,
                CreatedBy = toDto.CreatedBy,
                UpdatedBy = toDto.UpdatedBy,
                CreatedDate = toDto.CreatedDate,
                UpdatedDate = toDto.UpdatedDate,
            };

             entity.VisitorAttachmentMaps  = new List<VisitorAttachmentMap>();
            //now not passing multiple attachments from visitor app need to change
            entity.VisitorAttachmentMaps.Add(new VisitorAttachmentMap()
            {
                VisitorProfileID = toDto.VisitorProfileID,
                CreatedBy = toDto.CreatedBy,
                UpdatedBy = toDto.UpdatedBy,
                CreatedDate = toDto.CreatedDate,
                UpdatedDate = toDto.UpdatedDate,
            });

            if (!entity.LoginID.HasValue)
            {
                var roleMaps = new List<LoginRoleMap>
                {
                    new LoginRoleMap()
                    {
                        LoginRoleMapIID = 0,
                        LoginID = entity.LoginID.HasValue ? (long)entity.LoginID : 0,
                        RoleID = 8,
                        CreatedBy = null,
                        CreatedDate = DateTime.Now,
                    }
                };

                var passwordSalt = PasswordHash.CreateHash(entity.MobileNumber1);

                entity.Login = new Login()
                {
                    LoginIID = entity.LoginID.HasValue ? (long)entity.LoginID : 0,
                    UserName = entity.EmailID,
                    LoginUserID = entity.VisitorNumber,
                    LoginEmailID = entity.EmailID,
                    LastLoginDate = (DateTime?)null,
                    PasswordHint = "Mobile number",
                    RequirePasswordReset = false,
                    PasswordSalt = passwordSalt,
                    Password = StringCipher.Encrypt(entity.MobileNumber1, passwordSalt),
                    CreatedBy = null,
                    CreatedDate = DateTime.Now,
                    StatusID = 1,
                    LoginRoleMaps = roleMaps
                };
            }

            using (var dbContext = new dbEduegateERPContext())
            {
                dbContext.Visitors.Add(entity);

                if (entity.VisitorIID == 0)
                {
                    dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                }

                dbContext.SaveChanges();

                return entity.VisitorIID;
            }
        }

        public VisitorDTO GetVisitorDetails(string QID, string passportNumber)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var visitorDTO = new VisitorDTO();

                var entity = !string.IsNullOrEmpty(QID) ? dbContext.Visitors.Where(v => v.QID == QID).Include(i => i.VisitorAttachmentMaps).FirstOrDefault() :
                    !string.IsNullOrEmpty(passportNumber) ? dbContext.Visitors.Where(v => v.PassportNumber == passportNumber).Include(i => i.VisitorAttachmentMaps).FirstOrDefault() : null;

                if (entity != null)
                {
                    visitorDTO = ToDTO(entity);

                    visitorDTO.IsError = false;
                }
                else
                {
                    visitorDTO.IsError = true;
                    visitorDTO.ErrorMessage = "No visitor Details found.";
                }

                return visitorDTO;
            }
        }

        public OperationResultDTO RegisterVisitorDetails(VisitorDTO toDto)
        {
            var result = new OperationResultDTO();

            using (var dbContext = new dbEduegateERPContext())
            {
                var visitorDetails = dbContext.Visitors.FirstOrDefault(v => v.QID == toDto.QID || v.PassportNumber == toDto.PassportNumber || v.EmailID == toDto.EmailID);

                if (visitorDetails != null)
                {
                    return new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = "User already exists under the same QID or Passport number or EmailID."
                    };
                }
            }

            try
            {
                MutualRepository mutualRepository = new MutualRepository();

                var sequence = mutualRepository.GetNextSequence("VisitorNo");
                //var lastSeq = sequence.LastSequence.ToString().PadLeft(5, '0');
                //var visitorNumber = sequence.Prefix + lastSeq;

                var visitorNumber = GenerateRandomVisitorNumber();

                toDto.VisitorNumber = sequence.Prefix + visitorNumber.ToString().PadLeft(5, '0');
                toDto.CreatedBy = _context != null && _context.LoginID.HasValue ? (int)_context.LoginID : (int?)null;
                toDto.CreatedDate = DateTime.Now;

                if (!toDto.LoginID.HasValue)
                {
                    using (var dbContext1 = new dbEduegateERPContext())
                    {
                        var loginDetails = dbContext1.Logins.FirstOrDefault(l => l.LoginUserID == toDto.VisitorNumber);

                        if (loginDetails != null)
                        {
                            toDto.LoginID = loginDetails.LoginIID;
                        }
                    }
                }

                var visitorIID = SaveVisitorData(toDto);

                if (visitorIID > 0)
                {
                    return new OperationResultDTO()
                    {
                        operationResult = OperationResult.Success,
                        Message = "Successfully saved."
                    };
                }
                else
                {
                    return new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = "Saving failed."
                    };
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
                return new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = "Saving failed."
                };
            }
        }


        public static int GenerateRandomVisitorNumber()
        {
            Random random = new Random();

            using (var dbContext = new dbEduegateERPContext())
            {

                var visitorNumber = dbContext.Visitors.ToList().Select(x => x.VisitorNumber.Substring(2));

                List<int> generatedNumbers = new List<int>(); 
                generatedNumbers = visitorNumber?.Select(int.Parse).ToList(); // Convert each string to int

                int randomNumber;

                do
                {
                    randomNumber = random.Next(00000, 99999);
                } while (generatedNumbers.Contains(randomNumber));

                return randomNumber;
            }
        }


        public VisitorDTO GetVisitorDetailsByLoginID(long loginID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var visitorDTO = new VisitorDTO();

                var entity = dbContext.Visitors.Where(v => v.LoginID == loginID).Include(i => i.VisitorAttachmentMaps).FirstOrDefault();

                if (entity != null)
                {
                    visitorDTO = ToDTO(entity);

                    visitorDTO.IsError = false;
                }

                return visitorDTO;
            }
        }

    }
}