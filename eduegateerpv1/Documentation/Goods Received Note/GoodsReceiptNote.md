---
author: Softop Solutions PVT Ltd.
topic : Goods Received Note
date  : 17/12/2022
---

# **Good Received Note -  User Manual** 
---
## Goods received note (GRN) is a document that acknowledges the delivery of goods to a customer by a supplier. A GRN consists of a record of goods that the buyer has received. This record helps the customer compare the goods delivered against the goods ordered.    
---
### Menu: Purchase >> Goods Received Note >> Create Goods Received Note

[![Image Link](/Images/pic01.png "pic1, Main Screen")](https://eduegate.visualstudio.com/eduegateerpv1/_versionControl?path=%24/eduegateerpv1/Documentation/Goods%20Received%20Note/Images/grn01.png&version=T)

--- 

## Features Added :  
>- Multiple Currency 
>- Unit and Fraction 

## Understand the screen 
> - Branch Name : select any branch Name
> - Document : purchase Invoice
> - Validity : Choose the validity Date 
> -	Remarks : user remarks
> -	Currency and Exchange rate : system provides multiple currency . user can give the exchange rate it will reflect in all value and cost calculation.

> - Supplier : Select  the supplier from the drop down list.
> - Delivery Type : There are many type of delivery available ,  select one from the list . [pic-2]
[![Image Link](/Images/pic01.png "pic2, Delivery Type")](https://eduegate.visualstudio.com/eduegateerpv1/_versionControl?path=%24/eduegateerpv1/Documentation/Goods%20Received%20Note/Images/grn02.png&version=T)

> -	Reference# : supplier reference number/date  
> -	Document Status : If the user want to save the document temporarily ,select ‘Draft’ and proceed . this document can be edited later then ‘submit’ the document . [pic-3]
[![Image Link](/Images/pic01.png "pic3, Document Status")](https://eduegate.visualstudio.com/eduegateerpv1/_versionControl?path=%24/eduegateerpv1/Documentation/Goods%20Received%20Note/Images/grn03.png&version=T)
> -	Product : Select the products/items to make purchase 
> - Warranty Start and Warranty End : Warranty period 
> -	Available quantity : System shows available quantity 
> - Quantity : User can enter the quantity 
> - Unit and Fraction : System brings the default unit – purchase unit  and the fraction [ Product screen ] 
Unit can be edited by the user , Fraction will be changed according to the unit
> - Foreign Rate : unit price of the item in the purchase currency .  system fills rate by default from the history when enter the quantity  
and user can edit the rate .
> - Local Rate : System calculates and display the local rate  [ hint : Local Rate = Foreign Rate x Exchange Rate ]
> - Foreign Amount : the total price of the item/Product in Foreign currency [ Hint : Quantity x Foreign Rate ] 	
> - Local Amount : System converts the Foreign amount to the local currency [ Hint : Local Rate x Quantity ]
> - Cost Center : General/Finance 
> - Discount : There is a provision to set the discount in percentage or in Amount
> - Sub Total [ without Discount ] and Grand Total [ after discount deduction ] 

### Save 
> - If the Document status ‘Draft’ , document will be saved for future amendment . it will not update the stock and accounts
> - If the Status : Submit/Submitted , document will be posted , can not be edited, updating stock  and accounts  

---
#### ***Goods Received Note updates the stock but does not make any changes on accounts***
---