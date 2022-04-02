namespace PSG.DeliveryService.Application.Responses;

public class UserResponse
{
    public string? Id { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime UserRegistrationTime { get; set; }
    
    public int OrdersCount { get; set; }

    public bool IsCourier { get; set; }
}