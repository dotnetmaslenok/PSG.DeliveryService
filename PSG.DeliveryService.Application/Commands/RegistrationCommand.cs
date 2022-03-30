﻿using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ResultMonad;

namespace PSG.DeliveryService.Application.Commands;

public class RegistrationCommand : BaseAccountCommand, IRequest<Result<string, IEnumerable<IdentityError>>>
{
    [Required(ErrorMessage = "Confirm password is required field")]
    public string? ConfirmedPassword { get; set; }
    
    [Required(ErrorMessage = "Choose who you want to be")]
    public bool IsCourier { get; set; }
}