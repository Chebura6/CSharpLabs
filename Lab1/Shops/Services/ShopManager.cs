using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;
namespace Shops.Services;

public class ShopManager
{
    private List<Shop> _shops;
    public ShopManager()
    {
        _shops = new List<Shop>();
    }

    public IReadOnlyCollection<Shop> Shops => _shops;
    public Shop AddShopInSystem(Shop shop)
    {
        ArgumentNullException.ThrowIfNull(shop, "Null shop detected");
        _shops.Add(shop);
        return shop;
    }

    public void CreatePurchase(Client client, Shop shop, ShoppingListElement product)
    {
        ArgumentNullException.ThrowIfNull(client, "Null client detected");
        ArgumentNullException.ThrowIfNull(shop, "Null shop detected");
        ArgumentNullException.ThrowIfNull(product, "Null product detected");
        decimal? purchaseCost = shop.ProductsCheck(product);
        if (purchaseCost is not null && client.Balance >= purchaseCost)
        {
            shop.Buy(product);
            client.CreatePurchase(purchaseCost.Value);
        }
        else
        {
            throw ShopManagerException.TransactionError();
        }
    }

    public void CreateSomePurchases(Client client, Shop shop, List<ShoppingListElement> shoppingList)
    {
        if (!AreSomePurchasesSuccessful(client, shop, shoppingList)) throw ShopManagerException.SomePurchaseMakingError();
        foreach (ShoppingListElement currentProduct in shoppingList)
        {
            client.CreatePurchase(shop.ProductsCheck(currentProduct).Value);
            shop.Buy(currentProduct);
        }
    }

    public Shop FindCheapestShop(Client client, List<ShoppingListElement> shoppingList)
    {
        ArgumentNullException.ThrowIfNull(client, "Null client detected");
        ArgumentNullException.ThrowIfNull(shoppingList, "Null shopping list detected");
        decimal? currentPurchaseCost = 0;
        decimal totalPurchaseCost = 0;
        var possiblePurchase = new List<CheapestPurchaseCandidate>();
        foreach (Shop currentShop in _shops)
        {
            foreach (ShoppingListElement product in shoppingList)
            {
                currentPurchaseCost = currentShop.ProductsCheck(product); // Проверяем, что нужный продукт есть в магазине
                if (currentPurchaseCost is null) break; // если это не так, то заканчиваем проверку данного магазина
                totalPurchaseCost += currentPurchaseCost.Value; // инкрементируем счетчик суммы итоговой покупки
                if (product != shoppingList.Last()) continue; // если текущий элемент не последний, проверяем дальше
                possiblePurchase.Add(new CheapestPurchaseCandidate(currentShop, totalPurchaseCost)); // если продукт послений, добавляем текущий магазин в кандидаты

                                                                                                     // на самый дешевый шоп, вместе с суммой покупки в нем
                totalPurchaseCost = 0; // зануляем счетчик для следующей итерации
            }
        }

        var sortedShops = new List<CheapestPurchaseCandidate>(
            possiblePurchase.OrderBy(o => o.PurchaseCost)); // сортируем кандидатов по сумме покупки
        return sortedShops[0].Shop; // возвращаем магазин с наименьшей суммой
    }

    private bool AreSomePurchasesSuccessful(Client client, Shop shop, List<ShoppingListElement> shoppingList)
    {
        ArgumentNullException.ThrowIfNull(shop, "Null shop detected");
        ArgumentNullException.ThrowIfNull(client, "Null client detected");
        ArgumentNullException.ThrowIfNull(shoppingList, "Null shopping list detected");
        decimal totalCost = 0;
        foreach (decimal? purchaseCost in shoppingList.Select(shop.ProductsCheck))
        {
            if (purchaseCost is not null) totalCost += purchaseCost.Value;
            if (purchaseCost is null) throw ShopManagerException.NotEnoughProductsInShop();
            if (client.Balance < totalCost)
            {
                return false;
            }
        }

        return true;
    }
}