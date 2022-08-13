using AutoMapper;
using FinBY.Domain.Data.DTO;
using FinBY.Domain.Entities;

namespace FinBY.Domain.Data.Converter.Profiles
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
        }
    }
}
