using System;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;
using Eduegate.Framework;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Domain.Mappers;
using Eduegate.Framework.Security;
using System.Linq;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Domain
{
    public class VoucherBL
    {
        private VoucherRepository voucherRepository = new VoucherRepository();
        private CustomerRepository customerRepository = new CustomerRepository();
        private WalletRepository walletRepository = new WalletRepository();
        private ShoppingCartRepository shoppingCartRepository = new ShoppingCartRepository();

        private CallContext _callContext;

        public VoucherBL(CallContext callContext)
        {
            _callContext = callContext;
        }

        /// <summary>
        /// Get Voucher Balance using voucher number
        /// </summary>
        /// <param name="voucherNumber"></param>
        /// <returns>decimal balance amount</returns>
        public decimal GetVoucherBalance(string voucherNumber)
        {
            decimal VoucherBalance = 0;
            VoucherBalance = voucherRepository.GetVoucherBalance(voucherNumber);

            return VoucherBalance;
        }

        public bool UpdateVoucherMapStatus(string voucherNumber)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get Voucher Detail using voucher number.
        /// </summary>
        /// <param name="voucherNumber"></param>
        /// <returns>VoucherMaster object</returns>
        public VoucherMaster GetVoucherDetail(string voucherNumber)
        {
            return voucherRepository.GetVoucherDetail(voucherNumber);
        }

        public bool UpdateVoucher(Eduegate.Framework.Helper.Enums.Status voucherMapStatus, CallContext callContext, long transactionHeadId = 0)
        {
            //get cart for this user
            //var cart = shoppingCartRepository.IsUserCartExist(callContext.UserId, (int)ShoppingCartStatus.CheckedOut);
            var cart = shoppingCartRepository.GetCartDetail(callContext.UserId, (int)ShoppingCartStatus.CheckedOut, new ShoppingCartBL(_callContext).GetSiteID(_callContext));

            //update vouchermap and voucher
            return voucherRepository.UpdateVoucher(Convert.ToInt64(cart.ShoppingCartIID), voucherMapStatus, Convert.ToInt64(callContext.UserId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="voucher"></param>
        /// <returns></returns>
        public bool UpdateVoucherBalance(string voucherNumber, string usedAmount)
        {
            return voucherRepository.UpdateVoucherBalance(voucherNumber, Convert.ToDecimal(usedAmount));
        }

        public VoucherValidityDTO ValidateVoucher(string voucherNumber, string userGUID)
        {
            VoucherValidityDTO voucherDTO = new VoucherValidityDTO();

            var walletCustomerLog = walletRepository.GetWalletCustomer(userGUID);

            //validate voucher with voucher number and customer id
            var voucher = voucherRepository.ValidateVoucher(voucherNumber, walletCustomerLog.CustomerId);

            //if voucher
            if (voucher.IsNotNull())
            {
                voucherDTO.VoucherNumber = voucher.VoucherNo;
                // if voucher has balance
                if (voucher.CurrentBalance > 0)
                {
                    voucherDTO.Status = Convert.ToInt16(Framework.Payment.VoucherStatus.Available);
                    voucherDTO.Amount = Convert.ToString(voucher.CurrentBalance);
                    voucherDTO.Info = String.Format("You have {0} KD in voucher.", Utility.FormatDecimal(Convert.ToDecimal(voucher.CurrentBalance), 3)); //KD right now
                }
                else
                {
                    //check if voucher transferred to wallet or used for order transaction
                    //VoucherTransaction VoucherOrderTransaction = voucherRepository.IsOrderVoucher(voucherNumber);
                    //VoucherWalletTransaction VoucherWalletTransactionDetail = voucherRepository.IsWalletVoucher(voucherNumber);

                    //get user detail by customer id
                    //var customer = customerRepository.GetCustomer(walletCustomerLog.CustomerId);
                    //string customerName = customer.FirstName + " " + customer.LastName;

                    voucherDTO.Status = Convert.ToInt16(Framework.Payment.VoucherStatus.Redeemed);
                    voucherDTO.Amount = "0";
                    voucherDTO.Info = String.Format("Voucher is already used.");
                }
            }
            else
            {
                //invalid voucher
                voucherDTO.Status = Convert.ToInt16(Framework.Payment.VoucherStatus.Invalid);
                voucherDTO.Amount = "0";
                voucherDTO.Info = "Invalid voucher";
            }

            //create DTO accordingly and send back
            return voucherDTO;
        }

        public VoucherMasterDTO GetVoucher(string voucherID)
        {
            return FromEntity(voucherRepository.GetVoucher(long.Parse(voucherID)));
        }

        public VoucherMasterDTO SaveVoucher(VoucherMasterDTO dto)
        {
            if (dto.ValidTillDate < DateTime.Now)
            {
                dto.IsError = true;
                dto.ErrorCode = ErrorCodes.Voucher.V001;
                return dto;
            }
            dto.MinAmount = dto.VoucherID == 0 && voucherRepository.GetVoucherTypeMinRequired(byte.Parse(dto.VoucherType)) == true ? 0 : dto.MinAmount;
            var updatedEntity = voucherRepository.SaveVoucher(ToEntity(dto, _callContext));
            return FromEntity(updatedEntity);

        }

        public static VoucherMasterDTO FromEntity(Voucher entity)
        {
            if (entity == null) return new VoucherMasterDTO();

            // Get encryption hash
            //var hash = new SettingRepository().GetSettingDetail("SERIALPASSPHRASE").SettingValue;
            var hashSetting = new SettingRepository().GetSettingDetail("SERIALPASSPHRASE");
            var hash = hashSetting != null ? hashSetting.SettingValue : "EDUEGATE";

            var dto = new VoucherMasterDTO()
            {
                CurrentBalance = entity.CurrentBalance.HasValue ? entity.CurrentBalance.Value : entity.Amount.Value,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                VoucherID = entity.VoucherIID,
                UpdatedDate = entity.UpdatedDate,
                ////TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                StatusID = entity.StatusID,
                VoucherType = entity.VoucherTypeID.ToString(),
                //VoucherPoint = entity.VoucherPoint,

                // IsSharable = entity.IsSharable.Value,
                VoucherPin = entity.VoucherPin,
                VoucherNo = entity.VoucherNo.IsNotNullOrEmpty() ? StringCipher.Decrypt(entity.VoucherNo, hash) : null,
                VoucherAmount = entity.Amount.Value,
                MinAmount = entity.MinimumAmount,
                ValidTillDate = entity.ExpiryDate,
                GenerateDate = entity.CreatedDate,
                Description = entity.Description,
                CompanyID = entity.CompanyID
            };

            if (entity.Customer.IsNotNull())
            {
                dto.Customer = new KeyValueDTO { Key = entity.Customer.CustomerIID.ToString(), Value = entity.Customer.FirstName + " " + entity.Customer.LastName };
                dto.CustomerID = entity.CustomerID;
            }

            return dto;
        }

        public static Voucher ToEntity(VoucherMasterDTO dto, CallContext _callContext)
        {

            // Get encryption hash
            //var hash = new SettingRepository().GetSettingDetail("SERIALPASSPHRASE").SettingValue;
            var hashSetting = new SettingRepository().GetSettingDetail("SERIALPASSPHRASE");
            var hash = hashSetting != null ? hashSetting.SettingValue : "EDUEGATE";
            var entity = new Voucher()
            {
                //CurrentBalance = dto.CurrentBalance, // do not populate current balance while creation
                CreatedBy = dto.CreatedBy,
                UpdatedBy = _callContext.IsNotNull() ? _callContext.LoginID.HasValue ? int.Parse(_callContext.LoginID.Value.ToString()) : (int?)null : null,
                CreatedDate = dto.CreatedDate,
                VoucherIID = dto.VoucherID,
                UpdatedDate = DateTime.Now,
                ////TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                StatusID = byte.Parse(dto.StatusID.Value.ToString()),
                VoucherTypeID = byte.Parse(dto.VoucherType),
                //VoucherPoint = entity.VoucherPoint,
                CustomerID = dto.Customer.IsNotNull() ? Convert.ToInt64(dto.Customer.Key) : (long?)null,
                IsSharable = dto.IsSharable,
                VoucherPin = dto.VoucherPin,
                VoucherNo = StringCipher.Encrypt(dto.VoucherNo, hash), // encrypt voucher number
                Amount = dto.VoucherAmount,
                MinimumAmount = dto.MinAmount,
                ExpiryDate = dto.ValidTillDate,
                Description = dto.Description,
                CompanyID = _callContext.IsNotNull() ? _callContext.CompanyID.HasValue ? _callContext.CompanyID.Value : default(int?) : default(int?),
            };

            /*
             If customer is selected voucher non-shareable else shareable 
             */
            if (dto.Customer.IsNotNull())
            {
                entity.CustomerID = Convert.ToInt64(dto.Customer.Key);
                entity.IsSharable = false;
            }
            else
            {
                entity.IsSharable = true;
            }

            if (entity.CompanyID == 0)
            {
                entity.CompanyID = _callContext.IsNotNull() ? _callContext.CompanyID.HasValue ? _callContext.CompanyID.Value : (int?)null : null;
            }

            if (entity.VoucherIID == 0)
            {
                if (_callContext.IsNotNull())
                {
                    entity.CreatedBy = _callContext.LoginID.HasValue ? int.Parse(_callContext.LoginID.Value.ToString()) : (int?)null;
                }

                entity.CreatedDate = DateTime.Now;
            }
            else
            {
                entity.CurrentBalance = dto.CurrentBalance;
            }

            return entity;
        }

        public Eduegate.Services.Contracts.VouchersDTO GetVoucherDetails(string voucherNo, long loginID, decimal cartAmount)
        {
            var voucher = voucherRepository.GetVoucherByVoucherNo(voucherNo);
            var voucherDTO = VouchersMapper.Mapper(_callContext).ToDTO(voucher);
            var customerID = new AccountBL(_callContext).GetCustomerIDbyLoginID(loginID);
            if (!string.IsNullOrEmpty(voucher.VoucherNo))
            {
                var minAmount = voucher.MinimumAmount.HasValue ? voucher.MinimumAmount : 0;
                if (minAmount == 0 || minAmount <= cartAmount)
                {
                    var currentBalance = voucher.CurrentBalance.HasValue ? voucher.CurrentBalance : 0;
                    if (voucher.IsSharable == true)
                    {
                        if (currentBalance > 0)
                        {
                            if (voucher.Amount >= cartAmount)
                            {
                                voucherDTO.Amount = cartAmount;
                            }
                            else
                            {
                                voucherDTO.Amount = voucher.Amount;
                            }
                        }
                        else
                        {
                            voucherDTO.VoucherMessage = "No balance available";
                        }
                    }
                    else
                    {
                        if (voucher.CustomerID == customerID)
                        {
                            if (currentBalance > 0)
                            {
                                if (voucher.Amount >= cartAmount)
                                {
                                    voucherDTO.Amount = cartAmount;
                                }
                                else
                                {
                                    voucherDTO.Amount = voucher.Amount;
                                }
                            }
                            else
                            {
                                voucherDTO.VoucherMessage = "No balance available";
                            }
                        }
                        else
                        {
                            voucherDTO.VoucherMessage = "Invalid Voucher No";
                        }
                    }
                }
                else
                {
                    voucherDTO.VoucherMessage = "Cart Amount should be atleast " + minAmount + " KD to use this voucher";
                }
            }
            else
            {
                voucherDTO.VoucherMessage = "Invalid Voucher No";
            }
            return voucherDTO;
        }

        public string CreateVoucher()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
        }
    }
}