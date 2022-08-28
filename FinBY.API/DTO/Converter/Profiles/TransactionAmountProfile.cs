using AutoMapper;
using FinBY.API.Data.DTO;
using FinBY.Domain.Entities;

namespace FinBY.API.Data.Converter.Profiles
{
    /// <summary>
    /// Profile to Map the TransactionAmount to other objects 
    /// </summary>
    public class TransactionAmountProfile : Profile
    {
        public TransactionAmountProfile()
        {
            CreateMap<TransactionAmount, TransactionAmountDTO>();
            //CreateMap<TransactionAmount, NewTransactionAmountDTO>();
            CreateMap<TransactionAmountDTO, TransactionAmount>();
            CreateMap<NewTransactionAmountDTO, TransactionAmount>();
        }
    }
}
