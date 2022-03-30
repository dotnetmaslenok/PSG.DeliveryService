using System.ComponentModel.DataAnnotations;
using MediatR;
using ResultMonad;

namespace PSG.DeliveryService.Application.Commands;

public class CreateOrderCommand : IRequest<Result<CreateOrderCommand, Exception>>
{
    public string? ClientId { get; set; }
    [Required(ErrorMessage = "Fast or scheduled delivery?")]
    public string? OrderType { get; set; }
    
    public string? OrderTime { get; set; }

    [Required(ErrorMessage = "Specify the weight of the product")]
    public string? OrderWeight { get; set; }

    [Required(ErrorMessage = "Enter initial delivery address")]
    public string? From { get; set; }

    [Required(ErrorMessage = "Enter final delivery address")]
    public string? To { get; set; }

    [Required(ErrorMessage = "Enter product name")]
    public string? ProductName { get; set; }
}