using Microsoft.AspNetCore.Identity;

namespace PSG.DeliveryService.Domain.Entities;

public class Courier : IdentityUser<int>
{
    public string PassportSN { get; set; }

    public string PassportCredentials { get; set; }

    public List<Order> Orders { get; set; }
}