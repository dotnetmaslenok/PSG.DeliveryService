using System.ComponentModel.DataAnnotations;

namespace PSG.DeliveryService.Application.ViewModels.BaseViewModels;

public class BaseAccountViewModel
{
    [Required]
    [RegularExpression(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$")]
    public string? PhoneNumber { get; set; }

    [Required]
    public string? Password { get; set; }
}