using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Repository;
using Eduegate.Domain.Repository.Payroll;
using Eduegate.Services.Contracts.Jobs;
using Eduegate.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;

namespace Eduegate.Domain.Mappers
{
    public class JobEntryHeadMapper : IDTOEntityMapper<JobEntryHeadDTO, JobEntryHead>
    {
        private CallContext _context;

        public static JobEntryHeadMapper Mapper(CallContext context)
        {
            var mapper = new JobEntryHeadMapper();
            mapper._context = context;
            return mapper;
        }

        public JobEntryHead ToEntity(JobEntryHeadDTO jobEntryDTO) //Translating job entry head data from DTO to entity
        {
            if (jobEntryDTO.IsNull())
            {
                throw new Exception("jobEntryDTO is null");
            }

            var jobEntryHead = new JobEntryHead()
            {
                JobEntryHeadIID = jobEntryDTO.JobEntryHeadIID,
                BranchID = jobEntryDTO.BranchID > 0 ? jobEntryDTO.BranchID : (long?)null,
                DocumentTypeID = jobEntryDTO.DocumentTypeID > 0 ? jobEntryDTO.DocumentTypeID : (int?)null,
                JobNumber = jobEntryDTO.JobNumber,
                JobStartDate = jobEntryDTO.JobStartDate,
                Remarks = jobEntryDTO.Remarks,
                JobEndDate = jobEntryDTO.JobEndDate,
                ReferenceDocumentTypeID = jobEntryDTO.ReferenceDocumentTypeID,
                TransactionHeadID = jobEntryDTO.TransactionHeadID,
                PriorityID = jobEntryDTO.PriorityID > 0 ? jobEntryDTO.PriorityID : (byte?)null,
                BasketID = jobEntryDTO.BasketID >= 0 ? jobEntryDTO.BasketID : (int?)null,
                JobStatusID = jobEntryDTO.JobStatusID > 0 ? jobEntryDTO.JobStatusID : (int?)null,
                JobSizeID = jobEntryDTO.JobSizeID > 0 ? jobEntryDTO.JobSizeID : (short?)null,
                JobOperationStatusID = jobEntryDTO.JobOperationStatusID > 0 ? jobEntryDTO.JobOperationStatusID : (byte?)null,
                EmployeeID = jobEntryDTO.EmployeeID > 0 ? jobEntryDTO.EmployeeID : (long?)null,
                ProcessStartDate = jobEntryDTO.ProcessStartDate,
                ProcessEndDate = jobEntryDTO.ProcessEndDate,
                VehicleID = jobEntryDTO.VehicleID > 0 ? jobEntryDTO.VehicleID : (long?)null,
                ServiceProviderId = jobEntryDTO.ServiceProviderId > 0 ? jobEntryDTO.ServiceProviderId : (int?)null,
                IsCashCollected = jobEntryDTO.IsCashCollected,
                UpdatedBy = _context != null ? _context.LoginID.HasValue ? int.Parse(_context.LoginID.ToString()) : jobEntryDTO.CreatedBy : jobEntryDTO.CreatedBy,
                UpdatedDate = DateTime.Now,
                CreatedDate = jobEntryDTO.JobEntryHeadIID == 0 ? DateTime.Now : jobEntryDTO.CreatedDate,
                CreatedBy = _context != null ? jobEntryDTO.JobEntryHeadIID == 0 && _context.LoginID.HasValue ? (int)_context.LoginID : jobEntryDTO.CreatedBy : jobEntryDTO.CreatedBy,
                NoOfPacket = jobEntryDTO.PacketNo > 0 ? jobEntryDTO.PacketNo : (byte)0,
                //TimeStamps = jobEntryDTO.TimeStamps == null ? null : Convert.FromBase64String(jobEntryDTO.TimeStamps),
                ParentJobEntryHeadId = jobEntryDTO.ParentJobEntryHeadId > 0 ? jobEntryDTO.ParentJobEntryHeadId : null,
                CompanyID = jobEntryDTO.CompanyID.IsNotNull()? jobEntryDTO.CompanyID : _context.CompanyID,
                JobActivityID = jobEntryDTO.JobActivityID,
                Reason = jobEntryDTO.Reason,
            };

            if (jobEntryHead.JobEntryHeadIID <= 0)
                //jobEntryHead.TimeStamps = null;

            if (jobEntryDTO.OrderContactMap != null)
            {
                jobEntryHead.OrderContactMap = OrderContactMapMapper.Mapper(_context).ToEntity(jobEntryDTO.OrderContactMap);
            }

            if (jobEntryDTO.JobEntryDetails.IsNotNull() && jobEntryDTO.JobEntryDetails.Count > 0)
            {
                var mapper = JobEntryDetailMapper.Mapper(_context);

                foreach (JobEntryDetailDTO jobEntryDetailDTO in jobEntryDTO.JobEntryDetails)
                {
                    if ((jobEntryDetailDTO.ProductSKUID.IsNotNull() && jobEntryDetailDTO.ProductSKUID > 0) || (jobEntryDetailDTO.ParentJobEntryHeadID.IsNotNull() && jobEntryDetailDTO.ParentJobEntryHeadID > 0))
                    {
                        jobEntryHead.JobEntryDetails.Add(mapper.ToEntity(jobEntryDetailDTO));
                    }
                }
            }

            return jobEntryHead;
        }

