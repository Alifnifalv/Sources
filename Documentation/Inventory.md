---
author: Softop Solutions Pvt Ltd.
topic: Inventory
date: 14/12/2022
---

# Stock and Inventory – Module
--- 

[[_TOC_]]



## Master Screens


> - **Unit Group :**
        Group of units: Unit Group is a set of Units . One group unit may have one or more units.<br>
        Table :catalog.UnitGroups. UnitGroupID, UnitGroupName .    


> - **Unit :**
        An inventory Unit or unit of measure (UOM)  is the standardized measurement unit used to count stock items <br>
        and to express them as specific quantities. Pieces, litres, kilograms, meters, etc.<br>
        Table : catalog.Units > UnitGroupID,Unit, UnitName and Fraction .<br>

        Fraction/Conversion Factor : A conversion factor represents the numeric value or ratio that is used to relate one unit of measure to another. 
        The conversion factor is an alternate value that is used to represent a unit of measure. 
        It can be used to define an item order or transfer quantity and storeroom balance.

        1 box : 12 Pcs  >>   Where 12 – fraction
        If 10 Boxes  >>  10 x 12 = 120 pcs
        If 20.00 for 10 boxes  >> 120 Pcs = 20.00
          NB : -  1 Pc = 20.00/120   [ if 2 is the unit price, 2/12   => unit price / Fraction ]

        

> - **Branch :**
        Branches are different locations where stock is kept [ Schools Stores ] <br>
        Table : mutual.Branches.

> - **Product Family :**
        Product Family or Product Group <br>
        Table : catalog.ProductFamilies > ProductFamilyIID,ProductFamilyName 

> - **Category :**
        category of products <br>
        Table : catalog.Categories

> - **Product :**
        Product or Item Master : all basic information about an item. Default Unit Group and Unit can be set in the Product Master [For Purchase and sale ] . <br>
        unit and its Fraction will be taken during all transactions. Default Unit may change at the time of purchase/GRN. <br>
        Table :catalog.Products > Product code,ProductDescription 

> - **Suppliers :**
        Information of the suppliers - Supplier Master <br>
        Table : mutual.Suppliers

> - **Customer :**
        Information of the customers - Customer Master <br>
        Table : mutual.customers

## Transaction Screens

### [ Purchase ]   
  
> - **Purchase Order :**  [ >>> Purchase Order User Manual <<< ](../Documentation/Purchase%20Order/purchaseOrder.md) <br>
        A purchase order is an document that we issue to suppliers/vendors, indicating relevant information about what we want to buy, the quantity,  <br>
        the price agreed for that particular product .<br>
        Features : Multiple currency , unit and fraction  <br>
        Tables : inventory.TransactionHead , inventory.TransactionDetails

> - **Service entry :** 
        Service entry is just like purchase order ,it is for service oriented purchase. Example : Airticket <br>
        GRN can be made against service . <br>
        Tables : inventory.TransactionHead , inventory.TransactionDetails

> - **Good Receipt Note :** [ >>> Goods Received Note User Manual <<< ](../Documentation/Goods%20Received%20Note/GoodsReceiptNote.md) <br>
        Goods received note (GRN) is a document that acknowledges the delivery of goods to a customer by a supplier. <br>
        A GRN consists of a record of goods that the buyer has received. This record helps the customer compare the goods delivered against the goods ordered. <br> 
        Features : Multiple currency , unit and fraction  . <br>
        GRN will be considered as received . Stock will be updated. GRN can be made from whether against Purchase order/Service entry or direct. <br>
        NB:- existing system will not allow to make GRN from more than one purchase Order.<br>
        Tables : inventory.TransactionHead , inventory.TransactionDetails
        inventory.InvetoryTransactions , inventory.ProductInventories

