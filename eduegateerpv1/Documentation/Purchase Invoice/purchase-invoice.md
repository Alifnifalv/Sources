---
author: Softop Solutions Pvt Ltd.
topic: Inventory - Purchase Invoice
date: 14/12/2022
---

<h1>Purchase Invoice -  User Manual<h1>

---
## Purchase invoices is used to update inventory levels. a document that a person or company receives when they buy something, giving details of price, payment conditions.    
---
### Menu: Purchase >> Purchase Invoice >> Create Purchase Invoice 

[![Image Link](/Images/pic01.png "pic1, Main Screen")](https://eduegate.visualstudio.com/eduegateerpv1/_versionControl?path=%24/eduegateerpv1/Documentation/Purchase%20Invoice/Images/pi01.png&version=T) 

--- 

## New Features Added :  
* 	Additional Expense 
* 	Multiple Currency for Purchase Invoice
* 	Landed cost and Last Cost 
* 	Unit and Fraction 

---

## Understand the screen 
[![Image Link](/Images/pic01.png "pic2, pi Screen")](https://eduegate.visualstudio.com/eduegateerpv1/_versionControl?path=%24/eduegateerpv1/Documentation/Purchase%20Invoice/Images/pi02.png&version=T) 
*	Branch Name : select any branch Name
* 	 Document : purchase Invoice
* 	Validity : Choose the validity Date 
* 	Remarks : user remarks
* 	Currency and Exchange rate : system provides multiple currency . user can give the exchange rate it will reflect in all value and cost calculation.

* Supplier : Select  the supplier from the drop down list.
*	Delivery Type : There are many type of delivery available ,  select one from the list . [pic-3]
[![Image Link](/Images/pic03.png "pic3, Delivery Type")](https://eduegate.visualstudio.com/eduegateerpv1/_versionControl?path=%24/eduegateerpv1/Documentation/Purchase%20Invoice/Images/pi03.png&version=T) 

*	Reference# : supplier reference number/date  
*	Document Status : If the user want to save the document temporarily ,select ‘Draft’ and proceed . this document can be edited later then ‘submit’ the document . [pic-4]
[![Image Link](/Images/pic04.png "pic4, Doc.Status")](https://eduegate.visualstudio.com/eduegateerpv1/_versionControl?path=%24/eduegateerpv1/Documentation/Purchase%20Invoice/Images/pi04.png&version=T) 

*	Product : Select the products/items to make purchase 
*	Available quantity : System shows available quantity 
*	Quantity : User can enter the quantity 
*	Unit and Fraction : System brings the default unit – purchase unit  and the fraction ***[ Product screen ]*** 
Unit can be edited by the user , Fraction will be changed according to the unit
*	Foreign Rate : unit price of the item in the purchase currency .  system fills rate by default from the history when enter the quantity  
and user can edit the rate .
*	Local Rate : System calculates and display the local rate  [ hint : Local Rate = Foreign Rate x Exchange Rate ]
*	Foreign Amount : the total price of the item/Product in Foreign currency [ Hint : Quantity x Foreign Rate ] 	
*	Local Amount : System converts the Foreign amount to the local currency [ Hint : Local Rate x Quantity ]
*	Landing Cost : This is associated with additional expense.  Calculation : ( Total Additional Expense in Local Currency / Total Amount in Local Currency ) x ( Amount of the particular item in local currency )

*	Last Cost Price :System Calculated the Last Cost and display .  [Hint :  Local Rate  + ( Landing Cost of the Product / Quantity) ]
*	There is a provision to set the discount in percentage or in Amount
*	Sub Total [ without Discount ] and Grand Total [ after discount deduction ] 

[![Image Link](/Images/pic05.png "pic5, Product Line")](https://eduegate.visualstudio.com/eduegateerpv1/_versionControl?path=%24/eduegateerpv1/Documentation/Purchase%20Invoice/Images/pi05.png&version=T) 

   ###  Additional Expense  
* Additional Expense : There are three type of expenses are available . Clearing Charges , Freight duties , Export duties . [pic-6]
     [![Image Link](/Images/pic06.png "pic6, Additional Expense")](https://eduegate.visualstudio.com/eduegateerpv1/_versionControl?path=%24/eduegateerpv1/Documentation/Purchase%20Invoice/Images/pi06.png&version=T) 
*	Provisional Account : Supp Clearing Charges ,Supp Freight duties , Supp Export duties [pic-7]
     [![Image Link](/Images/pic07.png "pic7, Provisional Account")](https://eduegate.visualstudio.com/eduegateerpv1/_versionControl?path=%24/eduegateerpv1/Documentation/Purchase%20Invoice/Images/pi07.png&version=T) 
* 	Currency : Multiple currency allows 
* 	Exchange Rate : user can enter the currency Exchange rate 
* 	Foreign Amount : User has to enter the amount 
*	Local Amount : System converts the amount to local currency .  
     [![Image Link](/Images/pic08.png "pic8, Add.Expense Line")](https://eduegate.visualstudio.com/eduegateerpv1/_versionControl?path=%24/eduegateerpv1/Documentation/Purchase%20Invoice/Images/pi08.png&version=T) 
      
 ###  	Payment : 
   *	Type : Payment Type : Different Payment types are available . 
        NB :- At present only credit note is allowed
   *	Reference No. : Anything by the user for reference 
   *	Amount : Amount will be filled by the system or user can enter .

### Save 
•	If the Document status ‘Draft’ , document will be saved for future amendment . it will not update the stock and accounts
•	If the Status : Submit/Submitted , document will be posted , can not be edited, updating stock  and accounts  

---
#### ***Purchase invoice makes changes on the stock and accounts***
---
