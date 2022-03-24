using System.Security.Claims;

namespace PSG.DeliveryService.Application.Common;

public static class CustomConstants
{
    public static class ErrorMessages
    {
        public const string PhoneAlreadyExistsMessage = "User with the login is already in the system";
        public const string WrongLoginMessage = "Wrong login";
        public const string WrongPasswordMessage = "Wrong password";
    }

    public static class UserClaims
    {
        public static readonly Claim ClientClaim = new Claim(ClaimTypes.Role, "Client");
        public static readonly Claim CourierClaim = new Claim(ClaimTypes.Role, "Courier");
    }
}