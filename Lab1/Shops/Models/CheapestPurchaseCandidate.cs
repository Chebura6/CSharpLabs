using Shops.Entities;

namespace Shops.Models;

public class CheapestPurchaseCandidate
{
    internal CheapestPurchaseCandidate(Shop shop, decimal purchaseCost)
    {
        if (shop is null) throw new ArgumentNullException();
        Shop = shop;
        PurchaseCost = purchaseCost;
    }

    internal Shop Shop { get; }
    internal decimal PurchaseCost { get; }
}