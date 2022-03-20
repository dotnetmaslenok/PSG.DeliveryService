namespace PSG.DeliveryService.Application.Common;

public static class CustomConstants
{
    public static class ErrorMessages
    {
        public const string PhoneAlreadyExistsMessage = "User with the login is already in the system";
        public const string SignInError = "Wrong login or password";
    }

    public static class UserClaimValues
    {
        public const string ClientClaimValue = "Client";
        public const string CourierClaimValue = "Courier";
    }
}