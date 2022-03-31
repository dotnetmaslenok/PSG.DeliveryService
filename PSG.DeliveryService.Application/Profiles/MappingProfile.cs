using AutoMapper;
using PSG.DeliveryService.Application.Commands;
using PSG.DeliveryService.Application.Responses;
using PSG.DeliveryService.Domain.Entities;

namespace PSG.DeliveryService.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegistrationCommand, ApplicationUser>()
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

        CreateMap<Order, OrderResponse>()
            .ForMember(x => x.OrderType,
                opt => opt.MapFrom(x => x.OrderType.ToString()));

        CreateMap<CreateOrderCommand, Order>()
            .ForMember(x => x.Id,
                opt => opt.MapFrom(x => x.ClientId))
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
            .ForMember(x => x.OrderTime, opt =>
            {
                opt.PreCondition(x => !string.IsNullOrEmpty(x.OrderTime));
                opt.MapFrom(x => DateTime.Parse(x.OrderTime!));
            })
            .ForMember(x => x.Distance, opt =>
            {
                opt.PreCondition(x => !string.IsNullOrEmpty(x.Distance));
                opt.PreCondition(x => double.TryParse(x.Distance, out double _));
                opt.MapFrom(x => double.Parse(x.Distance!));
            })
            .ForMember(x => x.DeliveryAddress,
                opt => opt.MapFrom(x => x.DeliveryAddress))
            .ForMember(x => x.ProductAddress,
                opt => opt.MapFrom(x => x.ProductAddress))
            .ForMember(x => x.ProductName,
                opt => opt.MapFrom(x => x.ProductName))
            .ForAllOtherMembers(opt => opt.Ignore());
    }
}