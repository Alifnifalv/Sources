using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Interfaces;
using Eduegate.Framework;
using Eduegate.Domain.Repository;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Domain.Payroll;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Domain.Entity;
using System.Data.Entity;

namespace Eduegate.Domain.Mappers
{
    public class TransactionHeadMapper : IDTOEntityMapper<TransactionHeadDTO, TransactionHead>
    {
        private CallContext _context;

        public static TransactionHeadMapper Mapper(CallContext context)
        {
            var mapper = new TransactionHeadMapper();
            mapper._context = context;
            return mapper;
        }
        public TransactionHead ToEntity(TransactionHeadDTO dto)
        {
            if (dto != null)
            {

                var transEntitlement = new List<TransactionHeadEntitlementMap>();

                if(dto.TransactionHeadEntitlementMaps.Count > 0)
                {
                    transEntitlement = dto.TransactionHeadEntitlementMaps.Count > 0 ? dto.TransactionHeadEntitlementMaps.Select(x => TransactionHeadEntitlementMapMapper.Mapper(_context).ToEntity(x)).ToList() : null;
                }

                var transactionHead = new TransactionHead()
                {
                    HeadIID = dto.HeadIID,
                    CompanyID = dto.CompanyID,
                    DocumentTypeID = (int)dto.DocumentTypeID,
                    TransactionDate = dto.TransactionDate.HasValue ? Convert.ToDateTime(dto.TransactionDate) : DateTime.Now,
                    CustomerID = dto.CustomerID,
                    StudentID = dto.StudentID,
                    StaffID = dto.StaffID,
                    Description = dto.Description,
                    Reference = dto.Reference,
                    TransactionNo = dto.TransactionNo,
                    SupplierID = dto.SupplierID,
                    TransactionStatusID = dto.TransactionStatusID,
                    DiscountAmount = dto.DiscountAmount,
                    DiscountPercentage = dto.DiscountPercentage,
                    BranchID = dto.BranchID != default(long) ? dto.BranchID : (long?)null,
                    ToBranchID = dto.ToBranchID != default(long) ? dto.ToBranchID : (long?)null,
                    DueDate = dto.DueDate,
                    DeliveryDate = dto.DeliveryDate,
                    CurrencyID = dto.CurrencyID != default(long) && dto.CurrencyID != null ? Convert.ToInt32(dto.CurrencyID) : (int?)null,
                    DeliveryMethodID = dto.DeliveryMethodID > 0 ? (short)dto.DeliveryMethodID : (short?)null, // we are converting becuase in DB we have short and we are passing DeliveryType -- Phy or vir
                    IsShipment = dto.IsShipment,
                    EmployeeID = dto.EmployeeID != default(long) ? Convert.ToInt64(dto.EmployeeID) : (long?)null,
                    EntitlementID = dto.EntitlementID.HasValue && dto.EntitlementID.Value == 0 ? (byte?)null : dto.EntitlementID,
                    UpdatedBy = _context.IsNotNull() ? (_context.LoginID.HasValue ? int.Parse(_context.LoginID.Value.ToString()) : (int?)null) : null,
                    UpdatedDate = DateTime.Now,
                    ReferenceHeadID = dto.ReferenceHeadID,
                    JobEntryHeadID = dto.JobEntryHeadID,
                    DeliveryTypeID = dto.DeliveryTypeID > 0 ? dto.DeliveryTypeID : null,   //Delivery Options like express,super
                    DeliveryCharge = dto.DeliveryCharge,
                    JobStatusID = dto.JobStatusID > 0 ? dto.JobStatusID : null,
                    ReceivingMethodID = dto.ReceivingMethodID > 0 ? dto.ReceivingMethodID : null,
                    ReturnMethodID = dto.ReturnMethodID > 0 ? dto.ReturnMethodID : null,
                    DocumentStatusID = dto.DocumentStatusID > 0 ? dto.DocumentStatusID : null,
                    DocumentCancelledDate = dto.DocumentCancelledDate.HasValue ? Convert.ToDateTime(dto.DocumentCancelledDate) : (DateTime?)null,
                    SchoolID = dto.SchoolID.HasValue ? dto.SchoolID : _context.IsNotNull() && _context.SchoolID.HasValue ? (byte)_context.SchoolID : null,
                    AcademicYearID = dto.AcademicYearID.HasValue ? dto.AcademicYearID : _context.IsNotNull() && _context.AcademicYearID.HasValue ? (int)_context.AcademicYearID : (int?)null,
                    ExchangeRate = dto.ExchangeRate,
                    ForeignAmount = dto.ForeignAmount,
                    TotalLandingCost = dto.TotalLandingCost,
                    LocalDiscount = dto.LocalDiscount,
                    InvoiceLocalAmount= dto.InvoiceLocalAmount,
                    InvoiceForeignAmount=dto.InvoiceForeignAmount,
                    DepartmentID = dto.DepartmentID,
                    ApprovedBy = dto.ApproverID,
                    TransactionHeadEntitlementMaps = transEntitlement,
                    Remarks = dto.Remarks,
                };

                if (dto.HeadIID <= 0)
                {
                    transactionHead.CreatedBy = _context.IsNotNull() ? (_context.LoginID.HasValue ? int.Parse(_context.LoginID.Value.ToString()) : (int?)null) : null;
                    transactionHead.CreatedDate = DateTime.Now;
                }

                return transactionHead;
            }

            else return new TransactionHead();
        }

