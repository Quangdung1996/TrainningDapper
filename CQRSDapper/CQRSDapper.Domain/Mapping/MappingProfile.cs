using AutoMapper;
using CQRSDapper.Domain.Models;
using CQRSDapper.Domain.Models.Dto;

namespace CQRSDapper.Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerMetaModel>().ReverseMap();
        }
    }
}