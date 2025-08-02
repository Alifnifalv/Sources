using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.MobileAppWrapper;
using Eduegate.Services.Contracts.Mutual;

namespace Eduegate.Services
{
    public class ECommerceService : BaseService, IECommerceService
    {
        //public async Task<OperationResultDTO> UpdateCartItemStatus(long jobDetailID, byte jobOperationStatusId,
        //    bool applyToAll = false, string barCode = "", long detailId = 0)
        //{
        //    return await new WarehouseBL(CallContext)
        //        .UpdateCartItemStatus(jobDetailID, jobOperationStatusId, applyToAll, barCode, detailId);
        //}

        //public async Task<JobStatusDto> UpdateJobStatus(long jobHeadId, byte jobOperationStatusId)
        //{
        //    return await new WarehouseBL(CallContext).UpdateJobStatus(jobHeadId, 0, jobOperationStatusId, 0);
        //}

        //public async Task<string> GetCartForOnlineCheckOutRequest(int paymentMethodID)
        //{
        //    return await new AppDataBL(this.CallContext).GetCartForOnlineCheckOutRequest(paymentMethodID);
        //}

        //public async Task<long?> GetNearestArea(string geoLocation)
        //{
        //    return await new AppDataBL(this.CallContext, _dbContext, _backgroundJobs).GetNearestArea(geoLocation);
        //}

        //public async Task<long?> GetNearestBranch(string geoLocation)
        //{
        //    return await new AppDataBL(this.CallContext).GetNearestBranch(geoLocation);
        //}

        //public async Task<BranchDTO> GetNearestBranchDetails(string geoLocation)
        //{
        //    return await new AppDataBL(this.CallContext).GetNearestBranchDetails(geoLocation);
        //}

        //public async Task<List<BranchDTO>> GetNearestBranchByOrder(string geoLocation)
        //{
        //    return await new AppDataBL(this.CallContext, _dbContext, _backgroundJobs).GetNearestBranchByOrder(geoLocation);
        //}

        //public async Task<List<PromotionDTO>> GetPromotionsByType(int promotionType)
        //{
        //    return await new AppDataBL(this.CallContext, _dbContext, _backgroundJobs).GetPromotionsByType(promotionType, null);
        //}

        //public async Task<List<NotificationAlertsDTO>> GetPushNotifications(string status, int currentPage)
        //{
        //    return await new NotificationBL(CallContext).GetPushNotifications(status, currentPage);
        //}

        //public async Task<int> GetUnreadPushNotificationCount()
        //{
        //    return await new NotificationBL(CallContext).GetUnreadPushNotificationCount();
        //}

        //public async Task<ShareHolderDTO> GetShareHolderInfo(string emiratesID)
        //{
        //    return await new Domain.ShoppingCart.CustomerBL(CallContext, _dbContext, _backgroundJobs).GetShareHolder(emiratesID);
        //}

        //public async Task SaveCustomerCard(CustomerCardDTO card)
        //{
        //    await new Domain.ShoppingCart.CustomerBL(CallContext, _dbContext, _backgroundJobs).SaveCustomerCard(card);
        //}

        //public async Task<OperationResultDTO> SaveJobStatus(long cartId, long headId,
        //    byte jobStatusID, byte operationStatus, long employeeId)
        //{
        //    return await new AppDataBL(this.CallContext, _dbContext, _backgroundJobs).SaveJobStatus(cartId, headId,
        //        jobStatusID, operationStatus, employeeId);
        //}

        //public async Task SetPaymentGatewayWebHook()
        //{
        //    string body = Encoding.UTF8.GetString(OperationContext.Current.RequestContext.RequestMessage.GetBody<byte[]>());
        //    await new AppDataBL(this.CallContext, _dbContext, _backgroundJobs).SetPaymentGatewayWebHook(body);
        //}

        //public async Task LogPaymentLog(PaymentLogDTO dto)
        //{
        //    await new AppDataBL(this.CallContext, _dbContext, _backgroundJobs).LogPaymentLog(dto);
        //}

        //public async Task<string> GetDriverGeoLocationByOrderID(long orderID)
        //{
        //    return await new ShoppingCartBL(CallContext, _dbContext, _backgroundJobs).GetDriverGeoLocationByOrderID(orderID);
        //}

        //public async Task<DriverDetailDTO> GetDriverDetailsByOrderID(long orderID)
        //{
        //    return await new ShoppingCartBL(CallContext, _dbContext, _backgroundJobs).GetDriverDetailsByOrderID(orderID);
        //}

        //public bool AlterItem(AlterItemDTO item)
        //{
        //    return new Domain.Catalogs.ProductDetailBL(this.CallContext).AlterItem(item);
        //}

        //public async Task<List<BranchDTO>> GetUserBranches()
        //{
        //    return await new Domain.ShoppingCart.UserServiceBL(this.CallContext, _dbContext, _backgroundJobs).GetUserBranches();
        //}

        //public async Task<ProductInventoryBranchDTO> GetInventoryDetailsSKUID(long branchID, long SKUID)
        //{
        //    return await new Domain.ShoppingCart.ProductDetailBL(CallContext, _dbContext)
        //        .GetInventoryDetailsSKUID(branchID: branchID, skuID: SKUID);
        //}

        //public ProductInventoryBranchDTO UpdateInventoryPriceDetailsBySKUID(ProductInventoryBranchDTO inventory)
        //{
        //    return new InventoryBL(CallContext).UpdateInventoryPriceDetailsBySKUID(inventory);
        //}