> - **Purchase Invoice :** [ >>> Purchase Invoice User Manual <<< ](../Documentation/Purchase%20Invoice/purchase-invoice.md) <br>
        Purchase invoices is used to update inventory levels. a document that a person or company receives when they buy something, giving details of price, payment conditions.<br>        
        Features :  
        •	Editable Multiple Currency and exchange rate. <br>
        •	Editable Unit <br>
        •	Payment Mode :  Cash and card are available for payment .  <br>
        •	Additional Expanses : Define charges such as freight, handling and administrative fees. These costs can be added to a purchase. <br>
            This will be reflected in the rates billed to a customer. Several additional cost items can be assigned . <br>
            Eg :- Clearing Charges,Freight duties,Export Dutiies .  Currency : Allows Multiple Currency .         <br>
            The Extra expanse will reflect on Landed Cost and Last Cost Price  at the same time.<br>
            Discount amount or percentage *[ it will be in foreign currency and will be converted to local currency ]* is distributed to each item.      <br> 
        •   System provides three method to make purchase invoice. <br>
            ***First method*** is against GRN, stock will not be updated in this method as already done at GRN but accounts will be updated.  <br>
            ***The second method*** is against purchase order , stock and account will be updated. <br>
            In ***Third method*** is direct Purchase , where both stock and account will be updated. <br>

            LandingCost=(TotalAdditionalExpense/totalAmount)*Amount
            Last Cost Price =Rate +(LandingCost /Qty)

            Note : All cost calculation will be based on local currency 
            Tables :inventory.TransactionHead , 
                    inventory.TransactionDetails
                    inventory.InvetoryTransactions ,        
                    inventory.ProductInventories                      
                    Account Tables : [account].[AdditionalExpenses]
                        [account].[AdditionalExpenseProvisionalAccountMaps]
                        [account].[AdditionalExpensesTransactionsMaps]
                        account.Trantail_Narration 
                        account.Trantail_Payment 
                        account.Trantail_SubLedger 
                        account.Trantail_CostCenter 
                        account.Trantail,account.Tranhead  etc.   


> - **Purchase Return :**
        A purchase return occurs when a buyer returns goods purchased from a supplier.  <br>
        Transaction is possible whether against a purchase invoice or not.    <br>
        Possible Return method : Refund or Exchange.<br>
        Multiple currency , unit and fraction.<br>
        Table : inventory.TransactionHead , inventory.TransactionDetails,<br>
        inventory.InvetoryTransactions , inventory.ProductInventories and accounts tables.

### [ Sales ]

> - **Sales Order :**
        A sales order is a document generated by the seller specifying the details about the product or services ordered by the customer.  <br>
        Features : Different Delivery Options <br>
        Tables : inventory.TransactionHead , inventory.TransactionDetails <br>

> - **Sales Invoice :**
        A Sales Invoice is a bill that send to the Customers against their purchase. Sales invoice is generated against Sales Order. <br>                               
        Features : Editable Unit. <br>
        Payment Options : Card , Cash , credit Note(*sales return) <br>
        Tables: inventory.TransactionHead , inventory.TransactionDetails <br>
               inventory.InvetoryTransactions , inventory.ProductInventories   <br>
               and accounts tables

> - **Sales Return :**
        A sales return is Goods sent back by a buyer to the seller. <br>
        Key Features : Payment Option : Cards , cash and Credit Note* <br>
        Credit Note a receipt given by a seller to a customer who has returned goods, which can be offset against future purchases. <br>
        (credit note details can be seen in sales invoice).
        Tables: inventory.TransactionHead , inventory.TransactionDetails <br>
            inventory.InvetoryTransactions , inventory.ProductInventories  <br>
            and accounts tables 
	
### [Other Transactions – Wrap/Unwrap]
System allows to make bundles of different items. This bundle also consider as a new item. <br>
   
> - **Bundle Wrap :**
        Wrapping is a transaction/process to make bundles of many items : <br>
        Note : Main Product - bundle will be treated as received to stock and the sub items will be Stock out.<br>
        Tables : inventory.TransactionHead , inventory.TransactionDetails 
        inventory.InvetoryTransactions , inventory.ProductInventories

> - **Bundle Unwrap :**  
        Transaction for Unwrap the bundle. It will revert the bundle to the primary items. Bundle Product will be deducted from the stock and others will be added to the stock.
        <br> 
        Tables : inventory.TransactionHead , inventory.TransactionDetails
        inventory.InvetoryTransactions , inventory.ProductInventories

### [Other Transactions – Branch Transfer]
System Provides the facility to transfer items from one store/Branch to another. <br>

> - **Branch Transfer Request :** 
        A request can be made to transfer the item. 

> - **Branch Transfer :**	
        Transfer one or more items from one branch to another. <br>
        Stock will be deducted from one branch and will be added to other. <br> 
        Tables : inventory.TransactionHead , inventory.TransactionDetails
        inventory.InvetoryTransactions , inventory.ProductInventories.
    
         
## List/ Prints/Reports

> - Inventory Details :  Available Stock details – item/Product wise 
> - Purchase voucher
> - Sales Invoice
> - Inventory Summary report : Stock Summary . Available stock , unit , Cost price.
> - Product Sale Report : Product wise Sold Qty ,Price , Current Stock.

#### Progressing – New

> - Stock Movement Reports - Transaction : All Transactions with in a period of time. [Price , Qty and Amount]
    
> - Stock Movement Reports Detailed : Opening Balance, All Transactions, Closing and Current Stock with in a period of time.  [Price , Qty and Amount]
Parameters : From Date and To Date
    
> - Stock Card : Opening , closing and all transaction with in a period of a particular item/product . 
Parameters : From Date and To Date ,  product
    
> - Other purchase and sale reports
