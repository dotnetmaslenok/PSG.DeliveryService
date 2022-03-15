using System.ComponentModel.DataAnnotations;

namespace PSG.DeliveryService.Application.ViewModels.AccountViewModels;

public class BaseViewModel
{
    [Required]
    [RegularExpression(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$")]
    public string? PhoneNumber { get; set; }

    [Required]
    [MinLength(8)]
    public string? Password { get; set; }
}