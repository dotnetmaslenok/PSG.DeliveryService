namespace PSG.DeliveryService.Domain.Entities;

public class PassportCredentials
{
    public int Id { get; set; }

    public string? FullName { get; set; }

    public string? Series { get; set; }

    public string? Number { get; set; }
}