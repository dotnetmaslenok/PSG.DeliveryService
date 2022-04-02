using AutoMapper;
using PSG.DeliveryService.Application.Commands;
using PSG.DeliveryService.Application.Helpers;
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
            .ForMember(x => x.Id,
                opt => opt.MapFrom(x => GuidConverter.Encode(x.Id)))
            .ForMember(x => x.ProductName,
                opt => opt.MapFrom(x => x.ProductName))
            .ForMember(x => x.OrderType,
                opt => opt.MapFrom(x => x.OrderType.ToString()))
            .ForMember(x => x.OrderTime,
                opt => opt.MapFrom(x => x.OrderTime))
            .ForMember(x => x.TotalPrice,
                opt => opt.MapFrom(x => x.TotalPrice))
            .ForMember(x => x.DeliveryAddress,
                opt => opt.MapFrom(x => x.DeliveryAddress))
            .ForMember(x => x.ProductAddress,
                opt => opt.MapFrom(x => x.ProductAddress))
            .ForMember(x => x.Distance,
                opt => opt.MapFrom(x => x.Distance))
            .ForMember(x => x.OrderWeight,
                opt =>
                {
                    opt.PreCondition(x =>
                        int.TryParse(new string(x.OrderWeight.ToString().Where(char.IsDigit).ToArray()), out _));
                    opt.MapFrom(x => int.Parse(new string(x.OrderWeight.ToString().Where(char.IsDigit).ToArray())));
                })
            .ForMember(x => x.DeliveryType,
                opt => opt.MapFrom(x => x.DeliveryType.ToString()))
            .ForMember(x => x.OrderState,
                opt => opt.MapFrom(x => x.OrderState.ToString()))
            .ForMember(x => x.CustomerId,
                opt =>
                {
                    opt.PreCondition(x => x.CustomerId.HasValue);
                    opt.MapFrom(x => GuidConverter.Encode(x.CustomerId));
                })
            .ForMember(x => x.CourierId,
                opt =>
                {
                    opt.PreCondition(x => x.CourierId.HasValue);
                    opt.MapFrom(x => GuidConverter.Encode(x.CourierId));
                })
            .ForMember(x => x.CustomerPhoneNumber,
                opt =>
                {
                    opt.PreCondition(x => x.Customer is not null);
                    opt.MapFrom(x => x.Customer!.PhoneNumber);
                })
            .ForMember(x => x.CourierPhoneNumber,
                opt =>
                {
                    opt.PreCondition(x => x.Courier is not null);
                    opt.MapFrom(x => x.Courier!.PhoneNumber);
                })
            .ForAllOtherMembers(opt => opt.Ignore());

        CreateMap<ApplicationUser, UserResponse>()
            .ForMember(x => x.Id,
                opt => opt.MapFrom(x => GuidConverter.Encode(x.Id)))
            .ForMember(x => x.IsCourier,
                opt => opt.MapFrom(x => x.IsCourier))
            .ForMember(x => x.OrdersCount, opt =>
                opt.MapFrom(x => x.IsCourier ? x.CourierOrders!.Count : x.CustomerOrders!.Count))
            .ForMember(x => x.PhoneNumber,
                opt => opt.MapFrom(x => x.PhoneNumber))
            .ForMember(x => x.UserRegistrationTime,
                opt => opt.MapFrom(x => x.UserRegistrationTime))
            .ForAllOtherMembers(opt => opt.Ignore());

        CreateMap<CreateOrderCommand, Order>()
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
                opt.PreCondition(x => double.TryParse(x.Distance, out _));
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