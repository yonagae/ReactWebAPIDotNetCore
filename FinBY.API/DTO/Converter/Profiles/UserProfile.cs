using AutoMapper;
using FinBY.API.Data.DTO;
using FinBY.Domain.Entities;

namespace FinBY.API.Data.Converter.Profiles
{
    public  class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            //     .ForMember(dest =>
            //    dest.FName,
            //    opt => opt.MapFrom(src => src.FirstName))
            //.ForMember(dest =>
            //    dest.LName,
            //    opt => opt.MapFrom(src => src.LastName))
        }
    }
}
