using AutoMapper;
using FinBY.Domain.Data.DTO;
using FinBY.Domain.Entities;

namespace FinBY.Domain.Data.Converter.Profiles
{
    public  class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();

        //     .ForMember(dest =>
        //    dest.FName,
        //    opt => opt.MapFrom(src => src.FirstName))
        //.ForMember(dest =>
        //    dest.LName,
        //    opt => opt.MapFrom(src => src.LastName))
        }
    }
}
