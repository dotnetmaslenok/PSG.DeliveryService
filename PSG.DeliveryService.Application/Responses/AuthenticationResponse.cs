namespace PSG.DeliveryService.Application.Responses;

public sealed class AuthenticationResponse
{
    public string? UserId { get; set; }

    public string? AccessToken { get; set; }
}