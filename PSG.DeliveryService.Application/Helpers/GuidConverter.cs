namespace PSG.DeliveryService.Application.Helpers;

public static class GuidConverter
{
    public static string Encode(Guid? guid)
    {
        return Convert.ToBase64String(guid!.Value.ToByteArray()).Replace('/', '_').Replace('+', '-').Substring(0, 22);
    }

    public static Guid Decode(string encodedGuid)
    {
        encodedGuid = encodedGuid.Replace('_', '/').Replace('-', '+');
        var buffer = Convert.FromBase64String(encodedGuid + "==");
        return new Guid(buffer);
    }
}