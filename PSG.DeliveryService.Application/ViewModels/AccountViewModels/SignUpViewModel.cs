using PSG.DeliveryService.Application.ViewModels.BaseViewModels;

namespace PSG.DeliveryService.Application.ViewModels.AccountViewModels;

public class SignUpViewModel : BaseAccountViewModel
{
    public string UserName { get; set; }
    public bool IsCourier { get; set; }
}