        public JobEntryHeadDTO ToDTO(JobEntryHead jobEntryHead) //Translating job entry head data from entity to DTO
        {
            if (jobEntryHead.IsNull())
            {
                throw new Exception("jobEntryHead is null");
            }

            var wareHouseRepository = new WarehouseRepository();

            var jobEntryHeadDTO = new JobEntryHeadDTO()
            {
                JobEntryHeadIID = jobEntryHead.JobEntryHeadIID,
                BranchID = jobEntryHead.BranchID,
                BranchName = jobEntryHead.BranchID.HasValue ? new ReferenceDataRepository().GetBranch(jobEntryHead.BranchID.Value).BranchName : null,
                DocumentTypeID = jobEntryHead.DocumentTypeID.HasValue ? jobEntryHead.DocumentTypeID.Value : (int?)null,
                DocumentTypeName = jobEntryHead.DocumentTypeID.HasValue ? new MetadataRepository().GetDocumentType(jobEntryHead.DocumentTypeID.Value).TransactionTypeName : string.Empty,
                JobNumber = jobEntryHead.JobNumber,
                JobStartDate = jobEntryHead.JobStartDate,
                Remarks = jobEntryHead.Remarks,
                JobEndDate = jobEntryHead.JobEndDate,
                TransactionHeadID = jobEntryHead.TransactionHeadID,
                TransactionNumber = jobEntryHead.TransactionHead.IsNotNull() ? jobEntryHead.TransactionHead.TransactionNo : null,
                EmployeeID = jobEntryHead.EmployeeID,
                EmployeeName = jobEntryHead.EmployeeID.HasValue ? new EmployeeRepository().GetEmployeeName(jobEntryHead.EmployeeID.Value) : string.Empty,
                JobStatusID = jobEntryHead.JobStatusID,
                JobStatus = jobEntryHead.JobStatusID.HasValue ? wareHouseRepository.GetJobStatus(Convert.ToInt32(jobEntryHead.JobStatusID)).IsNotNull() ?
                            wareHouseRepository.GetJobStatus(Convert.ToInt32(jobEntryHead.JobStatusID)).StatusName : string.Empty : string.Empty,
                PriorityID = jobEntryHead.PriorityID,
                Priority = jobEntryHead.PriorityID.HasValue ? wareHouseRepository.GetJobPriority(jobEntryHead.PriorityID.Value).Description : string.Empty,
                ReferenceDocumentTypeID = jobEntryHead.ReferenceDocumentTypeID,
                BasketID = jobEntryHead.BasketID,
                JobOperationStatusID = jobEntryHead.JobOperationStatusID,
                JobOperationStatus = jobEntryHead.JobOperationStatusID.HasValue ? wareHouseRepository.GetJobOperation(jobEntryHead.JobOperationStatusID.Value).Description : string.Empty,
                ProcessStartDate = jobEntryHead.ProcessStartDate,
                ProcessEndDate = jobEntryHead.ProcessEndDate,
                VehicleID = jobEntryHead.VehicleID,
                JobSizeID = jobEntryHead.JobSizeID,
                IsCashCollected = jobEntryHead.IsCashCollected.IsNotNull() ? Convert.ToBoolean(jobEntryHead.IsCashCollected) : default(bool),
                VehicleCode = jobEntryHead.VehicleID.HasValue ? new MutualRepository().GetVehicle(jobEntryHead.VehicleID.Value).VehicleCode : string.Empty,
                CreatedBy = jobEntryHead.CreatedBy,
                CreatedDate = jobEntryHead.CreatedDate,
                UpdatedBy = jobEntryHead.UpdatedBy,
                UpdatedDate = jobEntryHead.UpdatedDate,
                //TimeStamps = jobEntryHead.TimeStamps == null ? null : Convert.ToBase64String(jobEntryHead.TimeStamps),
                LoginID = jobEntryHead.EmployeeID.HasValue ? Convert.ToInt64(new EmployeeRepository().GetEmployee((long)jobEntryHead.EmployeeID).LoginID) : 0,
                PacketNo = jobEntryHead.NoOfPacket.HasValue ? jobEntryHead.NoOfPacket.Value : (byte)0,
                ServiceProviderId = jobEntryHead.ServiceProviderId,
                ServiceProviderName = jobEntryHead.ServiceProviderId.HasValue ? new DistributionBL(_context).GetServiceProvider(jobEntryHead.ServiceProviderId.Value).ProviderName : string.Empty,
                ParentJobEntryHeadId = jobEntryHead.ParentJobEntryHeadId,
                CompanyID = jobEntryHead.CompanyID.IsNotNull() ? jobEntryHead.CompanyID : _context.CompanyID,
                JobActivityID = jobEntryHead.JobActivityID,
                Reason = jobEntryHead.Reason,
            };

            if (jobEntryHead.OrderContactMap != null)
            {
                jobEntryHeadDTO.OrderContactMap = OrderContactMapMapper.Mapper(_context).ToDTO(jobEntryHead.OrderContactMap);
            }

            Basket basket = wareHouseRepository.GetBasket(Convert.ToInt64(jobEntryHead.BasketID));

            if (basket.IsNotNull())
                jobEntryHeadDTO.BasketName = basket.BasketCode;
            var mapper = JobEntryDetailMapper.Mapper(_context);

            if (jobEntryHead.JobEntryDetails.IsNotNull() && jobEntryHead.JobEntryDetails.Count > 0)
            {
                jobEntryHeadDTO.JobEntryDetails = new List<JobEntryDetailDTO>();

                foreach (var jobEntryDetail in jobEntryHead.JobEntryDetails)
                {
                    jobEntryHeadDTO.JobEntryDetails.Add(mapper.ToDTO(jobEntryDetail, jobEntryHead.CompanyID.HasValue ? jobEntryHead.CompanyID.Value : _context.CompanyID.Value));
                }
            }

            return jobEntryHeadDTO;
        }

