using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Framework.Logs;
using Eduegate.Framework.Extensions;
using System.Diagnostics;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Services.Contracts.Checkout;
using System.Data;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace Eduegate.Domain.Repository
{
    public class VoucherRepository
    {
        public decimal GetVoucherBalance(string voucherNumber)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                DateTime today = DateTime.Now.Date;

                return dbContext.VoucherMasters
                    .Where(x => x.VoucherNo == voucherNumber && x.ValidTillDate > today.Date)
                    .AsNoTracking()
                    .Select(x => x.CurrentBalance)
                    .FirstOrDefault();
            }
        }

        public VoucherMaster GetVoucherDetail(string voucherNumber)
        {
            DateTime today = DateTime.Now.Date;

            using (var dbContext = new dbEduegateERPContext())
            {
                return dbContext.VoucherMasters
                    .Where(x => x.VoucherNo == voucherNumber && x.ValidTillDate > today.Date)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
            throw new NotImplementedException();
        }

        public bool UpdateVoucherBalance(string voucherNumber, decimal usedAmount)
        {
            bool Response = false;

            var Voucher = new Voucher();
            try
            {
                using (var dbContext = new dbEduegateERPContext())
                {
                    //select voucher using voucher number
                    Voucher = dbContext.Vouchers.Where(x => x.VoucherNo == voucherNumber)
                        .AsNoTracking()
                        .FirstOrDefault();

                    if (Voucher.IsNotNull())
                    {
                        //update selected voucher with new balance
                        Voucher.CurrentBalance = Voucher.CurrentBalance - usedAmount;
                        Voucher.StatusID = 3; //do we have to set redeem to true if full amount is not used?
                        dbContext.SaveChanges();
                        Response = true;
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to update voucher balance. voucher no:" + voucherNumber, TrackingCode.Voucher); //
            }
            return Response;
        }

        public bool UpdateVoucher(long shoppingCartID, Eduegate.Framework.Helper.Enums.Status voucherStatus, long CustomerID)
        {
            var exit = false;
            byte status = Convert.ToByte(voucherStatus);
            using (var db = new dbEduegateERPContext())
            {
                //update vouchermap
                var cartVoucherMap = db.ShoppingCartVoucherMaps.Where(x => x.ShoppingCartID == shoppingCartID && x.StatusID == (byte)Eduegate.Framework.Helper.Enums.Status.Valid)
                    .AsNoTracking()
                    .FirstOrDefault();
                if (cartVoucherMap.IsNull())
                    return exit;

                cartVoucherMap.StatusID = (byte)voucherStatus;

                //update voucher
                var voucher = db.Vouchers.Where(x => x.VoucherIID == cartVoucherMap.VoucherID)
                    .AsNoTracking()
                    .FirstOrDefault();
                if (voucher.IsNull())
                    return exit;

                voucher.StatusID = (byte)VoucherStatuses.Redeemed;

                if (voucher.CurrentBalance.IsNotNull())
                    voucher.CurrentBalance = voucher.CurrentBalance - cartVoucherMap.Amount;
                else
                    voucher.CurrentBalance = voucher.Amount - cartVoucherMap.Amount;


                db.SaveChanges();
                exit = true;
            }
            return exit;
        }

        public Voucher ValidateVoucher(string voucherNumber, long customerID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                DateTime today = DateTime.Now.Date;

                return dbContext.Vouchers
                    .Where(x => x.VoucherNo == voucherNumber
                        && x.ExpiryDate > today.Date
                        && x.CustomerID == customerID
                        )
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public VoucherTransaction IsOrderVoucher(string voucherNumber)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                return dbContext.VoucherTransactions
                    .Where(x => x.VoucherNo == voucherNumber).OrderByDescending(x => x.TransID)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public VoucherWalletTransaction IsWalletVoucher(string voucherNumber)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                return dbContext.VoucherWalletTransactions
                    .Where(x => x.VoucherNo == voucherNumber).OrderByDescending(x => x.TransID)
                    .FirstOrDefault();
            }
        }

        public List<VoucherStatus> GetVoucherStatuses()
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                return dbContext.VoucherStatuses.OrderBy(a => a.StatusName)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public List<VoucherType> GetVoucherTypes()
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                return dbContext.VoucherTypes.OrderBy(a => a.VoucherTypeName)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public Voucher GetVoucher(long voucherID)
        {
            Voucher entity = null;

            try
            {
                using (var dbContext = new dbEduegateERPContext())
                {
                    entity = dbContext.Vouchers.Include(i => i.VoucherType)
                        .Include(i => i.VoucherStatus)
                        .Include(i => i.Customer)
                        .Where(x => x.VoucherIID == voucherID)
                        .AsNoTracking()
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the Vouchers", TrackingCode.ERP);
            }

            return entity;
        }

        public Voucher SaveVoucher(Voucher voucher)
        {
            Voucher updatedEntity = null;

            try
            {
                using (var dbContext = new dbEduegateERPContext())
                {
                    dbContext.Vouchers.Add(voucher);

                    if (voucher.VoucherIID == 0)
                        dbContext.Entry(voucher).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    else
                        dbContext.Entry(voucher).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    dbContext.SaveChanges();
                    updatedEntity = dbContext.Vouchers
                        .Include(i => i.VoucherType)
                        .Include(i => i.VoucherStatus)
                        .Include(i => i.Customer)
                        .Where(x => x.VoucherIID == voucher.VoucherIID)
                        .AsNoTracking()
                        .FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return updatedEntity;
        }

        public Voucher GetVoucherByVoucherNo(string voucherNo)
        {
            var voucher = new Voucher();

            DateTime today = DateTime.Now.Date;
            using (var dbContext = new dbEduegateERPContext())
            {
                voucher = dbContext.Vouchers.Where(a => a.VoucherNo == voucherNo && a.StatusID == (int)Eduegate.Framework.Enums.VoucherStatus.Active && a.ExpiryDate > today.Date)
                    .AsNoTracking()
                    .FirstOrDefault();
                if (voucher == null)
                {
                    voucher = new Voucher();
                }
            }

            return voucher;
        }

        public bool VoucherTransaction(CheckoutPaymentDTO dto)
        {
            using (var db = new dbEduegateERPContext())
            {
                var voucher = db.Vouchers.Where(a => a.VoucherNo == dto.VoucherNo)
                    .AsNoTracking()
                    .FirstOrDefault();
                if (voucher != null)
                {
                    var currentbal = voucher.CurrentBalance - dto.VoucherAmount;
                    voucher.CurrentBalance = currentbal;
                    if (currentbal == 0)
                    {
                        voucher.StatusID = (int)Eduegate.Framework.Enums.VoucherStatus.Redeemed;
                    }
                    db.ShoppingCartVoucherMaps.Add(new ShoppingCartVoucherMap() { ShoppingCartID = long.Parse(dto.ShoppingCartID), VoucherID = voucher.VoucherIID, Amount = dto.VoucherAmount });
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public ShoppingCartVoucherMap GetVoucherByCartID(long cartID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                return dbContext.ShoppingCartVoucherMaps.Where(x => x.ShoppingCartID == cartID)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public bool GetVoucherTypeMinRequired(byte voucherTypeID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                return dbContext.VoucherTypes.Where(x => x.VoucherTypeID == voucherTypeID && x.IsMinimumAmtRequired == false)
                    .AsNoTracking()
                    .Any();
            }
        }


        public string CreateVoucher()
        {
            using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
            using (SqlCommand cmd = new SqlCommand("inventory.GenerateVoucher", conn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@VoucherType", SqlDbType.VarChar));
                adapter.SelectCommand.Parameters["@VoucherType"].Value = "Num";

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@Length", SqlDbType.Int));
                adapter.SelectCommand.Parameters["@Length"].Value = 12;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@VoucherNo", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters["@VoucherNo"].Direction = ParameterDirection.Output;

                //the SqlDataAdapter automatically does this (and closes the connection, too)

                DataSet dt = new DataSet();
                adapter.Fill(dt);

                return cmd.Parameters["@VoucherNo"].Value.ToString();

            }
            /** using ado.net **/

        }
    }
}