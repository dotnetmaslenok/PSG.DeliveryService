﻿using System.ComponentModel.DataAnnotations;

namespace PSG.DeliveryService.Application.ViewModels.BaseViewModels;

public class BaseAccountViewModel
{
    [Required(ErrorMessage = "Phone number is required field")]
    [RegularExpression(@"^[+][7]-[(]\d{3}[)][-]\d{3}[-]\d{2}-\d{2}")]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "Password is required field")]
    public string? Password { get; set; }
}