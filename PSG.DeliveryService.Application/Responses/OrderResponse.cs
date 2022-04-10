namespace PSG.DeliveryService.Application.Responses;

public sealed class OrderResponse
{
    public string? Id { get; set; }
    
    public string? ProductName { get; set; }

    public string? OrderType { get; set; }

    public DateTime OrderTime { get; set; }
    
    public double Distance { get; set; }

    public decimal TotalPrice { get; set; }

    public string? DeliveryAddress { get; set; }

    public string? ProductAddress { get; set; }

    public int OrderWeight { get; set; }
    
    public string? DeliveryType { get; set; }

    public string? OrderState { get; set; }

    public string? CustomerId { get; set; }

    public string? CustomerPhoneNumber { get; set; }

    public string? CourierId { get; set; }

    public string? CourierPhoneNumber { get; set; }
}