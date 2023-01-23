
using System;
using System.Linq;
using API.Dtos;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
           CreateMap<AppUser,MemberDto >()
           .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src =>
            src.Photos.FirstOrDefault(p => p.IsMain).Url))
             .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));;
           CreateMap<Photo,PhotoDto>();
           CreateMap<Category,CategoryWithDetailsDto>()
             .ForMember(dest => dest.HaKaDocClient, opt => opt.MapFrom(src => src.HaKaDocClient.Name))
             .ForMember(dest => dest.TotalProducts, opt => opt.MapFrom(src => src.Products.Count()))
           ;
           CreateMap<Category,CategoryForListDto>();
           CreateMap<Product,ProductForListDto>();
           CreateMap<Product,ProductWithDetailDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));
           CreateMap<MemberUpdateDto,AppUser>();
           CreateMap<RegisterDto,AppUser>();
           CreateMap<CustomerCreationDto,Customer>();
           CreateMap<Message, MessageDto>()
           .ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(src => src.Sender.Photos.FirstOrDefault(p => p.IsMain).Url))
           .ForMember(dest => dest.RecipientPhotoUrl, opt => opt.MapFrom(src => src.Recipient.Photos.FirstOrDefault(p => p.IsMain).Url));
           CreateMap<Customer,CustomerForListDto>()
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.Name))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country.Name))
            .ForMember(dest => dest.District, opt => opt.MapFrom(src => src.District.Name))
            .ForMember(dest => dest.BirthCountry, opt => opt.MapFrom(src => src.BirthCountry.Name))
            .ForMember(dest => dest.MaritalSatus, opt => opt.MapFrom(src => src.MaritalSatus.Name))
            .ForMember(dest => dest.BirthDistrict, opt => opt.MapFrom(src => src.BirthDistrict.Name))
            .ForMember(dest => dest.BirthCity, opt => opt.MapFrom(src => src.BirthCity.Name))
            .ForMember(dest => dest.HaKaDocClient, opt => opt.MapFrom(src => src.HaKaDocClient.Name));

            CreateMap<SubProductAddingDto,SubProduct>();
            CreateMap<InventOp,InventopForListDto>()
            .ForMember( dest => dest.InventOpType, opt =>opt.MapFrom(src => src.InventOpType.Name));
             CreateMap<SubProduct,SubProductListDto>()
           .ForMember( dest => dest.Category, opt =>opt.MapFrom(src => src.Product.Category.Name))
           .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src =>src.Photos.FirstOrDefault(p => p.IsMain).Url))
           .ForMember(dest => dest.Product, opt => opt.MapFrom(src =>src.Product.Name));
             CreateMap<Store,StoreListDto>()
           .ForMember(dest => dest.DistrictName, opt => opt.MapFrom(src => src.District.Name));
     CreateMap<SubProductSN,SubProductSnListDto>();

           CreateMap<DateTime,DateTime>().ConvertUsing(d => DateTime.SpecifyKind(d, DateTimeKind.Utc));
        }
    }
}