        public TransactionHeadDTO ToDTO(TransactionHead entity)
        {
            if (entity != null)
            {
                // get the delivery type based on id
                var deliveryTypeDto = new DeliveryTypeDTO();

                if (entity.DeliveryMethodID > 0)
                {
                    deliveryTypeDto = new ReferenceDataBL(_context).GetDeliveryType((short)entity.DeliveryMethodID);
                }
                else
                    deliveryTypeDto = null;

                var transactionStatus = new TransactionStatus();

                if (entity.TransactionStatusID > 0)
                {
                    transactionStatus = new TransactionRepository().GetTransactionStatus((byte)entity.TransactionStatusID);
                }
                else
                    transactionStatus = null;

                var employeeDto = new EmployeeDTO();

                if (entity.EmployeeID > 0)
                {
                    employeeDto = new EmployeeBL(_context).GetEmployee(entity.EmployeeID.Value);
                }
                else
                    employeeDto = null;

                string deliveryOption = string.Empty;

                if (entity.DeliveryTypeID > 0)
                {
                    deliveryOption = new DistributionRepository().GetDeliveryTypeName(entity.DeliveryTypeID.Value);
                }

                // Get Reference Transaction Number
                var refTransaction = entity.ReferenceHeadID > 0 ? new TransactionRepository().GetTransaction(entity.ReferenceHeadID.Value) : null;
                var transactionDTO = new TransactionHeadDTO();

                transactionDTO.HeadIID = entity.HeadIID;
                transactionDTO.Description = entity.Description;
                transactionDTO.Reference = entity.Reference;
                transactionDTO.CompanyID = entity.CompanyID;
                transactionDTO.Remarks = entity.Remarks;

                if (entity.Customer != null)
                {
                    transactionDTO.CustomerID = entity.Customer.CustomerIID;
                    transactionDTO.CustomerName = string.Concat(entity.Customer.FirstName + entity.Customer.MiddleName + " " + entity.Customer.LastName);
                }
                if (entity.Supplier != null)
                {
                    transactionDTO.SupplierID = entity.Supplier.SupplierIID;
                    transactionDTO.SupplierName = string.Concat(entity.Supplier.FirstName + entity.Supplier.MiddleName + " " + entity.Supplier.LastName);
                }

                if (entity.StaffID != null)
                {
                    transactionDTO.StaffID = entity.StaffID;
                    transactionDTO.StaffName = string.Concat(entity.Employee1.EmployeeCode + " - " + entity.Employee1.FirstName + entity.Employee1.MiddleName + " " + entity.Employee1.LastName);
                }

                if (entity.ApprovedBy != null)
                {
                    transactionDTO.ApproverID = entity.ApprovedBy;
                    transactionDTO.ApproverName = string.Concat(entity.ApprovedByNavigation.EmployeeCode + " - "+entity.ApprovedByNavigation.EmployeeName);
                }
                if (entity.DepartmentID != null)
                {
                    transactionDTO.DepartmentID = entity.DepartmentID;
                    transactionDTO.Department = string.Concat(entity.Department.DepartmentName);
                }

                //if (entity.Student != null)
                //{
                //    transactionDTO.StudentID = entity.Student.StudentIID;
                //    transactionDTO.StudentName = string.Concat(entity.Student.FirstName + entity.Student.MiddleName + " " + entity.Student.LastName);
                //}

                transactionDTO.TransactionDate = entity.TransactionDate.IsNull() ? (DateTime?)null : Convert.ToDateTime(entity.TransactionDate);
                transactionDTO.TransactionNo = entity.TransactionNo;
                transactionDTO.TransactionStatusID = entity.TransactionStatusID;
                transactionDTO.DocumentStatusID = entity.DocumentStatusID.HasValue ? short.Parse(entity.DocumentStatusID.ToString()) : (short?)null;
                transactionDTO.DocumentTypeID = entity.DocumentTypeID;
                transactionDTO.DocumentTypeName = entity.DocumentTypeID.HasValue ? new ReferenceDataRepository().GetDocumentType(entity.DocumentTypeID.Value).TransactionTypeName : string.Empty;
                transactionDTO.DiscountAmount = entity.DiscountAmount;
                transactionDTO.DiscountPercentage = entity.DiscountPercentage;
                transactionDTO.BranchID = entity.BranchID != null ? (long)entity.BranchID : default(long);
                transactionDTO.ToBranchID = entity.ToBranchID != null ? (long)entity.ToBranchID : default(long);
                transactionDTO.CurrencyID = entity.CurrencyID != null ? (int)entity.CurrencyID : default(int);
                transactionDTO.DeliveryDate = entity.DeliveryDate != null ? (DateTime)entity.DeliveryDate : (DateTime?)null;
                transactionDTO.DocumentCancelledDate = entity.DocumentCancelledDate.IsNull() ? (DateTime?)null : Convert.ToDateTime(entity.DocumentCancelledDate);
                transactionDTO.DueDate = entity.DueDate != null ? (DateTime)entity.DueDate : (DateTime?)null;
                transactionDTO.EntitlementID = entity.EntitlementID;
                transactionDTO.IsShipment = entity.IsShipment != null ? (bool)entity.IsShipment : default(bool);
                transactionDTO.ExchangeRate = entity.ExchangeRate;
                transactionDTO.ForeignAmount = entity.ForeignAmount;
                transactionDTO.ReferenceHeadID = entity.ReferenceHeadID;
                transactionDTO.JobEntryHeadID = entity.JobEntryHeadID;
                transactionDTO.CreatedBy = entity.CreatedBy;
                transactionDTO.UpdatedBy = entity.UpdatedBy;
                transactionDTO.CreatedDate = entity.CreatedDate.HasValue ? entity.CreatedDate.Value.ToLongDateString() : null;
                transactionDTO.UpdatedDate = entity.UpdatedDate.HasValue ? entity.UpdatedDate.Value.ToLongDateString() : null;
                transactionDTO.DeliveryCharge = entity.DeliveryCharge;
                transactionDTO.JobStatusID = entity.JobStatusID;
                transactionDTO.ExchangeRate = entity.ExchangeRate;
                transactionDTO.InvoiceForeignAmount = entity.InvoiceForeignAmount;
                transactionDTO.InvoiceLocalAmount = entity.InvoiceLocalAmount;
                transactionDTO.DeliveryTypeName = entity.DeliveryType.IsNotNull() ? entity.DeliveryType.DeliveryMethod : null;
                var documentReference = new MetadataRepository().GetDocumentReferenceTypesByDocumentType((int)entity.DocumentTypeID);
                transactionDTO.DocumentReferenceType = ((Eduegate.Services.Contracts.Enums.DocumentReferenceTypes)Enum.Parse(typeof(Eduegate.Services.Contracts.Enums.DocumentReferenceTypes),
                    documentReference.ReferenceTypeID.ToString())).ToString();

                if (refTransaction.IsNotNull())
                {
                    transactionDTO.ReferenceTransactionNo = refTransaction.TransactionNo;
                    transactionDTO.PaidAmount = refTransaction.TransactionHeadEntitlementMaps.Count() > 0 ? refTransaction.TransactionHeadEntitlementMaps.Sum(x => x.Amount) : 0;
                }

                if (entity.Currency.IsNotNull())
                {
                    transactionDTO.CurrencyID = entity.Currency.CurrencyID;
                    transactionDTO.CurrencyName = entity.Currency.Name;
                }

                if (deliveryTypeDto.IsNotNull())
                {
                    transactionDTO.DeliveryMethodID = deliveryTypeDto.DeliveryTypeID;
                    transactionDTO.DeliveryTypeName = deliveryTypeDto.DeliveryMethod;
                }
                if (entity.ReceivingMethodID.IsNotNull())
                {
                    transactionDTO.ReceivingMethodID = entity.ReceivingMethodID;
                    transactionDTO.ReceivingMethodName = entity.ReceivingMethod.IsNotNull() ? entity.ReceivingMethod.ReceivingMethodName : string.Empty;
                }
                if (entity.ReturnMethodID.IsNotNull())
                {
                    transactionDTO.ReturnMethodID = entity.ReturnMethodID;
                    transactionDTO.ReturnMethodName = entity.ReturnMethod.IsNotNull() ? entity.ReturnMethod.ReturnMethodName : string.Empty;
                }

                if (transactionStatus.IsNotNull())
                {
                    transactionDTO.TransactionStatusID = transactionStatus.TransactionStatusID;
                    transactionDTO.TransactionStatusName = transactionStatus.Description;

                    //if null take set it from the transaction status
                    if (!transactionDTO.DocumentStatusID.HasValue)
                    {
                        if (transactionDTO.TransactionStatusID == (byte)Eduegate.Framework.Enums.TransactionStatus.Complete ||
                            transactionDTO.TransactionStatusID == (byte)Eduegate.Framework.Enums.TransactionStatus.Confirmed)
                        {
                            transactionDTO.DocumentStatusID = (short)Eduegate.Services.Contracts.Enums.DocumentStatuses.Completed;
                        }
                        else
                        {
                            transactionDTO.DocumentStatusID = (short)Eduegate.Services.Contracts.Enums.DocumentStatuses.Draft;
                        }
                    }
                    else // transaction engine is missed to update the document status, should handle it here.
                    {
                        if (transactionDTO.TransactionStatusID == (int)Eduegate.Services.Contracts.Enums.TransactionStatus.IntitiateReprecess
                            || transactionDTO.TransactionStatusID == (int)Eduegate.Services.Contracts.Enums.TransactionStatus.InProcess)
                        {
                            transactionDTO.DocumentStatusID = (short)Eduegate.Services.Contracts.Enums.DocumentStatuses.Completed;
                        }
                        else
                        {
                            transactionDTO.DocumentStatusID = entity.DocumentStatusID;
                        }
                    }
                }

                if (employeeDto.IsNotNull())
                {
                    transactionDTO.EmployeeID = employeeDto.EmployeeIID;
                    transactionDTO.EmployeeName = employeeDto.EmployeeCode +" - "+ employeeDto.EmployeeName;
                }

                if (!string.IsNullOrWhiteSpace(deliveryOption))
                {
                    transactionDTO.DeliveryTypeID = entity.DeliveryTypeID;
                    transactionDTO.DeliveryOption = deliveryOption;
                }

                transactionDTO.DocumentStatusName = transactionDTO.DocumentStatusID.HasValue ? new MutualRepository().GetDocumentStatus(transactionDTO.DocumentStatusID.Value).StatusName : string.Empty;
                // Get Entitlements
                transactionDTO.Entitlements = entity.HeadIID > 0 ? new TransactionBL(_context).GetEntitlementsByHeadIds(entity.HeadIID) : null;
                // if transactionDTO.Entitlements null then
                if (transactionDTO.Entitlements.IsNull() || transactionDTO.Entitlements.Count == 0)
                {
                    // if don't have a record in [inventory].[TransactionHeadEntitlementMap] then this code will run
                    transactionDTO.Entitlements = entity.HeadIID > 0 && entity.EntitlementID.IsNotNull()
                        ? new List<KeyValueDTO>() { new MutualBL(_context).GetEntitlementById((short)entity.EntitlementID) } : null;
                }

                if (entity.StudentID != null)
                {
                    transactionDTO.StudentID = entity.StudentID;
                    StudentDetail(transactionDTO, transactionDTO.StudentID);
                }
                transactionDTO.AgainstReferenceHeadID = GetReferenceHeadID(entity.HeadIID);

                return transactionDTO;
            }

            else return new TransactionHeadDTO();
        }

