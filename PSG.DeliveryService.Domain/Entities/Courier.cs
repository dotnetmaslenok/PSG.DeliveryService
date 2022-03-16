using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PSG.DeliveryService.Domain.Entities;

public class Courier : IdentityUser<int>
{
    public List<Order>? Orders { get; set; }
}
