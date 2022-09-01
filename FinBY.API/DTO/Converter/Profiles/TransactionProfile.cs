using AutoMapper;
using FinBY.API.Data.DTO;
using FinBY.Domain.Entities;

namespace FinBY.API.Data.Converter.Profiles
{
    /// <summary>
    /// Profile to Map the TransactionProfile to other objects 
    /// </summary>
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionDTO>();
            CreateMap<Transaction, NewTransactionDTO>();
            CreateMap<TransactionDTO, Transaction>();
            CreateMap<NewTransactionDTO, Transaction>();            
        }
    }
}
