using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public class ShareHolderDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public string Key;
        [DataMember]
        public string No;
        [DataMember]
        public string Title;
        [DataMember]
        public string Title_Arabic;
        [DataMember]
        public string Name;
        [DataMember]
        public string Name_Arabic;
        [DataMember]
        public int Total_Number_Of_Shares;
        [DataMember]
        public decimal Amount_Of_Investment;
        [DataMember]
        public string Membership_Card_No;
        [DataMember]
        public string Membership_Card_Expiry_Date;
        [DataMember]
        public string Family_Name;
        [DataMember]
        public string Passport_No;
        [DataMember]
        public string National_Id_No;
        [DataMember]
        public string Date_Of_Birth;
        [DataMember]
        public int Age;
        [DataMember]
        public string Address;
        [DataMember]
        public string Address_1;
        [DataMember]
        public string City;
        [DataMember]
        public string Phone_No;
        [DataMember]
        public string Country_Region_Code;
        [DataMember]
        public string Post_Code;
        [DataMember]
        public string E_Mail;
        [DataMember]
        public bool Shareholder_Family_Member;
        [DataMember]
        public string Shareholder_Family_Member_No;
        [DataMember]
        public string Relationship;
        [DataMember]
        public decimal Credit_Limit;
        [DataMember]
        public decimal Balance_Credit_Limit_Available;
        [DataMember]
        public decimal Balance_LCY;
        [DataMember]
        public string Date_Filter;
        [DataMember]
        public string Global_Dimension_1_Filter;
        [DataMember]
        public string Global_Dimension_2_Filter;
        [DataMember]
        public decimal Profit_Percent;
        [DataMember]
        public string Power_of_Attorney_Name;
        [DataMember]
        public string Customer_No;
        [DataMember]
        public bool Blocked;
        [DataMember]
        public string No_Series;
        [DataMember]
        public string Gender;
        [DataMember]
        public string Date_Of_Join;
        [DataMember]
        public string PO_Box;
        [DataMember]
        public bool Posted;
        [DataMember]
        public string Mobile_Number;
        [DataMember]
        public decimal Cost_Of_One_Share;
        [DataMember]
        public decimal Reserved_Value;
        [DataMember]
        public string Bank_Payment_Type;
        [DataMember]
        public string County;
        [DataMember]
        public string Bank_Name;
        [DataMember]
        public string Bank_Account_No;
        [DataMember]
        public string IBAN;
        [DataMember]
        public string Index;
    }
}
