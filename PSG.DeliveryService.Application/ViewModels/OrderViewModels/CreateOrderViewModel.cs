using System.ComponentModel.DataAnnotations;

namespace PSG.DeliveryService.Application.ViewModels.OrderViewModels;

public class CreateOrderViewModel
{
    //TODO: Токен юзера - че?
    public string ClientId { get; set; }
    [Required(ErrorMessage = "Fast or scheduled delivery?")]
    public string OrderType { get; set; }
    
    public string? OrderTime { get; set; }
    
    //TODO: Избавиться от него
    public string DeliveryType { get; set; }
    
    [Required(ErrorMessage = "Specify the weight of the product")]
    public string OrderWeight { get; set; }

    [Required(ErrorMessage = "Enter initial delivery address")]
    public string From { get; set; }

    [Required(ErrorMessage = "Enter final delivery address")]
    public string To { get; set; }

    [Required(ErrorMessage = "Enter product name")]
    public string ProductName { get; set; }
}