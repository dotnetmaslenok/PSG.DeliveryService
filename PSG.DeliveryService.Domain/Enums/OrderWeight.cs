using System.ComponentModel;

namespace PSG.DeliveryService.Domain.Enums;

public enum OrderWeight
{
    [Description("Less Kg")]
    Less1Kg,
    [Description("Less 5 Kg")]
    Less5Kg,
    [Description("Less 10 Kg")]
    Less10Kg,
    [Description("Less 15 Kg")]
    Less15Kg,
    [Description("Less 50 Kg")]
    Less50Kg,
    [Description("Less 100 Kg")]
    Less100Kg,
    [Description("Less 150 Kg")]
    Less150Kg,
    [Description("Less 200 Kg")]
    Less200Kg,
    [Description("Less 500 Kg")]
    Less500Kg,
    [Description("Less 700 Kg")]
    Less700Kg,
    [Description("Less 1000 Kg")]
    Less1000Kg,
    [Description("Less 1500 Kg")]
    Less1500Kg,
}