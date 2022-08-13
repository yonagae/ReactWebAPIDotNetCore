using AutoMapper;
using FinBY.Domain.Data.DTO;
using FinBY.Domain.Entities;

namespace FinBY.Domain.Data.Converter.Profiles
{
    /// <summary>
    /// Profile to Map the TransactionAmount to other objects 
    /// </summary>
    public class TransactionAmountProfile : Profile
    {
        public TransactionAmountProfile()
        {
            CreateMap<TransactionAmount, TransactionAmountDTO>();
            CreateMap<TransactionAmount, NewTransactionAmountDTO>();
        }
    }
}