        private TransactionHeadDTO StudentDetail(TransactionHeadDTO transactionDTO, long? studentID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                string defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");

                var student = dbContext.Students.Where(a => a.StudentIID == studentID)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.Parent)
                    .AsNoTracking()
                    .FirstOrDefault();

                //TODO: Need to check why include not bind data
                student.Class = student.Class ?? dbContext.Classes.AsNoTracking().FirstOrDefault(x => x.ClassID == student.ClassID);
                student.Section = student.Section ?? dbContext.Sections.AsNoTracking().FirstOrDefault(x => x.SectionID == student.SectionID);
                student.Parent = student.Parent ?? dbContext.Parents.AsNoTracking().FirstOrDefault(x => x.ParentIID == student.ParentID);

                transactionDTO.StudentName = student.AdmissionNumber + "-" + student.FirstName + " " + student.MiddleName + " " + student.LastName;
                transactionDTO.StudentClassSectionDescription = student.Class?.ClassDescription + " " + student.Section?.SectionName;
                transactionDTO.EmailID = student?.Parent?.GaurdianEmail ?? defaultMail;
                transactionDTO.SchoolID = student.SchoolID;

                return transactionDTO;
            }
        }

        public List<TransactionHeadDTO> TodTO(List<TransactionHead> entities)
        {
            var dtos = new List<TransactionHeadDTO>();

            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public OrderDetailDTO FromEntityToDTO(TransactionHead entity)
        {
            if (entity != null)
            {
                var orderDetail = new OrderDetailDTO()
                {
                    HeadIID = entity.HeadIID,
                    CompanyID = entity.CompanyID.HasValue ? entity.CompanyID.Value : default(int?),
                    BranchID = entity.BranchID.HasValue ? (long)entity.BranchID.Value : default(long?),
                    ToBranchID = entity.ToBranchID.HasValue ? (long)entity.ToBranchID.Value : default(long?),
                    DocumentTypeID = entity.DocumentTypeID != null ? (int)entity.DocumentTypeID : default(int?),
                    TransactionNo = entity.TransactionNo,
                    Description = entity.Description,
                    CustomerID = entity.CustomerID.HasValue ? entity.CustomerID.Value : default(long?),
                    SupplierID = entity.SupplierID.HasValue ? entity.SupplierID.Value : default(long?),
                    StudentID = entity.StudentID.HasValue ? entity.StudentID.Value : default(long?),
                    DiscountAmount = entity.DiscountAmount,
                    DiscountPercentage = entity.DiscountPercentage,
                    TransactionDate = entity.TransactionDate,
                    DueDate = entity.DueDate != null ? (DateTime)entity.DueDate : default(DateTime?),
                    DeliveryDate = entity.DeliveryDate,
                    TransactionStatusID = entity.TransactionStatusID.HasValue ? (byte)entity.TransactionStatusID.Value : default(byte?),
                    EntitlementID = entity.EntitlementID,
                    JobEntryHeadID = entity.JobEntryHeadID.HasValue ? (long)entity.JobEntryHeadID.Value : default(long?),
                    DeliveryMethodID = entity.DeliveryMethodID.HasValue ? (short)entity.DeliveryMethodID.Value : default(short?),
                    DeliveryDays = entity.DeliveryDays,
                    CurrencyID = entity.CurrencyID.HasValue ? (int)entity.CurrencyID.Value : default(int?),
                    ExchangeRate = entity.ExchangeRate,
                    IsShipment = entity.IsShipment.HasValue ? (bool)entity.IsShipment.Value : default(bool),
                    EmployeeID = entity.EmployeeID.HasValue ? (long)entity.EmployeeID.Value : default(long),
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    DeliveryCharge = entity.DeliveryCharge,
                    DeliveryTypeID = entity.DeliveryTypeID.HasValue ? (int)entity.DeliveryTypeID.Value : default(int?),
                    JobStatusID = entity.JobStatusID.HasValue ? (int)entity.JobStatusID.Value : default(int?),
                    DocumentStatusID = entity.DocumentStatusID,
                    DeliveryType = new DistributionRepository().GetDeliveryTypeName((int)entity.DeliveryTypeID),

                    // Map orderDetails using mapper
                    orderDetails = entity.OrderContactMaps.Select(x => OrderContactMapMapper.Mapper(_context).ToDTO(x)).ToList()
                };

                return orderDetail;
            }

            else return new OrderDetailDTO();
        }

        public TransactionHead FromDTOToEntity(OrderDetailDTO dto)
        {
            if (dto != null)
            {
                var transactionHead = new TransactionHead()
                {
                    HeadIID = dto.HeadIID,
                    CompanyID = dto.CompanyID,
                    DocumentTypeID = (int)dto.DocumentTypeID,
                    TransactionDate = dto.TransactionDate.IsNotNull() ? Convert.ToDateTime(dto.TransactionDate) : DateTime.Now,
                    CustomerID = dto.CustomerID,
                    StudentID = dto.StudentID,
                    Description = dto.Description,
                    TransactionNo = dto.TransactionNo,
                    SupplierID = dto.SupplierID,
                    TransactionStatusID = dto.TransactionStatusID,
                    DiscountAmount = dto.DiscountAmount,
                    DiscountPercentage = dto.DiscountPercentage,
                    BranchID = dto.BranchID != default(long) ? dto.BranchID : (long?)null,
                    ToBranchID = dto.ToBranchID != default(long) ? dto.ToBranchID : (long?)null,
                    DueDate = dto.DueDate,
                    DeliveryDate = dto.DeliveryDate,
                    DeliveryDays = dto.DeliveryDays,
                    CurrencyID = dto.CurrencyID != default(long) && dto.CurrencyID != null ? Convert.ToInt32(dto.CurrencyID) : (int?)null,
                    DeliveryMethodID = dto.DeliveryMethodID > 0 ? (short)dto.DeliveryMethodID : (short?)null, // we are converting becuase in DB we have short and we are passing DeliveryTypeID
                    IsShipment = dto.IsShipment,
                    EmployeeID = dto.EmployeeID != default(long) ? Convert.ToInt64(dto.EmployeeID) : (long?)null,
                    EntitlementID = dto.EntitlementID,
                    UpdatedBy = _context.IsNotNull() ? (_context.LoginID.HasValue ? int.Parse(_context.LoginID.Value.ToString()) : (int?)null) : null,
                    UpdatedDate = DateTime.Now,
                    CreatedDate = dto.CreatedDate,
                    CreatedBy = _context.IsNotNull() ? (_context.LoginID.HasValue ? int.Parse(_context.LoginID.Value.ToString()) : (int?)null) : null,
                    ReferenceHeadID = dto.ReferenceHeadID,
                    JobEntryHeadID = dto.JobEntryHeadID,
                    DeliveryTypeID = dto.DeliveryTypeID,
                    DeliveryCharge = dto.DeliveryCharge,
                    JobStatusID = dto.JobStatusID > 0 ? dto.JobStatusID : null,
                    DocumentStatusID = dto.DocumentStatusID > 0 ? dto.DocumentStatusID : null,
                    TransactionRole = dto.TransactionRole > 0 ? dto.TransactionRole : null,
                    OrderContactMaps = dto.orderDetails.Select(x => OrderContactMapMapper.Mapper(_context).ToEntity(x)).ToList()
                };

                return transactionHead;
            }

            else return new TransactionHead();
        }

        public TransactionHeadDTO ToDTOReference(TransactionHead entity)
        {
            return new TransactionHeadDTO()
            {
                HeadIID = entity.HeadIID,
                TransactionDate = entity.TransactionDate,
                TransactionNo = entity.TransactionNo,
                TransactionLoyaltyPoints = entity.TransactionHeadPointsMaps.FirstOrDefault().LoyaltyPoints
            };
        }
        public List<TransactionHeadDTO> ToDTOReferenceList(List<TransactionHead> entityList)
        {
            var transactionList = new List<TransactionHeadDTO>();
            foreach (var entity in entityList)
            {
                transactionList.Add(ToDTOReference(entity));
            }
            return transactionList;
        }

        public long GetReferenceHeadID(long? IID)
        {
            long referenceHeadID = new long();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var alreadyDoneTransactions = dbContext.TransactionHeads.Where(a => a.ReferenceHeadID == IID).AsNoTracking().FirstOrDefault();
                if (alreadyDoneTransactions != null)
                    referenceHeadID = alreadyDoneTransactions.HeadIID;
            }

            return referenceHeadID;
        }

        public TransactionHeadDTO GetTransactionHeadDetailsByID(long headID)
        {
            var tranHeadDTO = new TransactionHeadDTO();

            using (var dbContext = new dbEduegateERPContext())
            {
                var transHead = dbContext.TransactionHeads.Where(t => t.HeadIID == headID).AsNoTracking().FirstOrDefault();

                tranHeadDTO = new TransactionHeadDTO()
                {
                    HeadIID = transHead.HeadIID,
                    StudentID = transHead.StudentID,
                };

            }

            return tranHeadDTO;
        }

    }
}
