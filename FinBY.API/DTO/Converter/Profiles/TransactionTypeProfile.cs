using AutoMapper;
using FinBY.API.Data.DTO;
using FinBY.Domain.Entities;
using System.Drawing;

namespace FinBY.API.Data.Converter.Profiles
{
    /// <summary>
    /// Profile to Map the TransactionTypeProfile to other objects, just by extending the Profile class from the AutoMapper
    /// it will be mapped.
    /// </summary>
    public class TransactionTypeProfile : Profile
    {
        // $"#{src.ArgbColor.R:X2}{src.ArgbColor.G:X2}{src.ArgbColor.B:X2}"
        public TransactionTypeProfile()
        {
            CreateMap<TransactionType, TransactionTypeDTO>()
            .ForPath(dest => dest.ArgbColor, opt => opt.MapFrom(src => $"#{src.GetArgbColor().R:X2}{src.GetArgbColor().G:X2}{src.GetArgbColor().B:X2}"));

            CreateMap<TransactionType, NewTransactionTypeDTO>()
            .ForPath(dest => dest.ArgbColor, opt => opt.MapFrom(src => $"#{src.GetArgbColor().R:X2}{src.GetArgbColor().G:X2}{src.GetArgbColor().B:X2}"));


            CreateMap<NewTransactionTypeDTO, TransactionType>()
            .ConstructUsing(x => new TransactionType(x.Name, ColorTranslator.FromHtml(x.ArgbColor).ToArgb()))
            .ForPath(dest => dest.ArgbColor, opt => opt.MapFrom(src => ColorTranslator.FromHtml(src.ArgbColor).ToArgb()));

            CreateMap<TransactionTypeDTO, TransactionType>()
            .ConstructUsing(x => new TransactionType(x.Name, ColorTranslator.FromHtml(x.ArgbColor).ToArgb()))
            .ForPath(dest => dest.ArgbColor, opt => opt.MapFrom(src => ColorTranslator.FromHtml(src.ArgbColor).ToArgb()));
        }
    }
}
