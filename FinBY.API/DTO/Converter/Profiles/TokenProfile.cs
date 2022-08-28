using AutoMapper;
using FinBY.API.Data.DTO;
using FinBY.Domain.Entities;

namespace FinBY.API.DTO.Converter.Profiles;

public class TokenProfile : Profile
{
    public TokenProfile()
    {
        CreateMap<TokenDTO, Token>();
        CreateMap<Token, TokenDTO>();
    }
}
