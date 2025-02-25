﻿using System.ComponentModel.DataAnnotations;
using MediatR;
using PSG.DeliveryService.Application.Responses;
using ResultMonad;

namespace PSG.DeliveryService.Application.Commands;

public sealed class CreateOrderCommand : IRequest<Result<OrderResponse>>
{
    [Required]
    public string? ClientId { get; set; }  
    
    [Required(ErrorMessage = "Fast or scheduled delivery?")]
    public string? OrderType { get; set; }
    
    public string? OrderTime { get; set; }

    [Required(ErrorMessage = "Specify the weight of the product")]
    public string? OrderWeight { get; set; }

    [Required]
    public string? Distance { get; set; }

    [Required(ErrorMessage = "Enter initial delivery address")]
    public string? ProductAddress { get; set; }

    [Required(ErrorMessage = "Enter final delivery address")]
    public string? DeliveryAddress { get; set; }

    [Required(ErrorMessage = "Enter product name")]
    public string? ProductName { get; set; }
}