using AutoMapper;
using PSG.DeliveryService.Application.ViewModels.AccountViewModels;
using PSG.DeliveryService.Application.ViewModels.OrderViewModels;
using PSG.DeliveryService.Domain.Entities;

namespace PSG.DeliveryService.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //TODO: Кондишны для маппинга DeliveryPrice и ProductPrice (формулы/константы)
        CreateMap<CreateOrderViewModel, Order>()
            .ForMember(x => x.OrderWeight,
                opt =>
                {
                    opt.PreCondition(x => int.TryParse(x.OrderWeight, out _));
                    opt.MapFrom(x => int.Parse(x.OrderWeight!));
                })
            .ForMember(x => x.OrderType,
                opt =>
                {
                    opt.PreCondition(x => int.TryParse(x.OrderType, out _));
                    opt.MapFrom(x => int.Parse(x.OrderType!));
                })
            .ForMember(x => x.DeliveryType,
                opt =>
                {
                    opt.PreCondition(x => int.TryParse(x.DeliveryType, out _));
                    opt.MapFrom(x => int.Parse(x.DeliveryType!));
                })
            .ForMember(x => x.OrderTime, opt =>
            {
                opt.PreCondition(x => !string.IsNullOrEmpty(x.OrderTime));
                opt.MapFrom(x => DateTime.Parse(x.OrderTime!));
            })
            .ForMember(x => x.DeliveryAddress,
                opt => opt.MapFrom(x => x.To))
            .ForMember(x => x.ProductAddress,
                opt => opt.MapFrom(x => x.From))
            .ForMember(x => x.ProductName,
                opt => opt.MapFrom(x => x.ProductName))
            .ForAllOtherMembers(opt => opt.Ignore());

        CreateMap<SignUpViewModel, ApplicationUser>()
            .ForMember(x => x.PhoneNumber,
                opt => opt.MapFrom(x => x.PhoneNumber))
            .ForMember(x => x.UserName,
                opt => opt.MapFrom(x => new string(x.PhoneNumber!.Where(char.IsDigit).ToArray())))
            .ForMember(x => x.UserRegistrationTime,
                opt => opt.MapFrom(x => DateTime.Now))
            .ForMember(x => x.PhoneNumber,
                opt => opt.MapFrom(x => x.PhoneNumber))
            .ForMember(x => x.PhoneNumberConfirmed,
                opt => opt.MapFrom(x => true))
            .ForMember(x => x.IsCourier,
                opt => opt.MapFrom(x => x.IsCourier))
            .ForAllOtherMembers(opt => opt.Ignore());
    }
}