        public JobEntryHead Clone(JobEntryHead jobEntryHead)
        {
            if (jobEntryHead != null)
            {
                return new JobEntryHead()
                {
                    JobEntryHeadIID = jobEntryHead.JobEntryHeadIID,
                    BranchID = jobEntryHead.BranchID,
                    DocumentTypeID = jobEntryHead.DocumentTypeID,
                    JobNumber = jobEntryHead.JobNumber,
                    Remarks = jobEntryHead.Remarks,
                    JobStartDate = jobEntryHead.JobStartDate,
                    JobEndDate = jobEntryHead.JobEndDate,
                    ReferenceDocumentTypeID = jobEntryHead.ReferenceDocumentTypeID,
                    TransactionHeadID = jobEntryHead.TransactionHeadID,
                    PriorityID = jobEntryHead.PriorityID,
                    BasketID = jobEntryHead.BasketID,
                    JobStatusID = jobEntryHead.JobStatusID,
                    JobOperationStatusID = jobEntryHead.JobOperationStatusID,
                    JobSizeID = jobEntryHead.JobSizeID,
                    EmployeeID = jobEntryHead.EmployeeID,
                    ProcessStartDate = jobEntryHead.ProcessStartDate,
                    ProcessEndDate = jobEntryHead.ProcessEndDate,
                    VehicleID = jobEntryHead.VehicleID,
                    IsCashCollected = jobEntryHead.IsCashCollected,
                    CreatedDate = jobEntryHead.CreatedDate,
                    UpdatedDate = jobEntryHead.UpdatedDate,
                    CreatedBy = jobEntryHead.CreatedBy,
                    UpdatedBy = jobEntryHead.UpdatedBy,
                    //TimeStamps = jobEntryHead.TimeStamps,
                    NoOfPacket = jobEntryHead.NoOfPacket,
                    CompanyID = jobEntryHead.CompanyID,
                    JobEntryDetails = jobEntryHead.JobEntryDetails.Select(x => JobEntryDetailMapper.Mapper(_context).Clone(x)).ToList(),
                };
            }
            else return new JobEntryHead();
        }

    }
}
