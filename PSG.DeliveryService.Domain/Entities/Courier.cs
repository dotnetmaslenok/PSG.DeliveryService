using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PSG.DeliveryService.Domain.Entities;

public class Courier : IdentityUser<int>
{
    [ForeignKey("PassportCredentials")]
    public int PassportId { get; set; }

    public PassportCredentials? PassportCredentials { get; set; }
    
    public List<Order>? Orders { get; set; }
}
