using System.ComponentModel;

namespace PSG.DeliveryService.Domain.Enums;

public enum DeliveryType
{
    [Description("On foot")]
    OnFoot,
    [Description("On car")]
    OnCar,
    [Description("On truck")]
    OnTruck
}