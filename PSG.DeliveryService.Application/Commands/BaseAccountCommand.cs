using System.ComponentModel.DataAnnotations;

namespace PSG.DeliveryService.Application.Commands;

public class BaseAccountCommand
{
    [Required(ErrorMessage = "Phone number is required field")]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "Password is required field")]
    public string? Password { get; set; }
}