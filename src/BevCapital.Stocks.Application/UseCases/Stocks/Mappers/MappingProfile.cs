using AutoMapper;
using BevCapital.Stocks.Application.UseCases.Stocks.Response;
using BevCapital.Stocks.Domain.Entities;

namespace BevCapital.Stocks.Application.UseCases.Stocks.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Stock, StockOut>();
            CreateMap<StockPrice, StockPriceOut>();
        }
    }
}
