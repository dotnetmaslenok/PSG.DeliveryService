using PSG.DeliveryService.Domain.Enums;
using static PSG.DeliveryService.Application.Common.CustomConstants;

namespace PSG.DeliveryService.Application.Helpers;

public static class DeliveryPriceHelper
{
    public static decimal? CalculateDeliveryPrice(OrderType orderType, double distance, OrderWeight orderWeight)
    {
        decimal? price = default;
        
        if (int.TryParse(new string(orderWeight.ToString().Where(char.IsDigit).ToArray()), out int weight))
        {
            if (OrderType.Fast.CompareTo(orderType) == 0)
            {
                price = (decimal) (DeliveryPriceDependencies.FastDeliveryMultiplier * (distance + 1) * weight * DeliveryPriceDependencies.PricePerKm);
            }
            else
            {
                price = (decimal) ((distance + 1) * weight * DeliveryPriceDependencies.PricePerKm);
            }
        }

        return price;
    }
}