        //public async Task<OperationResultDTO> SendNotes(CommentDTO comment)
        //{
        //    var operationResult = new OperationResultDTO();

        //    try
        //    {
        //        await new Domain.Commons.CommonBL(CallContext).SaveComment(comment);
        //        operationResult.operationResult = Framework.Contracts.Common.Enums.OperationResult.Success;
        //        operationResult.Message = "Comment submitted successfully.";
        //        await new AppDataBL(CallContext, _dbContext, _backgroundJobs).SendPushNotificationForComments(comment);
        //        return operationResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        Eduegate.Logger.LogHelper<ECommerceService>.Fatal(ex.Message, ex);
        //        operationResult.Message = "Error occured while submitting your comments.";
        //        operationResult.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
        //        return operationResult;
        //    }
        //}

        //public async Task<bool> GetNewOrders(DateTime lastCheckedDate)
        //{
        //    return await new AppDataBL(this.CallContext, _dbContext, _backgroundJobs).GetNewOrders(lastCheckedDate);
        //}

        //public async Task<string> GetSetting(string settingCode)
        //{
        //    return await new AppDataBL(this.CallContext, _dbContext, _backgroundJobs).GetSetting(settingCode);
        //}

        //public async Task<List<CommentDTO>> GetComments(EntityTypes entityTypeID, long referenceID, long departmentID)
        //{
        //    return await new Domain.Commons.CommonBL(CallContext).GetComments((Eduegate.Framework.Enums.EntityTypes)entityTypeID, referenceID, departmentID);
        //}

        //public async Task<CommentDTO> SaveComment(CommentDTO comment)
        //{
        //    comment = await new Domain.Commons.CommonBL(CallContext).SaveComment(comment);
        //    await new AppDataBL(CallContext, _dbContext, _backgroundJobs).SendPushNotificationForComments(comment);
        //    return comment;
        //}

        //public async Task<List<OrderActivityDTO>> GetCartActivities(long cartID, string type)
        //{
        //    return await new ShoppingCartBL(CallContext, _dbContext, _backgroundJobs).GetCartActivities(cartID, type);
        //}

        //public async Task<OrderActivityDTO> SaveCartActivity(OrderActivityDTO activity)
        //{
        //    return await new ShoppingCartBL(CallContext, _dbContext, _backgroundJobs).SaveCartActivity(activity);
        //}

        //public async Task<OperationResultDTO> UpdateCartActivityStatus(long cartActivityID, byte statusID)
        //{
        //    return await new ShoppingCartBL(CallContext, _dbContext, _backgroundJobs).UpdateCartActivityStatus(cartActivityID, statusID);
        //}

        //public async Task<OperationResultDTO> UpdateCartActivityAction(CartActivityActionDTO action)
        //{
        //    return await new ShoppingCartBL(CallContext, _dbContext, _backgroundJobs).UpdateCartActivityAction(action);
        //}

        //public string DownloadInvoice(long headID)
        //{
        //    return new ShoppingCartBL(CallContext, _dbContext, _backgroundJobs).DownloadInvoice(headID);
        //}

        //public async Task<OperationResultDTO> EmailInvoice(long headID)
        //{
        //    return await new ShoppingCartBL(CallContext, _dbContext, _backgroundJobs).EmailInvoice(headID);
        //}

        //public async Task<OperationResultDTO> GenerateSalesOrder(long headID)
        //{
        //    return await new AppDataBL(CallContext, _dbContext, _backgroundJobs).GenerateSalesOrder(headID);
        //}

        //public async Task<NotificaitonSummaryDTO> GetNotificationSummary(DateTime lastUpdated)
        //{
        //    return await new AppDataBL(CallContext, _dbContext, _backgroundJobs).GetNotificationSummary(lastUpdated);
        //}

        //public async Task<List<CashChangeDTO>> GetChangesFor()
        //{
        //    return await new ShoppingCartBL(CallContext, _dbContext, _backgroundJobs).GetChangesFor();
        //}

        //public async Task<OperationResultDTO> UpdateCashChangesFor(long cartID, int changeID)
        //{
        //    return await new ShoppingCartBL(CallContext, _dbContext, _backgroundJobs).UpdateCashChangesFor(cartID, changeID);
        //}

        //public OperationResultDTO GenerateSalesOrderByTransactionNo(long transactionNo)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<DeliveryBlockingPeriodDTO> GetDeliveryBlockingPeriods()
        //{
        //    return await new ShoppingCartBL(CallContext, _dbContext, _backgroundJobs).GetDeliveryBlockingPeriods();
        //}

        //public async Task<List<string>> GetMobileNumberPrefixes()
        //{
        //    return await new ShoppingCartBL(CallContext, _dbContext, _backgroundJobs).GetMobileNumberPrefixes();
        //}

        //public List<MissingInfoDTO> GetMissingNotification()
        //{
        //    return null;
        //}

        //public async Task<OperationResultDTO> CancelOrder(long headID)
        //{
        //    return await new ShoppingCartBL(CallContext, _dbContext, _backgroundJobs).CancelOrder(headID);
        //}

        //public async Task<List<MessageTemplateDTO>> GetMessageTemplates(int templateTypeId)
        //{
        //    return await new Domain.ShoppingCart.ReferenceDataBL(CallContext, _dbContext).GetMessageTemplates(templateTypeId);
        //}
        public ShareHolderDTO GetShareHolderInfo(string emiratesID)
        {
            throw new NotImplementedException();
        }

        public void SaveCustomerCard(CustomerCardDTO card)
        {
            throw new NotImplementedException();
        }
    }
}