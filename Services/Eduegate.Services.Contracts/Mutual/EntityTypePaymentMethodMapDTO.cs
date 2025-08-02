using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public partial class EntityTypePaymentMethodMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long EntityTypePaymentMethodMapID { get; set; }
        [DataMember]
        public Nullable<int> EntityTypeID { get; set; }
        [DataMember]
        public Nullable<short> PaymentMethodID { get; set; }
        [DataMember]
        public Nullable<long> ReferenceID { get; set; }
        [DataMember]
        public Nullable<long> EntityPropertyID { get; set; }
        [DataMember]
        public Nullable<int> EntityPropertyTypeID { get; set; }
        [DataMember]
        public string AccountName { get; set; }
        [DataMember]
        public string AccountID { get; set; }
        [DataMember]
        public string BankName { get; set; }
        [DataMember]
        public string BankBranch { get; set; }
        [DataMember]
        public string IBANCode { get; set; }
        [DataMember]
        public string SWIFTCode { get; set; }
        [DataMember]
        public string IFSCCode { get; set; }
        [DataMember]
        public string NameOnCheque { get; set; }
        //public Nullable<int> CreatedBy { get; set; }
        //public Nullable<int> UpdatedBy { get; set; }
        //public Nullable<System.DateTime> CreatedDate { get; set; }
        //public Nullable<System.DateTime> UpdatedDate { get; set; }
        ////public byte[] TimeStamps { get; set; }
    }

    public class EntityTypePaymentMethodMapMapper
    {
        public EntityTypePaymentMethodMapDTO ToDto(EntityTypePaymentMethodMap entity)
        {
            if (entity != null)
            {
                return new EntityTypePaymentMethodMapDTO()
                {
                    EntityTypePaymentMethodMapID = entity.EntityTypePaymentMethodMapIID,
                    EntityTypeID = entity.EntityTypeID,
                    PaymentMethodID = entity.PaymentMethodID,
                    ReferenceID = entity.ReferenceID,
                    EntityPropertyID = entity.EntityPropertyID,
                    EntityPropertyTypeID = entity.EntityPropertyTypeID,
                    AccountID = entity.AccountID,
                    AccountName = entity.AccountName,
                    BankName = entity.BankName,
                    BankBranch = entity.BankBranch,
                    IBANCode = entity.IBANCode,
                    SWIFTCode = entity.SWIFTCode,
                    IFSCCode = entity.IFSCCode,
                    NameOnCheque = entity.NameOnCheque,
                };
            }
            else return new EntityTypePaymentMethodMapDTO();
        }

        public EntityTypePaymentMethodMap TOEntity(EntityTypePaymentMethodMapDTO dto)
        {
            if (dto != null)
            {
                return new EntityTypePaymentMethodMap()
                {
                    EntityTypePaymentMethodMapIID = dto.EntityTypePaymentMethodMapID,
                    EntityTypeID = dto.EntityTypeID,
                    PaymentMethodID = dto.PaymentMethodID,
                    ReferenceID = dto.ReferenceID,
                    EntityPropertyID = dto.EntityPropertyID,
                    EntityPropertyTypeID = dto.EntityPropertyTypeID,
                    AccountID = dto.AccountID,
                    AccountName = dto.AccountName,
                    BankName = dto.BankName,
                    BankBranch = dto.BankBranch,
                    IBANCode = dto.IBANCode,
                    SWIFTCode = dto.SWIFTCode,
                    IFSCCode = dto.IFSCCode,
                    NameOnCheque = dto.NameOnCheque,
                    UpdatedDate = DateTime.Now,
                };
            }
            else return new EntityTypePaymentMethodMap();
        }
    }
}
