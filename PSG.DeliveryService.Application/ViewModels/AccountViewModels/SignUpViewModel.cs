using System.ComponentModel.DataAnnotations;
using PSG.DeliveryService.Application.ViewModels.BaseViewModels;

namespace PSG.DeliveryService.Application.ViewModels.AccountViewModels;

public class SignUpViewModel : BaseAccountViewModel
{
    [Required(ErrorMessage = "Username is required field")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Confirm password is required field")]
    public string ConfirmedPassword { get; set; }
    
    [Required(ErrorMessage = "Choose who you want to be")]
    public bool IsCourier { get; set; }
}