﻿using AutoMapper;
using FinBY.API.Data.DTO;
using FinBY.Domain.Entities;

namespace FinBY.API.Data.Converter.Profiles
{
    /// <summary>
    /// Profile to Map the TransactionTypeProfile to other objects, just by extending the Profile class from the AutoMapper
    /// it will be mapped.
    /// </summary>
    public class TransactionTypeProfile : Profile
    {
        public TransactionTypeProfile()
        {
            CreateMap<TransactionType, TransactionTypeDTO>();
            CreateMap<TransactionType, NewTransactionTypeDTO>();
            CreateMap<NewTransactionTypeDTO, TransactionType>();
            CreateMap<TransactionTypeDTO, TransactionType>();
        }
    }
}