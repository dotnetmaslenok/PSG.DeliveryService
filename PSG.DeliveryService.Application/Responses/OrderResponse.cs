using PSG.DeliveryService.Domain.Entities;
using PSG.DeliveryService.Domain.Enums;

namespace PSG.DeliveryService.Application.Responses;

public class OrderResponse
{
    public Guid Id { get; set; }
    
    public string? ProductName { get; set; }

    public string? OrderType { get; set; }

    public DateTime OrderTime { get; set; }

    public decimal TotalPrice { get; set; }

    public string? DeliveryAddress { get; set; }

    public string? ProductAddress { get; set; }

    public int OrderWeight { get; set; }

    public OrderState OrderState { get; set; }

    public Guid CustomerId { get; set; }

    public string? CustomerPhoneNumber { get; set; }

    public Guid CourierId { get; set; }

    public string? CourierPhoneNumber { get; set; }
}