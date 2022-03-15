using System.ComponentModel;

namespace PSG.DeliveryService.Domain.Enums;

public enum DeliveryType
{
    [Description("On foot")]
    OnFoot,
    Car,
    Truck
}