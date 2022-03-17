namespace PSG.DeliveryService.Application.ViewModels.OrderViewModels;

public class CreateOrderViewModel
{
    public string OrderType { get; set; }
    
    public string? OrderTime { get; set; }
    
    public string DeliveryType { get; set; }
    
    public string OrderWeight { get; set; }

    public string From { get; set; }

    public string To { get; set; }

    public string ProductName { get; set; }
}